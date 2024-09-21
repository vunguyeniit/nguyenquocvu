
using SoKHCNVTAPI.Entities;

namespace SoKHCNVTAPI.Models;

public class UserResponse
{
    public long Id { get; set; }
    public string? NationalId { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? Ward { get; set; }
    public string? Role { get; set; }
    public string? Position { get; set; }
    public short Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public string? Remark { get; set; }
    public string? Email { get; set; }
    public string? Code { get; set; }
    public string? Phone { get; set; }
    public DateTime? LastLogin { get; set; }
    public long? GroupId { get; set; }

    public Nhom? Nhom { get; set; }

    public List<RoleModel>? roles {get; set; }
}


public class UserResponseAPP
{
    public string? MaQuocGia { get; set; }
    public string? HoTen { get; set; }
    public string? DiaChi { get; set; }
    public string? Tinh { get; set; }
    public string? Quan { get; set; }
    public string? Phuong { get; set; }
    public string? Quyen { get; set; }
    public string? ViTri { get; set; }
    public short TrangThai { get; set; }
    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }

    public string? Email { get; set; }
    public string? Ma { get; set; }
    public string? DienThoai { get; set; }
    public long? MaNhom { get; set; }

}