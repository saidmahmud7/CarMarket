using Domain.DTO_s.CityDto;
using Domain.DTO_s.DealerDto;
using Domain.Filters;
using Infrastructure.Response;
using Infrastructure.Service.CityService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;


[ApiController]
[Route("api/[controller]")]
public class CityController(ICityService service)
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetCityDto>>> GetAll([FromQuery]CityFilter filter) => await service.GetAll(filter);
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetCityDto>> GetCityById([FromRoute]int id) => await service.GetCityById(id);
    [HttpGet("GetCityWithDealer")]
    public async Task<PaginationResponse<List<GetCityWithDealerDto>>> GetWithDealer([FromQuery]CityFilter filter)=> await service.GetCityWithDealer(filter);
    [HttpPost]
    public async Task<ApiResponse<string>> Add([FromBody]CreateCityDto city) => await service.AddCity(city);
    [HttpPut]
    public async Task<ApiResponse<string>> Update([FromBody]UpdateCityDto city) => await service.UpdateCity(city);
    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.DeleteCity(id); 
}