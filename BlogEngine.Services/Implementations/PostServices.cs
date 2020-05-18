using Autofac;
using BlogEngine.DataAccess;
using BlogEngine.DataAccess.Implementations;
using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Transverse.Entities;
using BlogEngine.Transverse.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Services.Implementations
{
    public class PostServices : IPostService
    {
        private readonly IComponentContext components;
        private readonly IPost postRepository;

        public PostServices(IComponentContext components) 
        {
            this.components = components;
            postRepository = components.Resolve<IPost>();
        }

        public async Task<Response> SavePost(Post post)
        {
            Response response = new Response();
            try
            {
                await postRepository.Add(post);
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error saving post.";
            }
            return response;
        }

        public async Task<Response> UpdatePost(Post post)
        {
            Response response = new Response();
            try
            {
                await postRepository.Update(post);
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error updating post.";
            }
            return response;
        }

        public async Task<Response> DeletePostById(long id)
        {
            Response response = new Response();
            try
            {
                await postRepository.DeleteById(id);
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error deleting post by id.";
            }
            return response;
        }

        public async Task<ResponseList<Post>> GeAllPendingPostByUserId(long id)
        {
            ResponseList<Post> response = new ResponseList<Post>();
            try
            {
                List<Post> posts = await postRepository.GetAll();
                posts = posts.Where(x => x.User.Id == id && x.PostStateCode == BasicEnums.PostStates.Submited.GetHashCode().ToString()).ToList();
                response.List = posts;
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error to get pendig post by user.";
            }
            return response;
        }

        public async Task<ResponseList<Post>> GeAllRejectedPostByUserId(long id)
        {
            ResponseList<Post> response = new ResponseList<Post>();
            try
            {
                List<Post> posts = await postRepository.GetAll();
                posts = posts.Where(x => x.User.Id == id && x.PostStateCode == BasicEnums.PostStates.Rejected.GetHashCode().ToString()).ToList();
                response.List = posts;
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error to get rejected post by user.";
            }
            return response;
        }

        public async Task<ResponseList<Post>> GeAllCreatedPostByUserId(long id)
        {
            ResponseList<Post> response = new ResponseList<Post>();
            try
            {
                List<Post> posts = await postRepository.GetAll();
                posts = posts.Where(x => x.User.Id == id && x.PostStateCode == BasicEnums.PostStates.Created.GetHashCode().ToString()).ToList();
                response.List = posts;
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error to get created post by user.";
            }
            return response;
        }

        public async Task<ResponseList<Post>> GetAllWrittenPosts()
        {
            ResponseList<Post> response = new ResponseList<Post>();
            try
            {
                List<Post> posts = await postRepository.GetAll();
                posts = posts.Where(x => x.PostStateCode == BasicEnums.PostStates.Submited.GetHashCode().ToString()).ToList();
                response.List = posts;
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error to get all wirtten post.";
            }
            return response;
        }

        public async Task<ResponseList<Post>> GetAllApprovedPosts()
        {
            ResponseList<Post> response = new ResponseList<Post>();
            try
            {
                List<Post> posts = await postRepository.GetAll();
                posts = posts.Where(x => x.PostStateCode == BasicEnums.PostStates.Approved.GetHashCode().ToString()).ToList();
                response.List = posts;
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error to get all approved post.";
            }
            return response;
        }

        public async Task<ResponseEntity<Post>> GetPostById(long Id)
        {
            ResponseEntity<Post> response = new ResponseEntity<Post>();
            try
            {
                Post post = await postRepository.GetById(Id);
                response.Entity = post;
                response.State = BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = BasicEnums.State.Error;
                response.Message = "Error to get post by Id";
            }
            return response;
        }

    }
}
