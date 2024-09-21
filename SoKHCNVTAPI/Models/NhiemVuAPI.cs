using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class NhiemVuAPI
{
    public long Id { get; set; }
    public string? tennhiemvu { get; set; }
    public long? madinhdanhnhiemvuId { get; set; }
    public virtual DinhDanhNhiemVu? madinhdanhnhiemvu { get; set; }
    public long? capnhiemvuId { get; set; }
    public virtual CapDoNhiemVu? capnhiemvu { get; set; }
    public long? tochucchutriId { get; set; }
    public virtual ToChuc? tochucchutri { get; set; }
    public long? linhvucId { get; set; }
    public virtual LinhVucNghienCuu? linhvuc { get; set; }
    public long? loaihinhnhiemvu { get; set; }
    public int? thoigianthuchien { get; set; }
    public DateTime? thoigianbatdau { get; set; }
    public DateTime? thoigianketthuc { get; set; }
    public Decimal? tongkinhphi { get; set; }
    public short? trangthai { get; set; }

}