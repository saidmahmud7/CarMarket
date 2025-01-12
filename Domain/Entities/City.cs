namespace Domain;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public List<Dealer> Dealers { get; set; }
}