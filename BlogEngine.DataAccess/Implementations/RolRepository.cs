using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Implementations
{
    public class RolRepository : IRol
    {
        private readonly BlogEngineContext _context;

        public RolRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public async Task<Rol> Add(Rol entity)
        {
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Rol> Delete(Rol entity)
        {
            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Rol> DeleteById(long id)
        {
            Rol entity = await _context.Roles.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Rol> Update(Rol entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.Roles.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Rol>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> GetById(long id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
