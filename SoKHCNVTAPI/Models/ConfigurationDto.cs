﻿namespace SoKHCNVTAPI.Models;

public class ConfigurationDto
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? Description { get; set; }
}