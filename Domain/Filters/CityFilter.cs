namespace Domain.Filters;

public record CityFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Region { get; set; }
    public string? Country { get; set; } 
}