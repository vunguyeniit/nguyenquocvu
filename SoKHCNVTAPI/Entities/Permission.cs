using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("Permissions", Schema = "skhcn")]
public class Permission : BaseEntity
{
    public required long RoleId { get; set; }

    public required long UserId { get; set; }

    //public virtual Role? Role { get; set; }
    public long? GroupId { get; set; } = 0;

}