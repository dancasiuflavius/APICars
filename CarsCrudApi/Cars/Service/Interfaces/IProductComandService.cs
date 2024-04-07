using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;


namespace CarsCrudApi.Cars.Service.Interfaces;


public interface IProductComandService
{
    Task<Car> CreateProduct(CreateCarRequest productRequest);

    Task<Car> UpdateProduct(int id,UpdateCarRequest productRequest);

    Task<Car> DeleteProduct(int id);
}
