using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class MissionInformationDto
{
    [StringLength(500)] 
    public string? Name { get; set; }

    [StringLength(100)] 
    public string? MissionNumber { get; set; }

    public long? MissionLevelId { get; set; }
    public CapDoNhiemVu? MissionLevel { get; set; }

    [Required]
    public required long MissionIdentifyId { get; set; }
    public DinhDanhNhiemVu? MissionIdentifier { get; set; }
    public short? Status { get; set; }
    public long? OrganizationId { get; set; }
    public ToChuc? Organization { get; set; }

    public long? LinhVucNghienCuuId { get; set; }
    public LinhVucNghienCuu? LinhVucNghienCuu { get; set; }

    public long? ProjectTypeId { get; set; }
    public LoaiDuAn? ProjectType { get; set; }

    public int? TotalTimeWithMonth { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public decimal? TotalExpenses { get; set; }

    public string? TotalExpensesInWords { get; set; }

    public decimal? GovernmentExpenses { get; set; }

    public decimal? SelfExpenses { get; set; }

    public decimal? OtherExpenses { get; set; }

    public string? Keywords { get; set; }

    public string? CoQuanQuanLyKinhPhiNV { get; set; }
    public string? CanBo { get; set; } //1,2
    public long? LoaiHinhNhiemVu { get; set; }
    public long? ToChucChuTri { get; set; }
    public long? CoQuanCapTren { get; set; }
}

public class MissionProcessingDto
{
    public string? ResearchObjective { get; set; }

    public string? Summary { get; set; }

    public string? AnticipatedProduct { get; set; }

    [StringLength(200)] 
    public string? ApplicationScopeAddress { get; set; }

    public string? DecisionCode { get; set; }

    public DateTime? DecisionDate { get; set; }
}

public class MissionResultDto
{
    public DateTime? ReportingYear { get; set; }

    public string? ReportCode { get; set; }

    public DateTime? AcceptanceDate { get; set; }

    public string? ResearchRegistrationNumber { get; set; }

    public string? ConsolidatedFile { get; set; }

    public string? SummaryFile { get; set; }

    public string? ThongTinTomTat { get; set; }
}

public class MissionApplicationDto
{
    [StringLength(200)] 
    public string? Content { get; set; }

    [StringLength(200)] 
    public string? ApplicationAddress { get; set; }

    [StringLength(2000)] 
    public string? EconomicEfficiency { get; set; }
}

public class NhiemVuResponse
{
    // Information

    [StringLength(50)]
    public string? Code { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? MissionNumber { get; set; }

    public CapDoNhiemVu? MissionLevel { get; set; }
    
    public ToChuc? Organization { get; set; }
    
    public LinhVucNghienCuu? LinhVucNghienCuu { get; set; }
    
    public LoaiDuAn? ProjectType { get; set; }

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

    public string? Content { get; set; }
    
    public string? ApplicationAddress { get; set; }
    
    public string? EconomicEfficiency { get; set; }
    
    // Common
    public long? Id { get; init; }

    public short? Status { get; set; }
    
    public DateTime? CreatedAt { get; set; }
   
    public DateTime? UpdatedAt { get; set; }

    public string? CoQuanQuanLyKinhPhiNV { get; set; }
    public string? CanBo { get; set; } //1,2

    public IEnumerable<CanBo>? CanBos { get; set; }

    public long? LoaiHinhNhiemVu { get; set; }
    public long? ToChucChuTri { get; set; }
    public long? CoQuanCapTren { get; set; }
    public string? ThongTinTomTat { get; set; }
}


public class MissionSearchDto
{
    // Information
    
    [StringLength(50)]
    public string? Code { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? MissionNumber { get; set; }
    
    public DateTime? StartTime  { get; set; }
    
    public DateTime? EndTime  { get; set; }
    
    public decimal? TotalExpenses { get; set; }
    
    public decimal? GovernmentExpenses { get; set; }
    
    public decimal? SelfExpenses { get; set; }
    
    public decimal? OtherExpenses { get; set; }

    public string? Keywords { get; set; }
    
    // Processing
    
    public string? ResearchObjective { get; set; }
    
    public string? Summary { get; set; }
    
    public string? AnticipatedProduct { get; set; }
    
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

    public string? Content { get; set; }
    
    public string? ApplicationAddress { get; set; }
    
    public string? EconomicEfficiency { get; set; }
    
    // Common
    public long? Id { get; init; }

    public short? Status { get; set; }
    
    public DateTime? CreatedAt { get; set; }
   
    public DateTime? UpdatedAt { get; set; }
}