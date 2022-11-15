using Car.Bussiness.Abstract;
using Car.Entities.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarReservationApi.Controllers
{

    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelService _carModelService;
        public CarModelController(ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetList()
        {
            try
            {

                var area = _carModelService.GetAll();
                return new JsonResult(new CarJsonResult { ResultObject = area, Success = true, Message = "Başarılı" });

            }
            catch (Exception ex)
            {
                return new JsonResult(new CarJsonResult { Success = false, Message = ex.Message });

            }

        }
    }
}
