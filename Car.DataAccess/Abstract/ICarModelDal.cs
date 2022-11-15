using Car.Core.DataAccess;
using Car.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DataAccess.Abstract
{
    public interface ICarModelDal : IEntityRepository<CarModel>
    {
    }
}
