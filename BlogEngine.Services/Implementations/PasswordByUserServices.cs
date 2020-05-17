using Autofac;
using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Transverse;
using BlogEngine.Transverse.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Services.Implementations
{
    public class PasswordByUserServices : IPasswordByUserService
    {

        private readonly IComponentContext components;
        private readonly IPasswordByUser passwordByUserRepository;

        public PasswordByUserServices(IComponentContext components)
        {
            this.components = components;
            passwordByUserRepository = components.Resolve<IPasswordByUser>();
        }

        public async Task<ResponseEntity<PasswordByUser>> GetPassworByUserByUserId(long id)
        {
            ResponseEntity<PasswordByUser> response = new ResponseEntity<PasswordByUser>();
            try
            {
                response.Entity = await passwordByUserRepository.GetByUserId(id);
                response.State = Transverse.Enumerator.BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = Transverse.Enumerator.BasicEnums.State.Error;
                response.Message = "Error to get the password by user by id.";
            }
            return response;
        }

        public Response ValidatePassword(PasswordByUser user, string password)
        {
            Response response = new Response();
            
            try
            {
                if (!user.Hash.Equals(Security.SHA256Encrypt(string.Concat(user.Salt, password)))) {
                    response.State = Transverse.Enumerator.BasicEnums.State.Error;
                    return response;
                }

                response.State = Transverse.Enumerator.BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = Transverse.Enumerator.BasicEnums.State.Error;
                response.Message = "Error to validates user password.";
            }
            return response;
        }

    }
}
