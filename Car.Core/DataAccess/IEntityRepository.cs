using Car.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Car.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);

        List<T> GetSortedList(Expression<Func<T, bool>> filter = null);

        bool Any(Expression<Func<T, bool>> filter);

        T Get(Expression<Func<T, bool>> filter);

        T Add(T entity);

        T Update(T entity);

        void DBDelete(T entity);

        void DBDeleteMany(List<T> entities);
        bool CheckIfExistsWithCondition(Expression<Func<T, bool>> filter);
        int GetCountWithCondition(Expression<Func<T, bool>> filter = null);

        void AddMany(List<T> entities);
        List<T> GetListWithDeactivated(Expression<Func<T, bool>> filter = null);
        List<T> UpdateMany(List<T> entities);
    }
}
