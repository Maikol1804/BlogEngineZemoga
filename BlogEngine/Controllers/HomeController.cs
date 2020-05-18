using BlogEngine.Helpers;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Models;
using BlogEngine.Transverse.Enumerator;
using BlogEngine.Transverse.Entities;
using BlogEngine.DataAccess.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System;
using BlogEngine.Transverse.Constants;
using BlogEngineAPI.DTO.Mappers;

namespace BlogEngine.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult GetAllApprovedPosts()
        {

            ResponseViewModel response = new ResponseViewModel();

            Task<ResponseList<Post>> responseApprovedPosts = postServices.GetAllApprovedPosts();
            if (responseApprovedPosts.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting approved posts.";
                return Json(response);
            }

            List<PostViewModel> approvedPost = new List<PostViewModel>();
            if (responseApprovedPosts.Result.List != null)
            {
                approvedPost = MappersFactory.PostViewModel().ListMapView(responseApprovedPosts.Result.List);
            }

            response.Data = approvedPost;
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = (approvedPost.Count == 0 ? "No one has written a post yet." : string.Empty);

            return Json(response);

        }

        [HttpPost]
        public JsonResult DeletePost([FromBody]long id)
        {
            ResponseViewModel response = new ResponseViewModel();

            if (id == 0)
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error deleting post by Id.";
                return Json(response);
            }

            Task<Response> responseUpdatePostService = postServices.DeletePostById(id);
            if (responseUpdatePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error deleting post.";
                return Json(response);
            }

            response.Message = "Post deleted correctly.";
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();

            return Json(response);
        }

        [HttpPost]
        public JsonResult AddComment([FromBody]PostViewModel post)
        {
            ResponseViewModel response = new ResponseViewModel();

            Response responseValidate = ValidatePostViewModel(post);
            if (responseValidate.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = responseValidate.Message;
                return Json(response);
            }

            Task<ResponseEntity<Post>> responsePostService = postServices.GetPostById(post.Id);
            if (responsePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting post by Id.";
                return Json(response);
            }

            var userSession = HttpContext.Session.Get<LoggedInUserViewModel>(BasicConst.LOGGED_IN_USER_KEY);
            string authorName = BasicConst.ANONYMOUS_NAME;

            if (userSession != null)
            {
                Task<ResponseEntity<User>> responseUserService = userServices.GetUserById(userSession?.Id ?? 0);
                if (responseUserService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
                {
                    response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                    response.Message = "Error getting logged user.";
                    return Json(response);
                }
                authorName = responseUserService.Result.Entity.FullName;
            }

            responsePostService.Result.Entity.Comments.Add(new Comment()
            {
                Author = authorName,
                Body = post.CurrentComment
            });

            Task<Response> responseUpdatePostService = postServices.UpdatePost(responsePostService.Result.Entity);
            if (responseUpdatePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error adding comment to post.";
                return Json(response);
            }

            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = "Comment added correctly to post";
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

        [HttpPost]
        public JsonResult GetLoggedInUser()
        {
            ResponseViewModel response = new ResponseViewModel();

            try
            {
                response.Data = HttpContext.Session.Get<LoggedInUserViewModel>(BasicConst.LOGGED_IN_USER_KEY);
                response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            }
            catch (Exception)
            {
                //TODO Save to Log
                response.Message = "Error getting logged in user";
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
            }

            return Json(response);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            ResponseViewModel response = new ResponseViewModel();

            try 
            {
                HttpContext.Session.Clear();
                response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            } 
            catch (Exception) 
            {
                //TODO Save to Log
                response.Message = "Error trying log out";
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
            }

            return Json(response);
        }
    }
}
