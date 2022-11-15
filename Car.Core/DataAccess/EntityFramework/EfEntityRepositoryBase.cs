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
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
                 where TEntity : class, IEntity, new()
                 where TContext : DbContext, new()
    {
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                 ? getActiveList(context).ToList()
                 : getActiveList(context).Where(filter).ToList();
            }
        }

        public List<TEntity> GetListWithDeactivated(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter != null
                    ? context.Set<TEntity>().Where(filter).ToList()
                    : context.Set<TEntity>().ToList();
            }
        }

        public List<TEntity> GetSortedList(Expression<Func<TEntity, bool>> filter = null)
        {
            var result = GetList(filter);
            if (result != null && result.Count > 1) { result.Sort(); }
            return result;
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                var list = getActiveList(context);
                bool result = list.Any(filter);
                return result;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return getActiveList(context).SingleOrDefault(filter);
            }
        }

        private IQueryable<TEntity> getActiveList(TContext context)
        {
            return context.Set<TEntity>().Where(x => x.Status == 1);
        }

        public bool CheckIfExistsWithCondition(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Where(entity => entity.Status == 1).Any(filter);
            }
        }

        public int GetCountWithCondition(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                int count = filter == null
                    ? context.Set<TEntity>().Count()
                    : context.Set<TEntity>().Where(filter).Count();

                return count;
            }
        }

        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                entity.Status = 1;
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        public void AddMany(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Status = 1;
            }
            using (var context = new TContext())
            {
                context.Set<TEntity>().AddRange(entities);
                context.SaveChanges();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }
        public List<TEntity> UpdateMany(List<TEntity> entities)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().UpdateRange(entities);
                context.SaveChanges();
                return entities;
            }
        }

        public void DBDelete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public void DBDeleteMany(List<TEntity> entities)
        {
            using (var context = new TContext())
            {
                context.RemoveRange(entities);
                context.SaveChanges();
            }
        }
    }
}
