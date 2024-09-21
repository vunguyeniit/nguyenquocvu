using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class DoanhNghiepAPI
{
    [StringLength(250)] public long Id { get; set; }
    [StringLength(250)]public string? tendoanhnghiep { get; set; }
    [StringLength(250)]public string? tenviettat { get; set; }
    [StringLength(250)]public string? ngaythanhlap { get; set; }
    [StringLength(250)]public string? masothue { get; set; }
    [StringLength(250)]public string? dienthoai { get; set; }
    [StringLength(250)]public string? email { get; set; }
    [StringLength(250)] public string? website { get; set; }
    [StringLength(250)]public string? diachitrusochinh { get; set; }
    [StringLength(250)]public string? loaihinhtochuc { get; set; }
    [StringLength(250)] public string? linvucnc { get; set; }
    [StringLength(250)]public string? tinhthanh { get; set; }
    [StringLength(250)]public string? loaihinh { get; set; }


}