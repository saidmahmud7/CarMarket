using Domain.DTO_s.CarDto;
using Domain.Filters;
using Infrastructure.Response;

namespace Infrastructure.Service.CarService;

public interface ICarService
{
    Task<PaginationResponse<List<GetCarDto>>> GetAll(CarFilter filter,decimal? minPrice=0, decimal? maxPrice=0,decimal? maxMilage=0,decimal? minMillage = 0);
    Task<ApiResponse<List<GetCarDto>>> GetDealerWithCar(int dealerId);
    Task<ApiResponse<List<GetCarDto>>> GetBrandWithCar(int brandId);
    Task<ApiResponse<GetCarDto>> GetCarById(int id);
    Task<ApiResponse<string>> AddCar(CreateCarDto car);
    Task<ApiResponse<string>> UpdateCar(UpdateCarDto car);
    Task<ApiResponse<string>> DeleteCar(int id);
}