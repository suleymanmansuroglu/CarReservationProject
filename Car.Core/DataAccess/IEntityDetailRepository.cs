using Car.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Car.Core.DataAccess
{
    public interface IEntityDetailRepository<T> where T : IEntity, new()
    {
        List<T> GetDetailedList(Expression<Func<T, bool>> filter = null);

        List<T> GetSortedDetailedList(Expression<Func<T, bool>> filter = null);

        T GetDetail(Expression<Func<T, bool>> filter = null);
    }
}
