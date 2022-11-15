using Car.Bussiness.Abstract;
using Car.DataAccess.Abstract;
using Car.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Bussiness.Concrete.Managers
{
    public class CarModelManager : ICarModelService
    {
        private readonly ICarModelDal _carModelDal;
        public CarModelManager(ICarModelDal carModelDal)
        {
            _carModelDal = carModelDal;
        }

        public List<CarModel> GetAll()
        {
            return _carModelDal.GetList();
        }

    }
}
