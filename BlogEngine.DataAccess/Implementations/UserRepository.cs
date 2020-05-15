using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Implementations
{
    public class UserRepository : IUser
    {
        private readonly BlogEngineContext _context;

        public UserRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> Delete(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> DeleteById(long id)
        {
            User entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> Update(User entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
