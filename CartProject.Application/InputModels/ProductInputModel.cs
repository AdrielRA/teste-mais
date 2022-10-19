﻿using CartProject.Domain.Entities;

namespace CartProject.Application.InputModels;

public class ProductInputModel
{
    public string? Name { get; set; }
    public decimal Value { get; set; }

    public Product ToModel() => new()
    {
        Name = Name,
        Value = Value
    };
}
