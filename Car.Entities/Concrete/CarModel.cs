using Car.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Entities.Concrete
{
    public class CarModel : IEntity
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
}
