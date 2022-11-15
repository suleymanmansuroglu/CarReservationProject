using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Entities.Concrete
{
    public class CarJsonResult
    {
        public object ResultObject { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public bool Success { get; set; }
    }
}
