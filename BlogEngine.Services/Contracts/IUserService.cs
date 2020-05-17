using BlogEngine.DataAccess.Models;
using BlogEngine.Transverse.Entities;
using System.Threading.Tasks;

namespace BlogEngine.Services.Contracts
{
    public interface IUserService : IService
    {
        Task<ResponseEntity<User>> GetUserByUsername(string username);
    }
}
