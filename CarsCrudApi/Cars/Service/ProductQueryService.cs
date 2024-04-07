using CarsCrudApi.Cars.Model;
using CarsCrudApi.Cars.Repository;
using CarsCrudApi.Cars.Repository.Interfaces;
using CarsCrudApi.Cars.Service.Interfaces;
using CarsCrudApi.System.Constants;
using CarsCrudApi.System.Exceptions;

namespace CarsCrudApi.Cars.Service;

public class ProductQueryService : IProductQuerryService
{
    private ICarRepository _repository;

    public ProductQueryService(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Car>> GetAllProducts()
    {
        IEnumerable<Car> products = await _repository.GetAllAsync();

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Car>> GetProductsWithCategory(string category)
    {
        IEnumerable<Car> products = (await _repository.GetAllAsync())
            .Where(product => product.Category.Equals(category));

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Car>> GetProductsWithNoCategory()
    {
        IEnumerable<Car> products = (await _repository.GetAllAsync())
            .Where(product => product.Category == null!);

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Car>> GetProductsInPriceRange(double min, double max)
    {
        IEnumerable<Car> products = (await _repository.GetAllAsync())
            .Where(product => product.Price >= min && product.Price <= max);

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<Car> GetProductById(int id)
    {
        Car product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
        }

        return product;
    }
}
