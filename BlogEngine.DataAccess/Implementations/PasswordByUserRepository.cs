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
        private readonly BlogEngineContext _context;

        public PasswordByUserRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public async Task<PasswordByUser> Add(PasswordByUser entity)
        {
            _context.PasswordByUsers.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PasswordByUser> Delete(PasswordByUser entity)
        {
            _context.PasswordByUsers.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PasswordByUser> DeleteById(long id)
        {
            PasswordByUser entity = await _context.PasswordByUsers.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.PasswordByUsers.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PasswordByUser> Update(PasswordByUser entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.PasswordByUsers.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<PasswordByUser>> GetAll()
        {
            return await _context.PasswordByUsers.Include("User").ToListAsync();
        }

        public async Task<PasswordByUser> GetById(long id)
        {
            return await _context.PasswordByUsers.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _context.PasswordByUsers.Any(e => e.Id == id);
        }
    }
}
