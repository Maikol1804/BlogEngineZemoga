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
            var rolWriter = new Rol()
            {
                Id = 1,
                Code = "01",
                Name = "Writer"
            };
            context.Roles.Add(rolWriter);

            var rolEditor = new Rol()
            {
                Id = 2,
                Code = "02",
                Name = "Editor"
            };
            context.Roles.Add(rolEditor);

            var writer1 = new User
            {
                Id = 1,
                UserName = "jmendez",
                FullName = "Julian Mendez",
                Rol = rolWriter
            };
            context.Users.Add(writer1);

            var writer2 = new User
            {
                Id = 2,
                UserName = "mbonilla",
                FullName = "Maikol Bonilla",
                Rol = rolWriter
            };
            context.Users.Add(writer2);

            var editor1 = new User
            {
                Id = 3,
                UserName = "atovar",
                FullName = "Andre Tovar",
                Rol = rolEditor
            };
            context.Users.Add(editor1);

            var editor2 = new User
            {
                Id = 4,
                UserName = "ozapata",
                FullName = "Oscar Zapata",
                Rol = rolEditor
            };
            context.Users.Add(editor2);

            context.SaveChanges();
        }

    }

}
