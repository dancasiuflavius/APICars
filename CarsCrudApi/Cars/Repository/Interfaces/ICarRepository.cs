using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;

namespace CarsCrudApi.Cars.Repository.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByNameAsync(string name);

        Task<IEnumerable<Double>> GetAllAsyncPrice();
        //Task<Product> GetByIdAsync(int id);
        Task<Car> CreateAsync(CreateCarRequest carRequest);
        //Task<Product> UpdateAsync(int id, UpdateProductRequest productRequest);
        //Task DeleteAsync(int id);
    }
}
