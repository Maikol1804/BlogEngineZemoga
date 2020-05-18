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

        // GET: api/approvalposts
        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> GetAllWrittenPosts()
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

        // PUT: api/approvalposts/1/approve
        [HttpPut("{id:long}/approve")]
        public ActionResult<Response> PutApprovePost(long id)
        {
            Response response = new Response();

            if (id == 0)
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error getting post by Id.";
                return BadRequest(response);
            }

            Task<ResponseEntity<Post>> responsePostService = postServices.GetPostById(id);
            if (responsePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error getting post by Id.";
                return BadRequest(response);
            }

            responsePostService.Result.Entity.PostStateCode = BasicEnums.PostStates.Approved.GetHashCode().ToString();
            responsePostService.Result.Entity.ApprovalDate = DateTime.Now;

            Task<Response> responseUpdatePostService = postServices.UpdatePost(responsePostService.Result.Entity);
            if (responseUpdatePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error approving post.";
                return BadRequest(response);
            }

            response.Message = "Post approved correctly.";
            response.State = BasicEnums.State.Ok;

            return Ok(response);
        }

        // PUT: api/approvalposts/1/reject
        [HttpPut("{id:long}/reject")]
        public ActionResult<Response> PutRejectPost(long id)
        {
            Response response = new Response();

            if (id == 0)
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error getting post by Id.";
                return BadRequest(response);
            }

            Task<ResponseEntity<Post>> responsePostService = postServices.GetPostById(id);
            if (responsePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error getting post by Id.";
                return BadRequest(response);
            }

            responsePostService.Result.Entity.PostStateCode = BasicEnums.PostStates.Rejected.GetHashCode().ToString();

            Task<Response> responseUpdatePostService = postServices.UpdatePost(responsePostService.Result.Entity);
            if (responseUpdatePostService.Result.State.GetDescription() == BasicEnums.State.Error.GetDescription())
            {
                response.State = BasicEnums.State.Error;
                response.Message = "Error rejecting post.";
                return BadRequest(response);
            }

            response.Message = "Post rejected correctly.";
            response.State = BasicEnums.State.Ok;

            return Ok(response);
        }

    }
}