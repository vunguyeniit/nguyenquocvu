using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Helpers;


namespace SoKHCNVTAPI.Configurations;

public class DataContext : DbContext
{
    private readonly AppSettings _appSettings;

    public DataContext(DbContextOptions<DataContext> options, IOptions<AppSettings> appSettings) : base(options)
    {
        _appSettings = appSettings.Value;
    }

    #region CommonCategories

    public required DbSet<DonVi> Departments { get; set; }
    public required DbSet<Title> Titles { get; set; }
    public required DbSet<DinhDanhChuyenGia> ExpertIdentifiers { get; set; }
   // public required DbSet<ResearchField> ResearchFieldIds { get; set; }
    public required DbSet<DinhDanhNhiemVu> MissionIdentifiers { get; set; }
    //public required DbSet<OrganizationType> OrganizationTypes { get; set; }
    public required DbSet<DinhDanhToChuc> OrganizationIdentifiers { get; set; }
    public required DbSet<TrangThaiHoSo> FileStatuses { get; set; }
    public required DbSet<QuocGia> Countries { get; set; }
    public required DbSet<TrangThaiNhiemVu> MissionStatuses { get; set; }
    public required DbSet<TrinhDoDaoTao> EducationLevels { get; set; }
    public required DbSet<HinhThucSoHuu> HinhThucSoHuu { get; set; }
    public required DbSet<LoaiCongBo> PublicationTypes { get; set; }
    public required DbSet<DinhDanhCanBo> OfficerIdentifiers { get; set; }

    #endregion

    #region Address

    public required DbSet<TinhThanh> Provinces { get; set; }
    public required DbSet<District> Districts { get; set; }
    public required DbSet<Ward> Wards { get; set; }

    #endregion

    public required DbSet<User> Users { get; set; }
    public required DbSet<ActivityLog> ActivityLogs { get; set; }
    public required DbSet<DoanhNghiep> DoanhNghieps { get; set; }
    public required DbSet<Module> Modules { get; set; }
    public required DbSet<ThongTin> ThongTin { get; set; }
    public required DbSet<SoHuuTriTue> SoHuuTriTue { get; set; }
    public required DbSet<CongBo> CongBo { get; set; }
    //public required DbSet<StepStatus> StepStatus { get; set; }
    //public required DbSet<StepHasStatus> StepHasStatus { get; set; }
    //public required DbSet<StepUser> StepUser { get; set; }

    public required DbSet<Document> Documents { get; set; }
    public required DbSet<Menu> Menus { get; set; }
    public required DbSet<Nhom> Groups { get; set; }
    public required DbSet<ChuyenGia> Experts { get; set; }
    public required DbSet<ToChuc> ToChuc { get; set; }
    public required DbSet<DoiTacToChuc> DoiTacToChuc { get; set; }
    public required DbSet<NhanSuToChuc> NhanSuToChuc { get; set; }
      public required DbSet<AnToan> AnToan { get; set; }

    public required DbSet<LoaiDuAn> ProjectTypes { get; set; }
    public required DbSet<Degree> Degrees { get; set; }
    public required DbSet<VaiTroThamGia> ParticipationRoles { get; set; }
    public required DbSet<OwnershipRepresentative> OwnershipRepresentatives { get; set; }

    // Role & Permission
    public required DbSet<Role> Roles { get; set; }

    public required DbSet<Permission> Permissions { get; set; }
    //public required DbSet<RoleHasPermission> RoleHasPermissions { get; set; }


    #region Mission

    public required DbSet<LoaiNhiemVu> MissionTypes { get; set; }
    public required DbSet<TrangThaiNhiemVu> TaskStatus { get; set; }
    public required DbSet<CapDoNhiemVu> MissionLevels { get; set; }
    public required DbSet<NhiemVu> Missions { get; set; }

    #endregion

    #region Officer

    public required DbSet<CanBo> CanBo { get; set; }
    public required DbSet<HocVanCanBo> HocVanCanBos { get; set; }
    public required DbSet<NhiemVuCanBo> NhiemVuCanBos { get; set; }
    public required DbSet<CongBoKHCN> OfficerPublications { get; set; }

    public required DbSet<CanBoCongTac> CanBoCongTacs { get; set; }

    #endregion

    public required DbSet<KQHDToChuc> KQHDToChuc { get; set; }
    public required DbSet<WorkflowCategory> WorkflowCategories { get; set; }
    public required DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
    public required DbSet<DuAn> DuAns { get; set; }
    public required DbSet<Workflow> Workflows { get; set; }
    public required DbSet<WorkflowTemplateStep> WorkflowTemplateSteps { get; set; }
    public required DbSet<WorkflowStep> WorkflowSteps { get; set; }

    public required DbSet<WorkflowStepLog> WorkflowStepLogs { get; set; }

    public required DbSet<WorkHistory> WorkHistories { get; set; }

    public required DbSet<GiaoDien> GiaoDiens { get; set; }

    public required DbSet<ThamQuyenThanhLap> ThamQuyenThanhLap { get; set; }
    public required DbSet<DoTuoi> DoTuoi { get; set; }
    public required DbSet<HinhThucChuyenGiao> HinhThucChuyenGiao { get; set; }
    public required DbSet<HinhThucThanhLap> HinhThucThanhLap { get; set; }
    public required DbSet<LoaiHinhToChucDN> LoaiHinhToChucDN { get; set; }
    public required DbSet<LoaiHinhToChuc> LoaiHinhToChuc { get; set; }
    public required DbSet<LinhVucKHCN> LinhVucKHCN { get; set; }
    public required DbSet<MucTieuKTXH> MucTieuKTXH { get; set; }
    public required DbSet<QuyChuanKT> QuyChuanKT { get; set; }
    public required DbSet<NguonCapKinhPhi> NguonCapKinhPhi { get; set; }


    public required DbSet<HinhThucHTQT> HinhThucHTQT { get; set; }
    public required DbSet<LinhVucDaoTaoKHCN> LinhVucDaoTaoKHCN { get; set; }
    public required DbSet<LinhVucNCHTQT> LinhVucNCHTQT { get; set; }
    public required DbSet<LinhVucUngDung> LinhVucUngDung { get; set; }
    public required DbSet<MauPhuongTienDo> MauPhuongTienDo { get; set; }
    public required DbSet<NguonKPHTQT> NguonKPHTQT { get; set; }
    public required DbSet<DoiTacQT> DoiTacQT { get; set; }
    public required DbSet<ChucDanh> ChucDanh { get; set; }
    public required DbSet<QuocTich> QuocTich { get; set; }
    public required DbSet<TrinhDoChuyenMon> TrinhDoChuyenMon{ get; set; }
    public required DbSet<LGSP> lGSP { get; set; }
    public required DbSet<XQuang> XQuang { get; set; }
    public required DbSet<BucXa> BucXa { get; set; }
    public required DbSet<ThongKe> ThongKe { get; set; }
    public required DbSet<LoaiHinhNhiemVu> LoaiHinhNhiemVu { get; set; }
    public required DbSet<CapQuanLy> CapQuanLy { get; set; }
    public required DbSet<LinhVucNghienCuu> LinhVucNghienCuu { get; set; }

    public required DbSet<CapQuanLyHTQT> CapQuanLyHTQT { get; set; }
    public required DbSet<LinhVucNghienCuuTTQT> LinhVucNghienCuuTTQT { get; set; }
    public required DbSet<NguonCapKPEnum> NguonCapKPEnum { get; set; }

    public required DbSet<BieuDoMau> BieuDoMau { get; set; }
    public required DbSet<BieuDo> BieuDo { get; set; }
    public required DbSet<DuLieuBieuDo> DuLieuBieuDo { get; set; }
    public required DbSet<ActivityLogUser> ActivityLogUser { get; set; }


    //Thêm Tiêu chuẩn
    public required DbSet<TieuChuan> TieuChuan { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Permission

        //modelBuilder.Entity<Permission>().HasData(
        //    new Permission
        //    {
        //        Name = "manage role",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //    new Permission
        //    {
        //        Name = "create role",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //    new Permission
        //     {
        //         Name = "delete role",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //    new Permission
        //    {
        //          Name = "edit role",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //    new Permission
        //    {
        //           Name = "create organization",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //    new Permission
        //    {
        //            Name = "edit organization",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //     new Permission
        //     {
        //         Name = "delete organization",
        //         Status = 1,
        //         IsDeleted = false
        //     },
        //    new Permission
        //    {
        //        Name = "show organization",
        //        Status = 1,
        //        IsDeleted = false
        //    },
        //    new Permission
        //    {
        //        Name = "manage organization",
        //        Status = 1,
        //        IsDeleted = false
        //    }
        //    );

        #endregion

        #region Group

        modelBuilder.Entity<Nhom>().HasData(
            new Nhom
            {
                Id = 1,
                Code = "G1",
                Name = "Super Administrator",
                Description = "Group Super Administrator"
            },
            new Nhom
            {
                Id = 2,
                Code = "G2",
                Name = "Administrator",
                Description = "Group Administrator"
            });

        #endregion

        #region User

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Code = "KHCN0000",
                Password = PasswordBuilder.HashBCrypt("12345678"),
                NationalId = "1000000001",
                Phone = Utils.FormatPhoneNumber("0986435388"),
                Email = "ntcnet@gmail.com",
                Fullname = "Nguyen Công",
                GroupId = 1
            });

        #endregion
        
        #region Degree
        
        modelBuilder.Entity<Degree>().HasData(
            new Degree { Id = 1, Code="CD", Name = "Cao đẳng", Description = "Cao đẳng" },
            new Degree { Id = 2, Code="CN", Name = "Cử nhân", Description = "Cử nhân" },
            new Degree { Id = 3, Code="GS", Name = "Giáo sư", Description = "Giáo sư" },
            new Degree { Id = 4, Code="PGS", Name = "Phó giáo sư", Description = "Phó giáo sư" },
            new Degree { Id = 5, Code="TS", Name = "Tiến sĩ", Description = "Tiến sĩ" },
            new Degree { Id = 6, Code="THS", Name = "Thạc sĩ", Description = "Thạc sĩ" },
            new Degree { Id = 7, Code="KS", Name = "Kỹ sư", Description = "Kỹ sư" }
        );
        
        #endregion
        
        #region Department
        
        modelBuilder.Entity<DonVi>().HasData(
            new DonVi
            {
                Id = 1, 
                Code = "SKHVT",
                Name = "SỞ KHOA HỌC VÀ CÔNG NGHỆ TỈNH BÀ RỊA - VŨNG TÀU",
                Description = "SỞ KHOA HỌC VÀ CÔNG NGHỆ TỈNH BÀ RỊA - VŨNG TÀU"
            },
            new DonVi
            {
                Id = 2, Code = "TTUD", Name = "TRUNG TÂM THÔNG TIN VÀ ỨNG DỤNG KHOA HỌC & CÔNG NGHỆ TỈNH BÀ RỊA – VŨNG TÀU",
                Description = "TRUNG TÂM THÔNG TIN VÀ ỨNG DỤNG KHOA HỌC & CÔNG NGHỆ TỈNH BÀ RỊA – VŨNG TÀU"
            },
            new DonVi
            {
                Id = 3, Code = "TCDL", Name = "CHI CỤC TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG",
                Description = "CHI CỤC TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG"
            },
            new DonVi
            {
                Id = 4, Code = "TTDL", Name = "TRUNG TÂM KỸ THUẬT TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG",
                Description = "TRUNG TÂM KỸ THUẬT TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG"
            }
        );
        
        #endregion
        
        #region ExpertIdentifier
        
        modelBuilder.Entity<DinhDanhChuyenGia>().HasData(
            new DinhDanhChuyenGia
            {
                Id = 1, Code = "V.05.01.01", Name = "Nghiên cứu viên cao cấp (hạng I)",
                Description = "định danh chuyên gia"
            },
            new DinhDanhChuyenGia
            {
                Id = 2, Code = "V.05.01.02", Name = "Nghiên cứu viên chính (hạng II)",
                Description = "Nghiên cứu viên chính (hạng II)"
            },
            new DinhDanhChuyenGia
            {
                Id = 3, Code = "V.05.01.03", Name = "Nghiên cứu viên (hạng III)",
                Description = "Nghiên cứu viên (hạng III)"
            },
            new DinhDanhChuyenGia
            {
                Id = 4, Code = "V.05.01.04", Name = "Trợ lý nghiên cứu (hạng IV)",
                Description = "Trợ lý nghiên cứu (hạng IV)"
            },
            new DinhDanhChuyenGia
            {
                Id = 5, Code = "V.05.02.05", Name = "Kỹ sư cao cấp (hạng I)", Description = "Kỹ sư cao cấp (hạng I)"
            },
            new DinhDanhChuyenGia
                { Id = 6, Code = "V.05.02.06", Name = "Kỹ sư chính (hạng II)", Description = "Kỹ sư chính (hạng II)" },
            new DinhDanhChuyenGia
                { Id = 7, Code = "V.05.02.07", Name = "Kỹ sư (hạng III)", Description = "Kỹ sư (hạng III)" },
            new DinhDanhChuyenGia { Id = 8, Code = "V.05.02.08", Name = "Kỹ thuật viên", Description = "Kỹ thuật viên" }
        );
        
        #endregion
        
        #region MissionIdentifier
        
        modelBuilder.Entity<DinhDanhNhiemVu>().HasData(
            new DinhDanhNhiemVu
            {
                Id = 1, Code = "CT", Name = "CT",
                Description = "CT là ký hiệu chung cho các chương trình khoa học và công nghệ cấp Bộ."
            },
            new DinhDanhNhiemVu
            {
                Id = 2, Code = "DT", Name = "DT",
                Description = "ĐT là ký hiệu chung cho các đề tài khoa học và công nghệ cấp Bộ."
            },
            new DinhDanhNhiemVu
            {
                Id = 3, Code = "DA", Name = "DA",
                Description = "DA là ký hiệu chung cho các dự án khoa học và công nghệ cấp Bộ."
            },
            new DinhDanhNhiemVu
            {
                Id = 4, Code = "NVK", Name = "NVK",
                Description = "NVK là ký hiệu chung cho các nhiệm vụ khoa học và công nghệ cấp Bộ khác."
            }
        );
        
        #endregion
        
        #region PublicationType
        
        modelBuilder.Entity<LoaiCongBo>().HasData(
            new LoaiCongBo
            {
                Id = 1, Code="QT", Name = "Quốc tế",
                Description = "Quốc tế"
            },
            new LoaiCongBo
            {
                Id = 2, Code="TN", Name = "Trong nước",
                Description = "Trong nước"
            }
        );
        
        #endregion
        
        #region MissionStatus
        
        modelBuilder.Entity<TrangThaiNhiemVu>().HasData(
            new TrangThaiNhiemVu
            {
                Id = 1, Name = "Nháp", Description = "Nháp"
            },
            new TrangThaiNhiemVu
            {
                Id = 2, Name = "Trình duyệt", Description = "Trình duyệt"
            },
            new TrangThaiNhiemVu
            {
                Id = 3, Name = "Xác nhận", Description = "Xác nhận đã nhận nhiêm vụ"
            },
            new TrangThaiNhiemVu
            {
                Id = 4, Name = "Đang xử lý", Description = "Đang xử lý"
            },
            new TrangThaiNhiemVu
            {
                Id = 5, Name = "Hoàn thành", Description = "Hoàn thành"
            },
            new TrangThaiNhiemVu
            {
                Id = 6, Name = "Huỷ", Description = "Huỷ"
            }
        );
        
        #endregion

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _appSettings.ConnectionStrings.PostgresSql, p => p.MigrationsHistoryTable("__MigrationsHistory", "skhcn"));

        base.OnConfiguring(optionsBuilder);
    }
}