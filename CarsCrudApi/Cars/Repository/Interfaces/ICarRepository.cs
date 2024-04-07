using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;

namespace CarsCrudApi.Cars.Repository.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByNameAsync(string name);

        Task<IEnumerable<Double>> GetAllAsyncPrice();
        Task<Car> GetByIdAsync(int id);
        Task<Car> CreateAsync(CreateCarRequest carRequest);
        Task<Car> UpdateAsync(int id, UpdateCarRequest productRequest);
        Task<Car> DeleteAsync(int id);
    }
}
