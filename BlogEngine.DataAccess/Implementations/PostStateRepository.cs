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
        private readonly BlogEngineContext _context;

        public PostStateRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public async Task<PostState> Add(PostState entity)
        {
            _context.PostStates.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PostState> Delete(PostState entity)
        {
            _context.PostStates.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PostState> DeleteById(long id)
        {
            PostState entity = await _context.PostStates.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.PostStates.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PostState> Update(PostState entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.PostStates.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<PostState>> GetAll()
        {
            return await _context.PostStates.ToListAsync();
        }

        public async Task<PostState> GetById(long id)
        {
            return await _context.PostStates.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _context.PostStates.Any(e => e.Id == id);
        }
    }
}
