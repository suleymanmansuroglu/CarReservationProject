using Car.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Bussiness.Abstract
{
    public interface ICarModelService
    {
        List<CarModel> GetAll();
    }
}
