using CUSTOM.DbRepository.BaseDbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.DbRepository
{
    public class Repository<TDbContext> : IRepository<TDbContext> where TDbContext : DbContext
    {
        public Repository(TDbContext dbContext)
        {
            this.dbContext = (TDbContext)dbContext;
        }

        private readonly TDbContext dbContext;

        public TDbContext DbContext
        {
            get
            {
                return dbContext;
            }
        }

        public void Add<T>(T entity) where T : class
        {
            dbContext.Set<T>().Add(entity);
        }

        public IQueryable<T> GetAll<T>(bool trackEntity = false) where T : class
        {
            if (trackEntity)
                return dbContext.Set<T>().AsTracking();
            return dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByWhere<T>(Expression<Func<T, bool>> expression, bool trackEntity = false) where T : class
        {

            if (trackEntity)
                return dbContext.Set<T>().AsTracking<T>().Where<T>(expression);
            return dbContext.Set<T>().AsNoTracking<T>().Where<T>(expression);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        T IRepository<TDbContext>.GetById<T, TK>(TK id, bool trackEntity)
        {
            throw new NotImplementedException();
        }

        public void UndoAllChanges()
        {
            if (dbContext is ApplicationDbContext appDbContext)
            {
                appDbContext.ResetContextAndUndoAllChanges();
            }
            else
            {
                var modifiedEntities = dbContext.ChangeTracker.Entries().ToList();
                foreach (var entry in modifiedEntities)
                {
                    try
                    {
                        entry.State = EntityState.Detached;
                    }
                    catch { }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
