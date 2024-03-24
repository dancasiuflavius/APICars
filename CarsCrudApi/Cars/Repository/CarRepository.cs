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

        public async Task<Car> CreateAsync(CreateCarRequest carRequest)
        {

            var car = _mapper.Map<Car>(carRequest);


            _context.Cars.Add(car);

            await _context.SaveChangesAsync();

            return car;

        }
    }
}
