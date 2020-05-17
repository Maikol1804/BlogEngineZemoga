using Autofac;
using BlogEngine.DataAccess;
using BlogEngine.DataAccess.Implementations;
using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Transverse.Entities;
using System;
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
                response.State = Transverse.Enumerator.BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = Transverse.Enumerator.BasicEnums.State.Error;
                response.Message = "Error to save the post.";
            }
            return response;
        }

    }
}
