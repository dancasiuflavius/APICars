using AutoMapper;
using CarsCrudApi.Cars.Dto;
using CarsCrudApi.Cars.Model;

namespace CarsCrudApi.Cars.Mappings
{
    public class CarMappingProfile : Profile
    {

        public CarMappingProfile()
        {
            CreateMap<CreateCarRequest, Car>();
        }

    }
}
