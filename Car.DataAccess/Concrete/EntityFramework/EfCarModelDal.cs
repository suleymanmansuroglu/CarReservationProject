using Car.Core.DataAccess.EntityFramework;
using Car.DataAccess.Abstract;
using Car.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DataAccess.Concrete.EntityFramework
{
    public class EfCarModelDal : EfEntityRepositoryBase<CarModel, CarReservationContext>, ICarModelDal
    {

    }
}
