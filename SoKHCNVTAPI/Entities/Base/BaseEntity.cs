using SoKHCNVTAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Entities.Base;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; init; }
    public short? Status { get; set; } = 1;
    //public bool? IsDeleted { get; set; } = false;
    public DateTime? CreatedAt { get; set; } = Utils.getCurrentDate();
    public DateTime? UpdatedAt { get; set; } = Utils.getCurrentDate();

}