using Domain.DTO_s.BrandDto;
using Domain.DTO_s.DealerDto;
using Domain.Filters;
using Infrastructure.Response;
using Infrastructure.Service.BrandService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class BrandController(IBrandService service)
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetBrandDto>>> GetAll([FromQuery]BrandFilter filter) => await service.GetAll(filter);
    [HttpGet("GetBrandWithCras")]
    public async Task<PaginationResponse<List<GetBrandWithCarsDto>>> GetWithCars([FromQuery]BrandFilter filter)=> await service.GetBrandWithCars(filter);
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetBrandDto>> GetBrandById([FromRoute]int id) => await service.GetBrandById(id);
    [HttpPost]
    public async Task<ApiResponse<string>> Add([FromBody]CreateBrandDto brand) => await service.AddBrand(brand);
    [HttpPut]
    public async Task<ApiResponse<string>> Update([FromBody]UpdateBrandDto brand) => await service.UpdateBrand(brand);
    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.DeleteBrand(id);
}