using Domain.DTO_s.DealerDto;
using Domain.Filters;

namespace Domain.DTO_s.CityDto;

public record GetCityWithDealerDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public List<GetDealerDto> Dealers { get; set; }

}