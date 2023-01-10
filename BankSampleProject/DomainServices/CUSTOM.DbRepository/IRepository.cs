using CUSTOM.DbRepository.BaseDbModels;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.DbRepository
{
    public interface IRepository<TDbContext>
    {
        TDbContext DbContext { get; }
        int SaveChanges();
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;

        void Update<T>(IEnumerable<T> entities) where T : class;

        IQueryable<T> GetAll<T>(bool trackEntity = false) where T : class;

        T GetById<T, TK>(TK id, bool trackEntity = false) where T : class, IEntity<TK>;

        IQueryable<T> GetByWhere<T>(Expression<Func<T, bool>> expression, bool trackEntity = false) where T : class;

        void UndoAllChanges();


    }
}
