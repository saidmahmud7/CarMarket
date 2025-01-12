using System.Net;
using AutoMapper;
using Domain;
using Domain.DTO_s.CityDto;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.CityService;

public class CityService(DataContext context, IMapper mapper) : ICityService
{
    public async Task<PaginationResponse<List<GetCityDto>>> GetAll(CityFilter filter)
    {
        IQueryable<City> cities = context.Cities;
        if (!string.IsNullOrEmpty(filter.Name))
            cities = cities.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Region))
            cities = cities.Where(x => x.Region.ToLower().Contains(filter.Region.ToLower()));
        if (!string.IsNullOrEmpty(filter.Country))
            cities = cities.Where(x => x.Country.ToLower().Contains(filter.Country.ToLower()));
        int total = await cities.CountAsync();
        var result = await cities
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => mapper.Map<GetCityDto>(x))
            .ToListAsync();
        return new PaginationResponse<List<GetCityDto>>(filter.PageSize, filter.PageNumber, total, result);
    }

    public async Task<PaginationResponse<List<GetCityWithDealerDto>>> GetCityWithDealer(CityFilter filter)
    {
        IQueryable<City> cities = context.Cities.Include(x=>x.Dealers);
        if (!string.IsNullOrEmpty(filter.Name))
            cities = cities.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Region))
            cities = cities.Where(x => x.Region.ToLower().Contains(filter.Region.ToLower()));
        if (!string.IsNullOrEmpty(filter.Country))
            cities = cities.Where(x => x.Country.ToLower().Contains(filter.Country.ToLower()));
        int total = await cities.CountAsync();
        var result = await cities
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => mapper.Map<GetCityWithDealerDto>(x))
            .ToListAsync();
        return new PaginationResponse<List<GetCityWithDealerDto>>(filter.PageSize, filter.PageNumber, total, result);
        
    }

    public async Task<ApiResponse<GetCityDto>> GetCityById(int id)
    {
        var cities = await context.Cities.FirstOrDefaultAsync(x => x.Id == id);
        var citiesDto = mapper.Map<GetCityDto>(cities);
        return new ApiResponse<GetCityDto>(citiesDto);
    }

    public async Task<ApiResponse<string>> AddCity(CreateCityDto city)
    {
        var cities = mapper.Map<City>(city);
        await context.Cities.AddAsync(cities);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "City created");
    }

    public async Task<ApiResponse<string>> UpdateCity(UpdateCityDto city)
    {
        var existingCity = await context.Cities.FirstOrDefaultAsync(x => x.Id == city.Id);
        if (existingCity == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "City Not Found");
        existingCity.Id = city.Id;
        existingCity.Name = city.Name;
        existingCity.Region = city.Region;
        existingCity.Country = city.Country;
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "City updated");
    }

    public async Task<ApiResponse<string>> DeleteCity(int id)
    {
        var existingCity = await context.Cities.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCity == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "City Not Found");
        context.Cities.Remove(existingCity);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "City deleted");
    }
}