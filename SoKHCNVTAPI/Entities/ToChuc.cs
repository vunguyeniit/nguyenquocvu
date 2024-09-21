using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Entities;

[Table("ToChuc", Schema = "skhcn")]
public class ToChuc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public required string Ma { get; set; }

    public long NguoiTao { get; set; }

    public long NguoiCapNhat { get; set; }

    public short? TrangThai { get; set; } = 1;

    //public bool? IsDeleted { get; set; } = false;

    //public DateTime? NgayTao { get; set; }

    //public DateTime? NgayCapNhat { get; set; }
    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;

    public  string? TenToChuc { get; set; }

    public string? MoTa { get; set; }

    public string? TenTiengAnh { get; set; }

    public required string DienThoai { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? Website { get; set; }

    public string? CoQuanChuQuan { get; set; }

    public string? TinhThanh { get; set; }

    public string? NguoiDungDau { get; set; }

    public string? DSDonViTrucThuoc { get; set; }

    public long HinhThucSoHuu { get; set; } //OwnershipFormId

    public string? LoaiHinhToChuc { get; set; } // 

    public string? CoQuanQuanLyTrucTiep { get; set; }

    public string? LinhVucNC { get; set; }
    
    public long OrganizationIdentifierId { get; set; } // Định danh tổ chức

    // Tham quyen thanh lap ID
    public short? ChuyenDoiCoCheTuChu { get; set; } = 1; // Enum TrucThuoc
                                                     

    public string? TongKinhPhi { get; set; }
    public string? KinhPhiNSNN { get; set; }
    public string? KinhPhiNgoaiNSNN { get; set; }

    public string? TongDienTichTruSo { get; set; }
}

public class ToChucResponse
{
    public required string Ma { get; set; }

    public long NguoiTao { get; set; }

    public long NguoiCapNhat { get; set; }

    public short? TrangThai { get; set; } = 1;

    //public bool? IsDeleted { get; set; } = false;

    public DateTimeOffset? NgayTao { get; set; }
    public DateTimeOffset? NgayCapNhat { get; set;}
    public string? TenToChuc { get; set; }

    public string? MoTa { get; set; }

    public string? TenTiengAnh { get; set; }

    public  string? DienThoai { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? Website { get; set; }

    public string? CoQuanChuQuan { get; set; }

    public string? TinhThanh { get; set; }

    public string? NguoiDungDau { get; set; }

    public string? DSDonViTrucThuoc { get; set; }

    public long HinhThucSoHuu { get; set; } //OwnershipFormId

    public virtual HinhThucSoHuu? _HinhThucSoHuu { get; set; } // 

    public string? LoaiHinhToChuc { get; set; } // 
    public virtual IEnumerable<LoaiHinhToChuc>? LoaiHinhToChucs { get; set; } // 

    public string? CoQuanQuanLyTrucTiep { get; set; }

    public string? LinhVucNC { get; set; }

    public long OrganizationIdentifierId { get; set; } // Định danh tổ chức

    // Tham quyen thanh lap ID
    public short? ChuyenDoiCoCheTuChu { get; set; } = 1; // Enum TrucThuoc

    public string? TongKinhPhi { get; set; }
    public string? KinhPhiNSNN { get; set; }
    public string? KinhPhiNgoaiNSNN { get; set; }
    public string? TongDienTichTruSo { get; set; }

    public IEnumerable<NhanSuToChuc>? NhanSuToChucs { get; set; }
    public IEnumerable<DoiTacToChuc>? DoiTacToChucs { get; set; }
    public IEnumerable<KQHDToChuc>? KetQuaDauTus { get; set; }
}