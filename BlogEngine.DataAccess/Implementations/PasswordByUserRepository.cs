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
    public class PasswordByUserRepository : IPasswordByUser
    {
        protected IContext _context;
        protected DbSet<PasswordByUser> _dbset;

        public PasswordByUserRepository(IContext context)
        {
            _context = context;
            _dbset = _context.Set<PasswordByUser>();
        }

        public async Task<PasswordByUser> Add(PasswordByUser entity)
        {
            _dbset.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PasswordByUser> Delete(PasswordByUser entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PasswordByUser> DeleteById(long id)
        {
            PasswordByUser entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PasswordByUser> Update(PasswordByUser entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbset.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<PasswordByUser>> GetAll()
        {
            return await _dbset.Include("User").ToListAsync();
        }

        public async Task<PasswordByUser> GetById(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _dbset.Any(e => e.Id == id);
        }

        public async Task<PasswordByUser> GetByUserId(long id)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.UserId == id);
        }
    }
}
