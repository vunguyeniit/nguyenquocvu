namespace SoKHCNVTAPI.Models;

public class ToChucDto
{
    public required string Ma { get; set; }

    public long NguoiTao { get; set; }

    public long NguoiCapNhat { get; set; }

    public short? TrangThai { get; set; } = 1;

    public DateTime? NgayTao { get; set; } = DateTime.UtcNow;

    public DateTime? NgayCapNhat { get; set; } = DateTime.UtcNow;

    public required string TenToChuc { get; set; }

    public string? MoTa { get; set; }

    public string? TenTiengAnh { get; set; }

    public required string DienThoai { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? Website { get; set; }

    public string? CoQuanChuQuan { get; set; }

    public string? TinhThanh { get; set; }

    public string? NguoiDungDau { get; set; }
    public string? SangKien { get; set; }
    
    public string? DSDonViTrucThuoc { get; set; }

    public long HinhThucSoHuu { get; set; } //OwnershipFormId

    public string? LoaiHinhToChuc { get; set; } // 

    public string? CoQuanquanlyTrucTiep { get; set; }

    public string? LinhVucNC { get; set; }

    public long OrganizationIdentifierId { get; set; } // Định danh tổ chức

    // Tham quyen thanh lap ID
    public short? ChuyenDoiCoCheTuChu { get; set; } = 1; // Enum TrucThuoc

    public string? TongKinhPhi { get; set; }
    public string? KinhPhiNSNN { get; set; }
    public string? KinhPhiNgoaiNSNN { get; set; }

    public string? TongDienTichTruSo { get; set; }
}

public class ToChucFilter : PaginationDto
{
    public short? TrangThai { get; set; }
    public string? TenToChuc { get; set; }
    public string? LoaiHinhToChuc { get; set; }
    public string? LinhVucNC { get; set; }
    public string? TenTiengAnh { get; set; }
    public string? TinhThanh { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? Ma { get; set; }
    public string? Keyword { get; set; }
    public string? NgayCapNhat { get; set; }
}

public class OrganizationPartnerDto
{
    public required string Name { get; set; }

    public short Type { get; set; } = 1;

    public string? Roles { get; set; }

    public long OrganizationId { get; set; }
}

public class NhanSuToChucDto
{
    public required long MaToChuc { get; set; } = 0;
    public short? TongNhanLuc { get; set; } = 0;

    public short? GiaoSu { get; set; } = 0;

    public short? PhoGiaoSu { get; set; } = 0;

    public short? TienSi { get; set; } = 0;

    public short? ThacSi { get; set; } = 0;

    public short DaiHoc { get; set; } = 0;

    public short CaoDang { get; set; } = 0;
    public short? NhanLucKhac { get; set; } = 0;

    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

    public DateTime? ThoiGian { get; set; } = DateTime.Now;
}