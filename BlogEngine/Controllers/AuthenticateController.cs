using Autofac;
using BlogEngine.DataAccess.Models;
using BlogEngine.Helpers;
using BlogEngine.Models;
using BlogEngine.Transverse.Constants;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    public class AuthenticateController : BaseController
    {
        public AuthenticateController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult Login([FromBody]UserViewModel user)
        {
            ResponseViewModel response = new ResponseViewModel();

            if (user == null)
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Login failed.";
                return Json(response);
            }

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Username or password incorrect.";
                return Json(response);
            }

            Task<ResponseEntity<User>> responseUserService = userServices.GetUserByUsername(user.Username);
            if (responseUserService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Username or password incorrect.";
                return Json(response);
            }

            Task<ResponseEntity<PasswordByUser>> responsePasswordByUserService = passwordByUserServices.GetPassworByUserByUserId(responseUserService.Result.Entity.Id);
            if (responsePasswordByUserService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "password hasn't been assigned.";
                return Json(response);
            }

            Response responseValidateService = passwordByUserServices.ValidatePassword(responsePasswordByUserService.Result.Entity, user.Password);
            if (responseValidateService.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Username or password incorrect.";
                return Json(response);
            }

            UserLogguedInViewModel userLogguedInViewModel = new UserLogguedInViewModel()
            {
                FullName = responseUserService.Result.Entity.FullName,
                Username = responseUserService.Result.Entity.UserName,
                RolCode = responseUserService.Result.Entity.Rol.Code,
                RolName = responseUserService.Result.Entity.Rol.Name,
            };

            HttpContext.Session.Set(BasicConst.USER_LOGGED_IN_KEY, userLogguedInViewModel);

            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = "Welcome " + user.Username;

            return Json(response);
        }

    }
}