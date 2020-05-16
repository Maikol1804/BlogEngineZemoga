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
    public class PostStateRepository : IPostState
    {
        protected IContext _context;
        protected DbSet<PostState> _dbset;

        public PostStateRepository(IContext context)
        {
            _context = context;
            _dbset = _context.Set<PostState>();
        }

        public async Task<PostState> Add(PostState entity)
        {
            _dbset.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PostState> Delete(PostState entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PostState> DeleteById(long id)
        {
            PostState entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PostState> Update(PostState entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbset.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<PostState>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<PostState> GetById(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _dbset.Any(e => e.Id == id);
        }
    }
}
