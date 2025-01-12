namespace Domain.DTO_s.BrandDto;

public class UpdateBrandDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string LogoUrl { get; set; }
    public string? Description { get; set; }
}