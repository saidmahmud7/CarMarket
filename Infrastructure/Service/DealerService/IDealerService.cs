using Domain.DTO_s.DealerDto;
using Domain.Filters;
using Infrastructure.Response;

namespace Infrastructure.Service.DealerService;

public interface IDealerService
{
    Task<PaginationResponse<List<GetDealerDto>>> GetAll(DealerFilter filter);
    Task<PaginationResponse<List<GetDealerDto>>> GetDealerWithCity(DealerFilter filter);
    Task<ApiResponse<GetDealerDto>> GetDealerById(int id);
    Task<ApiResponse<string>> AddDealer(CreateDealerDto dealer);
    Task<ApiResponse<string>> UpdateDealer(UpdateDealerDto dealer);
    Task<ApiResponse<string>> DeleteDealer(int id);
}