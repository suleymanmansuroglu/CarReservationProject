using Car.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Car.Core.DataAccess.EntityFramework
{
    public class EfEntityDetailRepositoryBase<TEntity, TEntityDetail, TContext> :
        EfEntityRepositoryBase<TEntity, TContext>,
        IEntityDetailRepository<TEntityDetail>
                where TEntityDetail : class, IEntity, new()
                where TEntity : class, IEntity, new()
                where TContext : DbContext, new()

    {
        public virtual IQueryable<TEntityDetail> GetQueryable(TContext context)
        { return new List<TEntityDetail>().AsQueryable(); }

        private List<TEntityDetail> getList(TContext context, Expression<Func<TEntityDetail, bool>> filter = null)
        {
            return GetFilteredQueryable(context, filter).ToList();
        }
        private IQueryable<TEntityDetail> GetFilteredQueryable(TContext context, Expression<Func<TEntityDetail, bool>> filter = null)
        {
            var result = GetQueryable(context);
            if (result == null) { return new List<TEntityDetail>().AsQueryable(); }
            result = result.Where(x => x.Status == 1);
            return filter == null ? result : result.Where(filter);
        }

        public List<TEntityDetail> GetDetailedList(Expression<Func<TEntityDetail, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return getList(context, filter);
            }
        }

        public List<TEntityDetail> GetSortedDetailedList(Expression<Func<TEntityDetail, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                var result = getList(context, filter);
                result.Sort();
                return result;
            }
        }

        public TEntityDetail GetDetail(Expression<Func<TEntityDetail, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return getList(context, filter).FirstOrDefault();
            }
        }
    }
}
