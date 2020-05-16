using BlogEngine.DataAccess.Context;
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
        protected IContext _context;
        protected DbSet<Post> _dbset;

        public PostRepository(IContext context)
        {
            _context = context;
            _dbset = _context.Set<Post>();
        }

        public async Task<Post> Add(Post entity)
        {
            _dbset.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Post> Delete(Post entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Post> DeleteById(long id)
        {
            Post entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Post> Update(Post entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbset.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _dbset.Include("User").Include("PostState").ToListAsync();
        }

        public async Task<Post> GetById(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _dbset.Any(e => e.Id == id);
        }
    }
}
