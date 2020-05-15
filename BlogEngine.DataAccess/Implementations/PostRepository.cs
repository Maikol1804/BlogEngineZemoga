using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Implementations
{
    public class PostRepository : IPost
    {
        private readonly BlogEngineContext _context;

        public PostRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public async Task<Post> Add(Post entity)
        {
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Post> Delete(Post entity)
        {
            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Post> DeleteById(long id)
        {
            Post entity = await _context.Posts.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Post> Update(Post entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _context.Posts.Include("User").Include("PostState").ToListAsync();
        }

        public async Task<Post> GetById(long id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
