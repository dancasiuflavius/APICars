using CarsCrudApi.Cars.Model;

namespace CarsCrudApi.Cars.Service.Interfaces
{
    public interface IProductQuerryService
    {
        Task<IEnumerable<Car>> GetAllProducts();
        Task<IEnumerable<Car>> GetProductsWithCategory(string category);
        Task<IEnumerable<Car>> GetProductsWithNoCategory();
        Task<IEnumerable<Car>> GetProductsInPriceRange(double min, double max);
        Task<Car> GetProductById(int id);
    }
}
