using BlogEngine.DataAccess.Models;
using BlogEngine.Transverse.Entities;
using System.Threading.Tasks;

namespace BlogEngine.Services.Contracts
{
    public interface IPostService : IService
    {
        Task<Response> SavePost(Post post);
    }
}
