using BlogEngine.DataAccess.Context;
using BlogEngine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess
{
    public class BlogEngineContext : DbContext, IContext
    {
        public BlogEngineContext(DbContextOptions<BlogEngineContext> options) : base(options)
        {
        }

        #region DbSet's Definitions

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<PasswordByUser> PasswordByUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostState> PostStates { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        #endregion

    }

}
