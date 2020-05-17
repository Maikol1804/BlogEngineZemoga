using Autofac;
using BlogEngine.DataAccess.Models;
using BlogEngine.Helpers;
using BlogEngine.Models;
using BlogEngine.Transverse.Constants;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    public class PostController : BaseController
    {
        public PostController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult SavePost([FromBody]PostViewModel post) 
        {
            ResponseViewModel response = new ResponseViewModel();

            Response responseValidate = ValidatePostViewModel(post);
            if (responseValidate.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = responseValidate.Message;
                return Json(response);
            }

            var userSession = HttpContext.Session.Get<UserLoggedInViewModel>(BasicConst.USER_LOGGED_IN_KEY);
            Task<ResponseEntity<User>> responseUserService = userServices.GetUserById(userSession?.Id??0);
            if (responseUserService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting logged user.";
                return Json(response);
            }

            Post postEntity = new Post()
            {
                Title = post.Title,
                Body = post.Body,
                User = responseUserService.Result.Entity,
                PostStateCode = BasicEnums.PostStates.Created.GetHashCode().ToString()
            };

            Task<Response> responsePostService = postServices.SavePost(postEntity);
            if (responsePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription()) 
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error sending post to check.";
                return Json(response);
            }

            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = "Post sent to validate correctly";
            return Json(response);
        }

        [HttpPost]
        public Response ValidatePostViewModel(PostViewModel post) 
        {
            Response response = new Response
            {
                State = BasicEnums.State.Error
            };

            if (post == null)
            {
                response.Message = "Post can't be empty.";
                return response;
            }
            if (string.IsNullOrEmpty(post.Title))
            {
                response.Message = "Post title is required.";
                return response;
            }
            if (string.IsNullOrEmpty(post.Body))
            {
                response.Message = "Post body is required.";
                return response;
            }

            return new Response()
            {
                State = BasicEnums.State.Ok,
                Message = "Validations passed."
            };
        }

    }
}