using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class UserUpdateDto
{
    [StringLength(60, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; } = string.Empty;
    
    [StringLength(15, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    public string? Phone { get; set; } = string.Empty;
    
    [StringLength(30, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Password { get ; set; }
    
    [StringLength(20, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? NationalId { get; set; }

    [StringLength(100, ErrorMessage = "{0} không vượt quá {2} ký tự")] 
    public string? FullName { get; set; }

    [StringLength(300, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Address { get; set; }  

    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Province { get; set; }

    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? District { get; set; }

    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Ward { get; set; }

    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Role { get; set; }

    [StringLength(1, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Position { get; set; }

    [StringLength(500, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Remark { get; set; }
    
    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public required string Code { get; set; }

    public long? GroupId { get; set; }

    public short? Status { get; set; } = 1;

    public string? Menus { get; set; } = "";
}

public class UserUpdatePasswordDto
{
    [StringLength(30, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public required string Password { get; set; }
}