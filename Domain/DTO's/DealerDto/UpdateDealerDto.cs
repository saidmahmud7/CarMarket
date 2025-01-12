namespace Domain.DTO_s.DealerDto;

public class UpdateDealerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal Rating { get; set; }
    public int CityId { get; set; }
}