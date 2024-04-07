using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarsCrudApi.Cars.Controller.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ProductApiController:ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode: 200, type: typeof(List<Car>))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    public abstract Task<ActionResult<IEnumerable<Car>>> GetProducts();

    [HttpPost("create")]
    [ProducesResponseType(statusCode: 200, type: typeof(Car))]
    [ProducesResponseType(statusCode: 400, type: typeof(String))]
    public abstract Task<ActionResult<Car>> CreateProduct([FromBody] CreateCarRequest productRequest);

    [HttpPut("update")]
    [ProducesResponseType(statusCode: 200, type: typeof(Car))]
    [ProducesResponseType(statusCode: 400, type: typeof(String))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    public abstract Task<ActionResult<Car>> UpdateProduct([FromQuery] int id,[FromBody] UpdateCarRequest productRequest);

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Car))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    public abstract Task<ActionResult<Car>> DeleteProduct([FromRoute] int id);
}
