using Autofac;
using BlogEngine.DataAccess.Models;
using BlogEngine.Helpers;
using BlogEngine.Models;
using BlogEngine.Transverse.Constants;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
using BlogEngineAPI.DTO.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    public class RejectedPostsController : BaseController
    {
        public RejectedPostsController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult GetAllRejectedPosts()
        {

            ResponseViewModel response = new ResponseViewModel();

            var userSession = HttpContext.Session.Get<LoggedInUserViewModel>(BasicConst.LOGGED_IN_USER_KEY);
            Task<ResponseList<Post>> responseRejectedPosts = postServices.GeAllRejectedPostByUserId(userSession?.Id ?? 0);
            if (responseRejectedPosts.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting rejected posts.";
                return Json(response);
            }

            List<PostViewModel> rejectedPost = new List<PostViewModel>();
            if (responseRejectedPosts.Result.List != null)
            {
                rejectedPost = MappersFactory.PostViewModel().ListMapView(responseRejectedPosts.Result.List);
            }

            response.Data = rejectedPost;
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = (rejectedPost.Count == 0 ? "You have no rejected posts." : string.Empty);

            return Json(response);

        }

        [HttpPost]
        public JsonResult UpdatePost([FromBody]PostViewModel post)
        {
            ResponseViewModel response = new ResponseViewModel();

            Response responseValidate = ValidatePost(post);
            if (responseValidate.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = responseValidate.Message;
                return Json(response);
            }

            var userSession = HttpContext.Session.Get<LoggedInUserViewModel>(BasicConst.LOGGED_IN_USER_KEY);
            Task<ResponseEntity<User>> responseUserService = userServices.GetUserById(userSession?.Id ?? 0);
            if (responseUserService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting logged user.";
                return Json(response);
            }

            Post postEntity = new Post()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                User = responseUserService.Result.Entity,
                PostStateCode = BasicEnums.PostStates.Submited.GetHashCode().ToString()
            };

            Task<Response> responsePostService = postServices.UpdatePost(postEntity);
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
        public Response ValidatePost(PostViewModel post)
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