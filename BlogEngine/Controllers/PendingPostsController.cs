using Autofac;
using BlogEngine.DataAccess.Models;
using BlogEngine.Helpers;
using BlogEngine.Models;
using BlogEngine.Transverse.Constants;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
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

            var userSession = HttpContext.Session.Get<UserLoggedInViewModel>(BasicConst.USER_LOGGED_IN_KEY);

            Task<ResponseList<Post>> responsePendingPosts = postServices.GeAllPendingPostByUserId(userSession?.Id ?? 0);
            if (responsePendingPosts.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.Code = BasicEnums.State.Error.GetHashCode().ToString();
                response.Message = "Error getting pending posts.";
                return Json(response);
            }

            List<PostViewModel> pendingPost = new List<PostViewModel>();
            if (responsePendingPosts.Result.List != null) {
                foreach (var pending in responsePendingPosts.Result.List) {
                    pendingPost.Add(new PostViewModel()
                    {
                        Title = pending.Title,
                        Body = pending.Body,
                        CreatedDate = pending.CreatedDate.ToString("dddd, dd MMMM yyyy HH:mm", new CultureInfo("en-US"))
                    }); 
                }
            }
            
            response.Data = pendingPost;
            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
            response.Message = (pendingPost.Count == 0 ? "You have no pending posts." : string.Empty);

            return Json(response);

        }

    }
}