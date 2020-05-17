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

            List<PostViewModel> writtenPost = new List<PostViewModel>();
            if (responseApprovedPosts.Result.List != null)
            {
                foreach (var post in responseApprovedPosts.Result.List)
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
