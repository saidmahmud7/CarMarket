﻿namespace Domain;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string LogoUrl { get; set; }
    public string Description { get; set; }
    public List<Car> Cars { get; set; }
}