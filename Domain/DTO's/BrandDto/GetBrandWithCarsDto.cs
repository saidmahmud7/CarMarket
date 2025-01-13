using Domain.DTO_s.CarDto;

namespace Domain.DTO_s.BrandDto;

public class GetBrandWithCarsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string LogoUrl { get; set; }
    public string Description { get; set; }
    public List<GetCarDto> Cars { get; set; }

}