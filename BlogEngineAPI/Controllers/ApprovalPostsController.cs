using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
using BlogEngineAPI.DTO;
using BlogEngineAPI.DTO.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalPostsController : ControllerBase
    {
        public readonly IComponentContext component;
        public readonly IPostService postServices;

        public ApprovalPostsController(IComponentContext component)
        {
            this.component = component;
            postServices = this.component.Resolve<IPostService>();
        }

        // GET: api/ApprovalPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllWrittenPosts()
        {
            Response response = new Response();

            Task<ResponseList<Post>> responseWrittenPosts = postServices.GetAllWrittenPosts();
            if (responseWrittenPosts.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error getting written posts.";
                return BadRequest(response);
            }

            return MappersFactory.PostDTO().ListMap(responseWrittenPosts.Result.List);
        }
    }
}