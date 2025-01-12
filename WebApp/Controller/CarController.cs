using Domain.DTO_s.CarDto;
using Domain.Filters;
using Infrastructure.Response;
using Infrastructure.Service.CarService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class CarController(ICarService service)
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetCarDto>>> GetAll([FromQuery]CarFilter filter,decimal? minPrice=0,decimal? maxPrice = 0, decimal? maxMilage=0,decimal? minMillage = 0) => await service.GetAll(filter);
    [HttpGet("{dealerId}")]
    public async Task<ApiResponse<List<GetCarDto>>> GetDealerWithCar([FromRoute]int dealerId)  => await service.GetDealerWithCar(dealerId);
    [HttpGet("{brandId}")]
    public async Task<ApiResponse<List<GetCarDto>>> GetBrandWithCar([FromRoute]int brandId)  => await service.GetBrandWithCar(brandId);
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetCarDto>> GetCarById([FromRoute]int id) => await service.GetCarById(id);
    [HttpPost]
    public async Task<ApiResponse<string>> Add([FromBody]CreateCarDto car) => await service.AddCar(car);
    [HttpPut]
    public async Task<ApiResponse<string>> Update([FromBody]UpdateCarDto car) => await service.UpdateCar(car);
    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.DeleteCar(id);
}