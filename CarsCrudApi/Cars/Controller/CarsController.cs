using Microsoft.AspNetCore.Mvc;
using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;
using CarsCrudApi.Cars.Repository;
using CarsCrudApi.Cars.Repository.Interfaces;

namespace CarsCrudApi.Cars.Controller
{
    [ApiController]
    [Route("Cars")]

    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;

        private readonly ICarRepository _carRepository;

        public CarsController(ILogger<CarsController> logger, ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }



        [HttpGet("api/v1/all")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAll()
        {

            var products = await _carRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("api/v1/getName/{name}")]
        public async Task<ActionResult<Car>> GetName([FromRoute] string name)
        {
            var product = await _carRepository.GetByNameAsync(name);
            return Ok(product);
        }
        [HttpGet("api/v1/getAllByPrice")]
        public async Task<ActionResult<Double>> GetAllAsyncPrice()
        {
            var cars = await _carRepository.GetAllAsyncPrice();
            return Ok(cars);
        }
        [HttpPost("api/v1/create")]

        public async Task<ActionResult<Car>> CreateProduct([FromBody] CreateCarRequest createCarRequest)
        {
            var product = await _carRepository.CreateAsync(createCarRequest);


            return Ok(product);

        }
    }
}
