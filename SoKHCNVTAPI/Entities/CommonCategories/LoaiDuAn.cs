﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("ProjectTypes", Schema = "skhcn")]
public class LoaiDuAn : BaseEntity
{
    [StringLength(200)]
    public required string Name { get; set; }
    
    public required string Code { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class LoaiDuAnDto
{
    [StringLength(200)]
    public required string Name { get; set; }
    
    public required string Code { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public short? Status { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class LoaiDuAnFilter : PaginationDto, IKeyword
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public short? Status { get; set; }
    public string? Keyword { get; set; }
    public string? Description { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}