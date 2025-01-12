using System.Net;
using AutoMapper;
using Domain;
using Domain.DTO_s.BrandDto;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.BrandService;

public class BrandService(DataContext context, IMapper mapper) : IBrandService
{
    public async Task<PaginationResponse<List<GetBrandDto>>> GetAll(BrandFilter filter)
    {
        IQueryable<Brand> brands = context.Brands;
        if (!string.IsNullOrEmpty(filter.Name))
            brands = brands.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Country))
            brands = brands.Where(x => x.Country.ToLower().Contains(filter.Country.ToLower()));
        if (!string.IsNullOrEmpty(filter.LogoUrl))
            brands = brands.Where(x => x.LogoUrl.ToLower().Contains(filter.LogoUrl.ToLower()));
        if (!string.IsNullOrEmpty(filter.Description))
            brands = brands.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        int total = await brands.CountAsync();
        var result = await brands
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => mapper.Map<GetBrandDto>(x))
            .ToListAsync();
        return new PaginationResponse<List<GetBrandDto>>(filter.PageSize, filter.PageNumber, total, result);
    }

    public async Task<PaginationResponse<List<GetBrandWithCarsDto>>> GetBrandWithCars(BrandFilter filter)
    {
        IQueryable<Brand> brands = context.Brands.Include(x=>x.Cars);
        if (!string.IsNullOrEmpty(filter.Name))
            brands = brands.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Country))
            brands = brands.Where(x => x.Country.ToLower().Contains(filter.Country.ToLower()));
        if (!string.IsNullOrEmpty(filter.LogoUrl))
            brands = brands.Where(x => x.LogoUrl.ToLower().Contains(filter.LogoUrl.ToLower()));
        if (!string.IsNullOrEmpty(filter.Description))
            brands = brands.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        int total = await brands.CountAsync();
        var result = await brands
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => mapper.Map<GetBrandWithCarsDto>(x))
            .ToListAsync();
        return new PaginationResponse<List<GetBrandWithCarsDto>>(filter.PageSize, filter.PageNumber, total, result);    }

    public async Task<ApiResponse<GetBrandDto>> GetBrandById(int id)
    {
        var brands = await context.Brands.FirstOrDefaultAsync(x => x.Id == id);
        var brandsDto = mapper.Map<GetBrandDto>(brands);
        return new ApiResponse<GetBrandDto>(brandsDto);
    }

    public async Task<ApiResponse<string>> AddBrand(CreateBrandDto brand)
    {
        var brands = mapper.Map<Brand>(brand);
        await context.Brands.AddAsync(brands);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "Brand created");
    }

    public async Task<ApiResponse<string>> UpdateBrand(UpdateBrandDto brand)
    {
        var existingBrand = await context.Brands.FirstOrDefaultAsync(x => x.Id == brand.Id);
        if (existingBrand == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Brand Not Found");

        existingBrand.Id = brand.Id;
        existingBrand.Name = brand.Name;
        existingBrand.Country = brand.Country;
        existingBrand.LogoUrl = brand.LogoUrl;
        existingBrand.Description = brand.Description;
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "Brand updated");
    }

    public async Task<ApiResponse<string>> DeleteBrand(int id)
    {
        var existingBrand = await context.Brands.FirstOrDefaultAsync(x => x.Id == id);
        if (existingBrand == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Brand Not Found");
        context.Brands.Remove(existingBrand);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.OK, "Brand Deleted");
    }
}