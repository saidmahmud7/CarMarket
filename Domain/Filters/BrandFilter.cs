namespace Domain.Filters;

public record BrandFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? LogoUrl { get; set; }
    public string? Description { get; set; }
}