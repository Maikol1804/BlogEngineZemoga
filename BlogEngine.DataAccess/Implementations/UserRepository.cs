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
    public class UserRepository : IUser
    {
        protected IContext _context;
        protected DbSet<User> _dbset;

        public UserRepository(IContext context)
        {
            _context = context;
            _dbset = _context.Set<User>();
        }

        public async Task<User> Add(User entity)
        {
            _dbset.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> Delete(User entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> DeleteById(long id)
        {
            User entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> Update(User entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbset.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _dbset.Any(e => e.Id == id);
        }
    }
}
