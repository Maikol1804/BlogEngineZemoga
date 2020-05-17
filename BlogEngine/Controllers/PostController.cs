using Autofac;
using BlogEngine.DataAccess;
using BlogEngine.DataAccess.Models;
using BlogEngine.Helpers;
using BlogEngine.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Services.Implementations;
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

            if (post == null) {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error sending post to validate.";
                return Json(response);
            }
            if (string.IsNullOrEmpty(post.Title)) {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Post title is required.";
                return Json(response);
            }
            if (string.IsNullOrEmpty(post.Body))
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Post body is required.";
                return Json(response);
            }

            Post postEntity = new Post()
            {
                Title = post.Title,
                Body = post.Body
            };

            Task<Response> responseService = postServices.SavePost(postEntity);

            if (responseService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription()) 
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error sending post to validate.";
                return Json(response);
            }

            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = "Post sent to validate correctly";
            return Json(response);
        }

    }
}