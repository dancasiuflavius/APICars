using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;
using CarsCrudApi.Cars.Repository;
using CarsCrudApi.Cars.Repository.Interfaces;
using CarsCrudApi.Cars.Service.Interfaces;
using CarsCrudApi.System.Constants;
using CarsCrudApi.System.Exceptions;

namespace CarsCrudApi.Cars.Service
{
    public class ProductsCommandService : IProductComandService
    {
        private ICarRepository _repository;

        public ProductsCommandService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<Car> CreateProduct(CreateCarRequest productRequest)
        {
            if(productRequest.Price < 0)
            {
                throw new InvalidPrice(Constants.INVALID_PRICE);
            }

            Car product = await _repository.GetByNameAsync(productRequest.Name);

            if(product !=null)
            {
                throw new ItemAlreadyExists(Constants.PRODUCT_ALREADY_EXISTS);
            }

            product = await _repository.CreateAsync(productRequest);
            return product;
        }
        public async Task<Car> UpdateProduct(int id, UpdateCarRequest productRequest)
        {
            if (productRequest.Price < 0)
            {
                throw new InvalidPrice(Constants.INVALID_PRICE);
            }

            Car product = await _repository.GetByIdAsync(productRequest.Id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }
            product = await _repository.UpdateAsync(id,productRequest);
            return product;
        }
        public async Task<Car> DeleteProduct(int id)
        {
            Car product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            await _repository.DeleteAsync(id);
            return product;
        }
    }
}
