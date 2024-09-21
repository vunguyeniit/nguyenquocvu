using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class ResetPasswordDto
{
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [Required(ErrorMessage = "{0} là bắt buộc")]
    [StringLength(50, ErrorMessage = "{0} không vượt quá {2} ký tự")]
    public required string Username { get; set; }
}