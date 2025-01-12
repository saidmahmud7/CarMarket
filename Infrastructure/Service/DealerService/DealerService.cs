using System.Net;
using AutoMapper;
using Domain;
using Domain.DTO_s.DealerDto;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.DealerService;

public class DealerService(DataContext context, IMapper mapper) : IDealerService
{
    public async Task<PaginationResponse<List<GetDealerDto>>> GetAll(DealerFilter filter)
    {
        IQueryable<Dealer> dealers = context.Dealers;

        if (!string.IsNullOrEmpty(filter.Name))
            dealers = dealers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Address))
            dealers = dealers.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
        if (!string.IsNullOrEmpty(filter.Phone))
            dealers = dealers.Where(x => x.Phone.ToLower().Contains(filter.Phone.ToLower()));
        if (!string.IsNullOrEmpty(filter.Email))
            dealers = dealers.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
        if (filter.Rating.HasValue)
            dealers = dealers.Where(x => x.Rating == filter.Rating);
        if (filter.CityId.HasValue)
            dealers = dealers.Where(x => x.CityId == filter.CityId);
        int total = await dealers.CountAsync();
        var result = await dealers
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => mapper.Map<GetDealerDto>(x))
            .ToListAsync();
        return new PaginationResponse<List<GetDealerDto>>(filter.PageSize, filter.PageNumber, total, result);
    }

    public async Task<PaginationResponse<List<GetDealerDto>>> GetDealerWithCity(DealerFilter filter)
    {
        IQueryable<Dealer> dealers = context.Dealers;
        if (filter.CityId.HasValue)
            dealers = dealers.Where(x => x.CityId == filter.CityId);
        int total = await dealers.CountAsync();
        var result = await dealers
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .GroupBy(x => x.CityId)
            .Select(x => mapper.Map<GetDealerDto>(x))
            .ToListAsync();
        return new PaginationResponse<List<GetDealerDto>>(filter.PageSize, filter.PageNumber, total, result);
    }

    public async Task<ApiResponse<GetDealerDto>> GetDealerById(int id)
    {
        var dealers = await context.Dealers.FirstOrDefaultAsync(x => x.Id == id);
        var dealerDto = mapper.Map<GetDealerDto>(dealers);
        return new ApiResponse<GetDealerDto>(dealerDto);
    }

    public async Task<ApiResponse<string>> AddDealer(CreateDealerDto dealer)
    {
        var dealers = mapper.Map<Dealer>(dealer);
        await context.Dealers.AddAsync(dealers);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "Dealer created");
    }

    public async Task<ApiResponse<string>> UpdateDealer(UpdateDealerDto dealer)
    {
        var existingDealer = await context.Dealers.FirstOrDefaultAsync(x => x.Id == dealer.Id);
        if (existingDealer == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Dealer not found");

        existingDealer.Id = dealer.Id;
        existingDealer.Name = dealer.Name;
        existingDealer.Address = dealer.Address;
        existingDealer.Phone = dealer.Phone;
        existingDealer.Email = dealer.Email;
        existingDealer.Rating = dealer.Rating;
        existingDealer.CityId = dealer.CityId;
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "Dealer updated");
    }

    public async Task<ApiResponse<string>> DeleteDealer(int id)
    {
        var existingDealer = await context.Dealers.FirstOrDefaultAsync(x => x.Id == id);
        if (existingDealer == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Dealer not found");

        context.Dealers.Remove(existingDealer);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "Dealer deleted");
    }
}