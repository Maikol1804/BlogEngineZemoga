using BlogEngine.DataAccess.Models;
using BlogEngine.Transverse.Entities;
using System.Threading.Tasks;

namespace BlogEngine.Services.Contracts
{
    public interface IPostService : IService
    {
        Task<Response> SavePost(Post post);
        Task<Response> UpdatePost(Post post);
        Task<Response> DeletePostById(long id);
        Task<ResponseList<Post>> GeAllPendingPostByUserId(long id);
        Task<ResponseList<Post>> GeAllRejectedPostByUserId(long id);
        Task<ResponseList<Post>> GetAllWrittenPosts();
        Task<ResponseList<Post>> GetAllApprovedPosts();
        Task<ResponseEntity<Post>> GetPostById(long Id);
    }
}
