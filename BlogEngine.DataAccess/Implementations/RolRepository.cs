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
    public class RolRepository : IRol
    {
        protected IContext _context;
        protected DbSet<Rol> _dbset;

        public RolRepository(IContext context)
        {
            _context = context;
            _dbset = _context.Set<Rol>();
        }

        public async Task<Rol> Add(Rol entity)
        {
            _dbset.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Rol> Delete(Rol entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Rol> DeleteById(long id)
        {
            Rol entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Rol> Update(Rol entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbset.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Rol>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Rol> GetById(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public bool Exist(long id)
        {
            return _dbset.Any(e => e.Id == id);
        }
    }
}
