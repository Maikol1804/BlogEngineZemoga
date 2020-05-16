using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Context
{
    public interface IContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        //DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        //Database DataBase { get; }
        //bool IsOnTransaction();
        //void BeginTransaction();
        //void CommitTransaction();
        //void RollbackTransaction();
        Task<int> SaveChangesAsync();
        //void SetAutoDetectChangesEnabled(bool value);
        //void SetValidateOnSaveEnabled(bool value);
    }
}
