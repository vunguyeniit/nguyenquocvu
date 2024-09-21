using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;
namespace SoKHCNVTAPI.Entities;

[Table("CanBo", Schema = "skhcn")]
public class CanBo
{
    public long Id { get; set; }
    
    [StringLength(50)]
    public required string Ma { get; set; }
    public short? TrangThai { get; set; } = 1;
    public DateTime NgayThamGia { get; set; }  
    [StringLength(350)]
    public string? MoTa { get; set; }
    [StringLength(250)] public required string HoVaTen { get; set; }
    [StringLength(250)] public string? NgaySinh { get; set; }
    [StringLength(250)]public string? QuocTich { get; set; }
    //B?t bu?c
    [StringLength(12)]public required string CCCD { get; set; }
    [StringLength(250)]public string? NoiOHienNay { get; set; }
    [StringLength(250)]public string? TinhThanh { get; set; }
    [StringLength(15)]public required string DienThoai { get; set; } 
    [StringLength(250)] public string? Email { get; set; }
    [StringLength(250)] public string? ChucDanhNgheNghiep { get; set; }
    [StringLength(250)]public string? ChucDanh { get; set; }
    [StringLength(250)] public string? NamDatHocVi { get; set; }
    [StringLength(250)] public string? NamPhongChucDanh { get; set; }
    [StringLength(250)] public string? HocVi { get; set; }
    [StringLength(250)] public string? CoQuanCongTac { get; set; }
    [StringLength(250)] public string? LinhVucNC { get; set; }
    public short GioiTinh { get; set; } = 1;
    public DateTime? NgayTao { get; set; } = DateTime.Now;
}


public class CanBoDto
{
    [StringLength(50)]
    public required string Ma { get; set; }

    public short? TrangThai { get; set; } = 1;

    public DateTime NgayThamGia { get; set; }

    [StringLength(350)]
    public string? MoTa { get; set; }

    [StringLength(250)] public string? HoVaTen { get; set; }

    [StringLength(250)] public string? NgaySinh { get; set; }

    [StringLength(250)] public string? QuocTich { get; set; }

    [StringLength(12)] public required string CCCD { get; set; }

    [StringLength(250)] public string? NoiOHienNay { get; set; }
    [StringLength(250)] public string? TinhThanh { get; set; }

    [StringLength(15)] public required string DienThoai { get; set; }//
    [StringLength(250)] public string? Email { get; set; }//
    [StringLength(250)] public string? ChucDanhNgheNghiep { get; set; }
    [StringLength(250)] public string? ChucDanh { get; set; }
    [StringLength(250)] public string? NamDatHocVi { get; set; }
    [StringLength(250)] public string? NamPhongChucDanh { get; set; }
    [StringLength(250)] public string? HocVi { get; set; }
    [StringLength(250)] public string? CoQuanCongTac { get; set; }
    [StringLength(250)] public string? LinhVucNC { get; set; }

    public short GioiTinh { get; set; } = 1;//
}

public class CanBoFilter : PaginationDto, IKeyword
{
    public string? Ma { get; set; }
    public string? HoVaTen { get; set; }
    public string? HocVi { get; set; }
    public string? LinhVucNC { get; set; }
    public  string? DienThoai { get; set; }
    public string? CoQuanCongTac { get; set; }
    public string? Email { get; set; }

    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? Keyword { get; set; }
}

public class CanBoResponse
{
    public long? Id { get; set; }
    public string? Ma { get; set; }
    public DateTime? NguoiThamGia { get; set; }
    public string? MoTa { get; set; }
    public ToChuc? ToChuc { get; set; }
    public TrinhDoDaoTao? TrinhDoDaoTao { get; set; }
    public long? CategoryId { get; set; }
    public IEnumerable<LinhVucNghienCuu>? LinhVucNghienCuus { get; set; }
    public string? NgaySinh { get; set; }
    public byte? GioiTinh { get; set; } = 1;
    public string? HoVaTen { get; set; }
    public string? DienThoai { get; set; }
    public string? Email { get; set; }
    public string? QuocTich { get; set; }
    public string? CCCD { get; set; }
    public string? NoiOHienNay { get; set; }
    public string? TinhThanh { get; set; }
    public string? ChucDanhNgheNghiep { get; set; }
    public string? ChucDanh { get; set; }
    public string? NamDatHocVi { get; set; }
    public string? NamPhongChucDanh { get; set; }
    public string? HocVi { get; set; }
    public string? CoQuanCongTac { get; set; }
    public string? LinhVucNC { get; set; }
    public DateTime? NgayTao { get; set; } = DateTime.Now;
}

//OfficerEducations
[Table("HocVanCanBo", Schema = "skhcn")]
public class HocVanCanBo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    //public required long OfficerId { get; set; }
    public required long MaCanBo { get; set; }

    [StringLength(350)] public required string BacDaoTao { get; set; }

    [StringLength(350)] public string? TrinhDoDaoTao { get; set; }

    [StringLength(250)] public string? NoiDaoTao { get; set; }

    [StringLength(250)] public string? ChuyenNganh { get; set; }

    [StringLength(250)] public string? ChucDanhKHCN { get; set; }
    [StringLength(4)] public string? NamTotNghiep { get; set; }

    public DateTime? NgayTao { get; set; } = DateTime.Now;

    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

    //
}

[Table("CanBoCongTac", Schema = "skhcn")]
public class CanBoCongTac
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    //public required long OfficerId { get; set; }
    public required long MaCanBo { get; set; }

    [StringLength(250)] public required string ThoiGianCongTac { get; set; }

    [StringLength(250)] public string? ViTriCongTac { get; set; }

    [StringLength(250)] public string? ToChucCongTac { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }
}
//OfficerMissions

public class CanBoCongTacFilter : PaginationDto, IKeyword
{
    public long MaCanBo { get; set; } = 0;
    public string? ThoiGianCongTac { get; set; }
    public string? ViTriCongTac { get; set; }
    public string? ToChucCongTac { get; set; }
    public string? Keyword { get; set; }
}


[Table("NhiemVuCanBo", Schema = "skhcn")]
public class NhiemVuCanBo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    //public required long OfficerId { get; set; }
    public long? MaCanBo { get; set; }

    [StringLength(250)] public required string TenNhiemVu { get; set; }

    [StringLength(250)] public required string VaiTroThamGia { get; set; }

    [StringLength(4)] public string? NamBatDau { get; set; }

    [StringLength(4)] public string? NamKetThuc { get; set; }

    public DateTime? NgayTao { get; set; }
}

public class NhiemVuCanBoFilter : PaginationDto, IKeyword
{
    public long MaCanBo { get; set; } = 0;

    public  string? TenNhiemVu { get; set; }

    public  string? VaiTroThamGia { get; set; }

     public string? NamBatDau { get; set; }

     public string? NamKetThuc { get; set; }

    public string? Keyword { get; set; }
}

[Table("LichSuCongTac", Schema = "skhcn")]
public class WorkHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    public required long MaCanBo { get; set; }
    public string? ThoiGian { get; set; }
    public string? ViTri { get; set; }
    public string? ToChuc { get; set; }

    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

//OfficerPublications
[Table("CongBoKHCN", Schema = "skhcn")]
public class CongBoKHCN
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    //public  long? OfficerId { get; set; }
    public  long? MaCanBo { get; set; }
    public  string? LoaiCongBo { get; set; }
    public  string? TenCongBo { get; set; }
    public  string? NguonTrich { get; set; }
    public  string? ISBN { get; set; }
    public  string? URL { get; set; }
    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();
}

public class CongBoKHCNFilter : PaginationDto, IKeyword
{
    public long MaCanBo { get; set; } = 0;
    public string? LoaiCongBo { get; set; }
    public string? TenCongBo { get; set; }
    public string? NguonTrich { get; set; }
    public string? ISBN { get; set; }
    public string? URL { get; set; }
    public string? Keyword { get; set; }
}