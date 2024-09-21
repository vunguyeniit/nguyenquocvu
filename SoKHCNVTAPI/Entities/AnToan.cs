using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("AnToan", Schema = "skhcn")]
public class AnToan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [StringLength(350)] public string? AnToanBucXa { get; set; }
    [StringLength(350)] public string? ThietBiXQuang { get; set; }
    [StringLength(350)] public string? ChungChiBucXa{get; set; }
    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }
}

public class AnToanDto
{
    public long Id { get; set; }
    [StringLength(350)] public string? AnToanBucXa { get; set; }
    [StringLength(350)] public string? ThietBiXQuang { get; set; }
    [StringLength(350)] public string? ChungChiBucXa { get; set; }
    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }
}

public class AnToanFilter : PaginationDto, IKeyword
{
    public long? Id { get; set; }
    public string? AnToanBucXa { get; set; }
    public string? Keyword { get; set; }
}