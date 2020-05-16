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

            Post postEntity = new Post()
            {
                Title = post.Title,
                Body = post.Body
            };

            Task<Response> responseService = postServices.SavePost(postEntity);

            if (responseService.Result.State.GetDescription() == BasicEnums.State.Ok.GetDescription()) 
            {
                response.Code = BasicEnums.State.Ok.GetHashCode().ToString();
                response.Message = "Post sent to validate correctly";
            }

            return Json(response);
        }

    }
}