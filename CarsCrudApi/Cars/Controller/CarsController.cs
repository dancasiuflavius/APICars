using CarsCrudApi.Cars.Service.Interfaces;
using CarsCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
using CarsCrudApi.Cars.Controller.Interfaces;
using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;
using CarsCrudApi.Cars.Repository.Interfaces;
using CarsCrudApi.Cars.Service;
using CarsCrudApi.Cars.Service.Interfaces;
using CarsCrudApi.System.Exceptions;

namespace CarsCrudApi.Cars.Controller;



public class ProductsController : ProductApiController
{

    private IProductQuerryService _productQueryService;
    private IProductComandService _productCommandService;

    private readonly ILogger<ProductsController> _logger;


    public ProductsController(ILogger<ProductsController> logger, IProductQuerryService productQueryService, IProductComandService productCommandService)
    {
        _logger = logger;
        _productQueryService = productQueryService;
        _productCommandService = productCommandService;
    }



    [HttpGet("api/v1/all")]
    public override async Task<ActionResult<IEnumerable<Car>>> GetProducts()
    {
        try
        {
            var products = await _productQueryService.GetAllProducts();
            return Ok(products);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }


    }


    //[HttpGet("api/v1/getName/{name}")]
    //public async Task<ActionResult<Product>> GetName([FromRoute] string name)
    //{
    //    var product = await _productRepository.GetByNameAsync(name);
    //    return Ok(product);
    //}
    //[HttpGet("api/v1/getAllByPrice")]
    //public async Task<ActionResult<Double>> GetAllAsyncPrice()
    //{
    //    var productPrices = await _productRepository.GetAllAsyncPrice();
    //    return Ok(productPrices);
    //}

    //[HttpPost("api/v1/create")]

    public override async Task<ActionResult<Car>> CreateProduct(CreateCarRequest productRequest)
    {
        _logger.LogInformation(message: $"Rest request: Create product with DTO:\n{productRequest}");
        try
        {
            var product = await _productCommandService.CreateProduct(productRequest);

            return Ok(product);
        }
        catch (InvalidPrice ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ItemAlreadyExists ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }



    }
    // [HttpPut("api/v1/update")]
    public override async Task<ActionResult<Car>> UpdateProduct([FromQuery] int id, [FromBody] UpdateCarRequest request)
    {
        _logger.LogInformation(message: $"Rest request: Create product with DTO:\n{request}");
        try
        {

            Car response = await _productCommandService.UpdateProduct(id, request);

            return Accepted(response);
        }
        catch (InvalidPrice ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(ex.Message);
        }
    }

    //[HttpDelete("api/v1/delete")]
    public override async Task<ActionResult<Car>> DeleteProduct([FromQuery] int id)
    {
        _logger.LogInformation(message: $"Rest request: Delete product with id:\n{id}");
        try
        {
            Car product = await _productCommandService.DeleteProduct(id);

            return Ok(product);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogError(ex.Message + $"Error while trying to delete product: \n{id}");
            return NotFound(ex.Message);
        }
    }
}



