using Domain.DTO_s.BrandDto;
using Domain.DTO_s.CityDto;
using Domain.Filters;
using Infrastructure.Response;

namespace Infrastructure.Service.BrandService;

public interface IBrandService
{
    Task<PaginationResponse<List<GetBrandDto>>> GetAll(BrandFilter filter);
    Task<PaginationResponse<List<GetBrandWithCarsDto>>> GetBrandWithCars(BrandFilter filter);
    Task<ApiResponse<GetBrandDto>> GetBrandById(int id);
    Task<ApiResponse<string>> AddBrand(CreateBrandDto brand);
    Task<ApiResponse<string>> UpdateBrand(UpdateBrandDto brand);
    Task<ApiResponse<string>> DeleteBrand(int id);
}