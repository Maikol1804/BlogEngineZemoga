using BlogEngine.DataAccess.Models;
using BlogEngine.Transverse.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Services.Contracts
{
    public interface IPasswordByUserService : IService
    {
        Task<ResponseEntity<PasswordByUser>> GetPassworByUserByUserId(long id);

        Response ValidatePassword(PasswordByUser user, string password);
    }
}
