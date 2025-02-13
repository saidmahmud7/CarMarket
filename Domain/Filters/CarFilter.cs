﻿namespace Domain.Filters;

public record CarFilter : BaseFilter
{
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? Color { get; set; }
    public int? DealerId { get; set; }
    public int? BrandId { get; set; }
}