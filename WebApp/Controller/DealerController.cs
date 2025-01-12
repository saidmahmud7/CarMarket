using Domain.DTO_s.CarDto;
using Domain.DTO_s.DealerDto;
using Domain.Filters;
using Infrastructure.Response;
using Infrastructure.Service.DealerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class DealerController(IDealerService service)
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetDealerDto>>> GetAll([FromQuery]DealerFilter filter) => await service.GetAll(filter);
    [HttpGet("GroupWithCity")]
    public async Task<PaginationResponse<List<GetDealerDto>>> GetWithCity([FromQuery]DealerFilter filter) => await service.GetDealerWithCity(filter);
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetDealerDto>> GetDealerById([FromRoute]int id) => await service.GetDealerById(id);
    [HttpPost]
    public async Task<ApiResponse<string>> Add([FromBody]CreateDealerDto dealer) => await service.AddDealer(dealer);
    [HttpPut]
    public async Task<ApiResponse<string>> Update([FromBody]UpdateDealerDto dealer) => await service.UpdateDealer(dealer);
    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.DeleteDealer(id);
}