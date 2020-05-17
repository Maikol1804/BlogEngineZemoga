using BlogEngine.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<BlogEngineContext>())
                {
                    AddTestData(context);
                }
            }
        }

        private static void AddTestData(BlogEngineContext context)
        {

            #region Roles

            Rol rolWriter = new Rol()
            {
                Id = 1,
                Code = "1",
                Name = "Writer"
            };
            context.Roles.Add(rolWriter);

            Rol rolEditor = new Rol()
            {
                Id = 2,
                Code = "2",
                Name = "Editor"
            };
            context.Roles.Add(rolEditor);

            #endregion

            #region Writer 1

            User writer1 = new User
            {
                Id = 1,
                UserName = "jmendez",
                FullName = "Julian Mendez",
                Rol = rolWriter
            };
            context.Users.Add(writer1);

            // PASS: Z3moga.852
            PasswordByUser passwordByUserWriter1 = new PasswordByUser()
            {
                Id = 1,
                UserId = 1,
                Hash = "0f27e5f3b235de0bd3a6e4dc771f6892a5b84410ba7c54973bfb2735a072e303",
                Salt = "f6Mv"
            };
            context.PasswordByUsers.Add(passwordByUserWriter1);

            #endregion

            #region Writer 2

            User writer2 = new User
            {
                Id = 2,
                UserName = "mbonilla",
                FullName = "Maikol Bonilla",
                Rol = rolWriter
            };
            context.Users.Add(writer2);

            // PASS: Z3moga.852
            PasswordByUser passwordByUserWriter2 = new PasswordByUser()
            {
                Id = 2,
                UserId = 2,
                Hash = "0f27e5f3b235de0bd3a6e4dc771f6892a5b84410ba7c54973bfb2735a072e303",
                Salt = "f6Mv"
            };
            context.PasswordByUsers.Add(passwordByUserWriter2);

            #endregion

            #region Edtior 1

            User editor1 = new User
            {
                Id = 3,
                UserName = "atovar",
                FullName = "Andres Tovar",
                Rol = rolEditor
            };
            context.Users.Add(editor1);

            // PASS: Z3moga.963
            PasswordByUser passwordByUserEditor1 = new PasswordByUser()
            {
                Id = 3,
                UserId = 3,
                Hash = "40331eb73de18b87602e67b28d4e22a7f9e449f2a446c7f1144b5d5a03d3a185",
                Salt = "hON0"
            };
            context.PasswordByUsers.Add(passwordByUserEditor1);

            #endregion

            #region Editor 2

            User editor2 = new User
            {
                Id = 4,
                UserName = "ozapata",
                FullName = "Oscar Zapata",
                Rol = rolEditor
            };
            context.Users.Add(editor2);

            // PASS: Z3moga.963
            PasswordByUser passwordByUserEditor2 = new PasswordByUser()
            {
                Id = 4,
                UserId = 4,
                Hash = "40331eb73de18b87602e67b28d4e22a7f9e449f2a446c7f1144b5d5a03d3a185",
                Salt = "hON0"
            };
            context.PasswordByUsers.Add(passwordByUserEditor2);

            #endregion

            context.SaveChanges();

        }

    }

}
