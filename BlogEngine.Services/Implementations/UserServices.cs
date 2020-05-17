using Autofac;
using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Transverse.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Services.Implementations
{
    public class UserServices : IUserService
    {

        private readonly IComponentContext components;
        private readonly IUser userRepository;

        public UserServices(IComponentContext components)
        {
            this.components = components;
            userRepository = components.Resolve<IUser>();
        }

        public async Task<ResponseEntity<User>> GetUserByUsername(string username)
        {
            ResponseEntity<User> response = new ResponseEntity<User>();
            try
            {
                response.Entity = await userRepository.GetByUsername(username);

                if (response.Entity == null)
                {
                    response.State = Transverse.Enumerator.BasicEnums.State.Error;
                    response.Message = "User not exist.";
                    return response;
                }

                response.State = Transverse.Enumerator.BasicEnums.State.Ok;
            }
            catch (Exception)
            {
                //TODO Save in log
                response.State = Transverse.Enumerator.BasicEnums.State.Error;
                response.Message = "Error to get the user by username.";
            }
            return response;
        }

    }
}
