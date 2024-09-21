using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class UserDto
{
    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    [Required(ErrorMessage = "{0} là bắt buộc")]
    public required string Code { get; set; }

    [StringLength(60, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    [Required(ErrorMessage = "{0} là bắt buộc")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; } = string.Empty;
    
    [StringLength(15, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    [Required(ErrorMessage = "{0} là bắt buộc")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    public string? Phone { get; set; } = string.Empty;
    
    [StringLength(30, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Password { get ; set; }
    
    [StringLength(20, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    [Required(ErrorMessage = "{0} là bắt buộc")]
    public required string NationalId { get; set; }

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

    [StringLength(100, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Position { get; set; }

    [StringLength(500, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public string? Remark { get; set; }
    public  int? LoginFailed { get; set; }
    public long? GroupId { get; set; }
}

public class BaseUserInfo
{
    public required long Id { get; set; }

    public required string Email { get; set; }

    public required string FullName { get; set; }

    public required string Code { get; set; }

    //public required string LastName { get; set; }

    public short? Status { get; set; }
}