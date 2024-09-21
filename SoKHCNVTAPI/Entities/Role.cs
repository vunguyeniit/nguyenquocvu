using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities;

[Table("Roles", Schema = "skhcn")]

public class Role : BaseEntity
{
    [StringLength(50)]
    public required string Module { get; set; }

    [StringLength(50)]
    public required string Code { get; set; }

    [StringLength(200)]
    public required string Description { get; set; }

    public required long CreatedBy { get; set; }

    public virtual required List<Permission> Permissions { get; set; }
}

public class RoleModuleDto
{
    [StringLength(50)]
    public required string Module { get; set; }

    public virtual required List<RoleDto> RoleDtos { get; set; }

    public virtual List<Menu>? Menus { get; set; }
}

public class RoleDto
{
    public required long Id { get; set; }

    public required string Code { get; set; }
}

public class RoleUserDto
{
    public required long UserId { get; set; }

    public virtual required List<string> RoleCodes { get; set; }
}