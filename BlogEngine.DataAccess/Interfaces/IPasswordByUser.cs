using BlogEngine.DataAccess.Models;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Interfaces
{
    public interface IPasswordByUser : IRepository<PasswordByUser>
    {
        Task<PasswordByUser> GetByUserId(long id);
    }
}
