using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("NhiemVu", Schema = "skhcn")]
public class NhiemVu : BaseEntity
{
    public long Id { get; set; }
    // Information
    [StringLength(50)]
    public required string Code { get; set; }

    [StringLength(500)]
    public required string Name { get; set; }

    [StringLength(100)]
    public required string MissionNumber { get; set; }
    public long? MissionIdentifyId { get; set; }
    public virtual DinhDanhNhiemVu? MissionIdentify { get; set; }

    public long? MissionLevelId { get; set; }
    public virtual CapDoNhiemVu? MissionLevel { get; set; }
    
    public long? OrganizationId { get; set; }
    public virtual ToChuc? Organization { get; set; }
    
    public long? LinhVucNghienCuuId { get; set; }
    public virtual LinhVucNghienCuu? LinhVucNghienCuu { get; set; }
    
    public long? ProjectTypeId { get; set; }
    public virtual LoaiDuAn? ProjectType { get; set; }

    public int? TotalTimeWithMonth  { get; set; }
    
    public DateTime? StartTime  { get; set; }
    
    public DateTime? EndTime  { get; set; }
    
    public decimal? TotalExpenses { get; set; }
    
    public string? TotalExpensesInWords { get; set; }
    
    public decimal? GovernmentExpenses { get; set; }
    
    public decimal? SelfExpenses { get; set; }
    
    public decimal? OtherExpenses { get; set; }

    public string? Keywords { get; set; }
    
    // Processing
    
    public string? ResearchObjective { get; set; }
    
    public string? Summary { get; set; }
    
    public string? AnticipatedProduct { get; set; }
    
    [StringLength(200)]
    public string? ApplicationScopeAddress { get; set; }
    
    public string? DecisionCode { get; set; }
    
    public DateTime? DecisionDate { get; set; }

    // Result

    public DateTime? ReportingYear { get; set; }
    
    public string? ReportCode { get; set; }
    
    public DateTime? AcceptanceDate { get; set; }
    
    public string? ResearchRegistrationNumber { get; set; }
    
    public string? ConsolidatedFile { get; set; }
    
    public string? SummaryFile { get; set; }

    // Application

    [StringLength(200)]
    public string? Content { get; set; }
    
    [StringLength(200)]
    public string? ApplicationAddress { get; set; }
    
    [StringLength(2000)]
    public string? EconomicEfficiency { get; set; }


    public string? CoQuanQuanLyKinhPhiNV { get; set; }
    public string? CanBo { get; set; } //1,2
    [NotMapped]
    public virtual IEnumerable<CanBo>? CanBos { get; set; }
    public long? LoaiHinhNhiemVu { get; set; }
  
    public long? ToChucChuTri { get; set; }
    public long? CoQuanCapTren { get; set; }
    public string? ThongTinTomTat { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;

}

public class NhiemVuDto
{
    // Information
    [StringLength(50)]
    public string? Code { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? MissionNumber { get; set; }

    public long? MissionIdentifyId { get; set; }

    public long? MissionLevelId { get; set; }
    
    public long? OrganizationId { get; set; }
    public ToChuc? Organization { get; set; }
    
    //public long? ResearchFieldId { get; set; }
    
    public long? ProjectTypeId { get; set; }

    public int? TotalTimeWithMonth  { get; set; }
    
    public DateTime? StartTime  { get; set; }
    
    public DateTime? EndTime  { get; set; }
    
    public decimal? TotalExpenses { get; set; }
    
    public string? TotalExpensesInWords { get; set; }
    
    public decimal? GovernmentExpenses { get; set; }
    
    public decimal? SelfExpenses { get; set; }
    
    public decimal? OtherExpenses { get; set; }

    public string? Keywords { get; set; }
    
    // Processing
    
    public string? ResearchObjective { get; set; }
    
    public string? Summary { get; set; }
    
    public string? AnticipatedProduct { get; set; }
    
    [StringLength(200)]
    public string? ApplicationScopeAddress { get; set; }
    
    public string? DecisionCode { get; set; }
    
    public DateTime? DecisionDate { get; set; }

    // Result

    public DateTime? ReportingYear { get; set; }
    
    public string? ReportCode { get; set; }
    
    public DateTime? AcceptanceDate { get; set; }
    
    public string? ResearchRegistrationNumber { get; set; }
    
    public string? ConsolidatedFile { get; set; }
    
    public string? SummaryFile { get; set; }

    // Application

    [StringLength(200)]
    public string? Content { get; set; }
    
    [StringLength(200)]
    public string? ApplicationAddress { get; set; }
    
    [StringLength(2000)]
    public string? EconomicEfficiency { get; set; }

    public string? CoQuanQuanLyKinhPhiNV { get; set; }
    public string? CanBo { get; set; } //1,2
    public long? LoaiHinhNhiemVu { get; set; }
    public long? ToChucChuTri { get; set; }
    public long? CoQuanCapTren { get; set; }
    public string? ThongTinTomTat { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class NhiemVuFilter : PaginationDto, IKeyword
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? MissionNumber { get; set; }
    public string? AnticipatedProduct { get; set; }
    public short? Status { get; set; }
    public string? Keyword { get; set; }

}
