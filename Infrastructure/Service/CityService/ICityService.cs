using Domain.DTO_s.CarDto;
using Domain.DTO_s.CityDto;
using Domain.Filters;
using Infrastructure.Response;

namespace Infrastructure.Service.CityService;

public interface ICityService
{
    Task<PaginationResponse<List<GetCityDto>>> GetAll(CityFilter filter);
    Task<PaginationResponse<List<GetCityWithDealerDto>>> GetCityWithDealer(CityFilter filter);
    Task<ApiResponse<GetCityDto>> GetCityById(int id);
    Task<ApiResponse<string>> AddCity(CreateCityDto city);
    Task<ApiResponse<string>> UpdateCity(UpdateCityDto city);
    Task<ApiResponse<string>> DeleteCity(int id);
}