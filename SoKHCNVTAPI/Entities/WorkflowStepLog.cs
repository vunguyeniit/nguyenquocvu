using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

//buoc cua quy trinh
[Table("LSBuoc", Schema = "skhcn")]
public class WorkflowStepLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [StringLength(50)] public required string Ma { get; set; }

    [Column("MaBuoc")]
    public required long WorkflowStepId { get; set; } // Ma Quy tirnh

    [StringLength(500)] public string DinhKem { get; set; } = ""; //Tap tin dinh kem

    [StringLength(1000)] public string? NoiDung { get; set; } = "";

    public required long NguoiTao { get; set; } = 0; //Nguoi tao

    public string HoTen { get; set; } = "";

    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();

    public string MaChuKy { get; set; } = "";

    public string HanhDong { get; set; } = "Xem"; // Xem, Thêm, Cập Nhật, Xóa, Duyệt, Ký
}