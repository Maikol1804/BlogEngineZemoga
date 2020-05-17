using Autofac;
using BlogEngine.DataAccess.Models;
using BlogEngine.Helpers;
using BlogEngine.Models;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    public class ApprovalPostsController : BaseController
    {
        public ApprovalPostsController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult GetAllWrittenPosts()
        {
            ResponseViewModel response = new ResponseViewModel();

            Task<ResponseList<Post>> responseWrittenPosts = postServices.GetAllWrittenPosts();
            if (responseWrittenPosts.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting written posts.";
                return Json(response);
            }

            List<PostViewModel> writtenPost = new List<PostViewModel>();
            if (responseWrittenPosts.Result.List != null)
            {
                foreach (var post in responseWrittenPosts.Result.List)
                {
                    writtenPost.Add(new PostViewModel()
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CreatedDate = post.CreatedDate.ToString("dddd, dd MMMM yyyy HH:mm", new CultureInfo("en-US")),
                        CreatorFullName = post.User.FullName
                    });
                }
            }

            response.Data = writtenPost;
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = (writtenPost.Count == 0 ? "No one has written a post yet." : string.Empty);

            return Json(response);

        }

        [HttpPost]
        public JsonResult ApprovePost([FromBody]long id) {

            ResponseViewModel response = new ResponseViewModel();

            if (id == 0) {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting post by Id.";
                return Json(response);
            }

            Task<ResponseEntity<Post>> responsePostService = postServices.GetPostById(id);
            if (responsePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting post by Id.";
                return Json(response);
            }

            responsePostService.Result.Entity.PostStateCode = BasicEnums.PostStates.Approved.GetHashCode().ToString();

            Task<Response> responseUpdatePostService = postServices.UpdatePost(responsePostService.Result.Entity);
            if (responseUpdatePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error approving post.";
                return Json(response);
            }

            response.Message = "Post approved correctly.";
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();

            return Json(response);
        }

        [HttpPost]
        public JsonResult RejectPost([FromBody]long id)
        {
            ResponseViewModel response = new ResponseViewModel();

            if (id == 0)
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting post by Id.";
                return Json(response);
            }

            Task<ResponseEntity<Post>> responsePostService = postServices.GetPostById(id);
            if (responsePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting post by Id.";
                return Json(response);
            }

            responsePostService.Result.Entity.PostStateCode = BasicEnums.PostStates.Rejected.GetHashCode().ToString();

            Task<Response> responseUpdatePostService = postServices.UpdatePost(responsePostService.Result.Entity);
            if (responseUpdatePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error rejecting post.";
                return Json(response);
            }

            response.Message = "Post rejected correctly.";
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();

            return Json(response);
        }

    }
}