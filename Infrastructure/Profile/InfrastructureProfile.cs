using Domain;
using Domain.DTO_s.BrandDto;
using Domain.DTO_s.CarDto;
using Domain.DTO_s.CityDto;
using Domain.DTO_s.DealerDto;

namespace Infrastructure.Profile;

public class InfrastructureProfile : AutoMapper.Profile
{
    public InfrastructureProfile()
    {
        CreateMap<CreateCarDto,Car>();
        CreateMap<Car, GetCarDto>();

        CreateMap<CreateCityDto, City>();
        CreateMap<City, GetCityDto>();
        CreateMap<City, GetCityWithDealerDto>();

        CreateMap<CreateDealerDto, Dealer>();
        CreateMap<Dealer, GetDealerDto>();

        CreateMap<CreateBrandDto, Brand>();
        CreateMap<Brand, GetBrandDto>();
        CreateMap<Brand, GetBrandWithCarsDto>();
    }
}