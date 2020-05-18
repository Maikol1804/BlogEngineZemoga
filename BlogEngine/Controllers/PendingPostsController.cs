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
using System.Globalization;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    public class PendingPostsController : BaseController
    {
        public PendingPostsController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult GetAllPendingPosts() {

            ResponseViewModel response = new ResponseViewModel();

            var userSession = HttpContext.Session.Get<LoggedInUserViewModel>(BasicConst.LOGGED_IN_USER_KEY);
            Task<ResponseList<Post>> responsePendingPosts = postServices.GeAllPendingPostByUserId(userSession?.Id ?? 0);
            if (responsePendingPosts.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting pending posts.";
                return Json(response);
            }

            List<PostViewModel> pendingPost = new List<PostViewModel>();
            if (responsePendingPosts.Result.List != null) 
            {
                pendingPost = MappersFactory.PostViewModel().ListMapView(responsePendingPosts.Result.List);
            }
            
            response.Data = pendingPost;
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = (pendingPost.Count == 0 ? "You have no pending posts." : string.Empty);

            return Json(response);

        }

    }
}