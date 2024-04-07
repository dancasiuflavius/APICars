using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CarsCrudApi.Data;
using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;
using CarsCrudApi.Cars.Repository.Interfaces;

namespace CarsCrudApi.Cars.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public CarRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }
        public async Task<Car> GetByNameAsync(string name)
        {
            return await _context.Cars.FirstOrDefaultAsync(car => car.Name.Equals(name));

        }
        public async Task<IEnumerable<Double>> GetAllAsyncPrice()
        {

            return await _context.Cars.Select(product => product.Price).ToListAsync();
        }

        
        public async Task<Car> CreateAsync(CreateCarRequest productRequest)
        {

            var product = _mapper.Map<Car>(productRequest);


            _context.Cars.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }
        public async Task<Car> UpdateAsync(int id, UpdateCarRequest request)
        {
            var product = await _context.Cars.FindAsync(id);

            product.Name = request.Name ?? product.Name;
            product.Price = request.Price ?? product.Price;
            product.Category = request.Category ?? product.Category;
            product.DateOfFabrication = request.DateOfFabrication ?? product.DateOfFabrication;

            _context.Cars.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Car> DeleteAsync(int id)
        {
            var product = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
