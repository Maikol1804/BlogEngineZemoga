using BlogEngine.DataAccess.Models;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Interfaces
{
    public interface IUser : IRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}
