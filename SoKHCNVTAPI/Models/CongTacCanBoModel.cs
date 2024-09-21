using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class CanBoCongTacDto
{
    public long? MaCanBo { get; set; }

    [StringLength(250)]
    public required string ThoiGianCongTac { get; set; }
    
    [StringLength(250)]
    public string? ViTriCongTac { get; set; }
    
    [StringLength(250)]
    public string? ToChucCongTac { get; set; }

}

public class CanBoCongTacResponse
{
    public long? Id { get; set; }

    public  long? MaCanBo { get; set; }
    public string? ThoiGianCongTac { get; set; }
    public string? ViTriCongTac { get; set; }
    public string? ToChucCongTac { get; set; }
    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }
  

}