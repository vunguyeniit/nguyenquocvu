using AutoMapper;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

using QuocGia = SoKHCNVTAPI.Entities.CommonCategories.QuocGia;


namespace SoKHCNVTAPI.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        // Department
        CreateMap<DonViDto, DonVi>();

        // Title
        CreateMap<TitleDto, Title>();

        // ResearchField
        CreateMap<LinhVucNghienCuuDto, LinhVucNghienCuu>();
        // ResearchField
        CreateMap<LinhVucNghienCuu, LinhVucNghienCuuResponse>();
        

        // Organization Identifier
        CreateMap<OrganizationIdentifierDto, DinhDanhToChuc>();

        // Organization Type
        //CreateMap<OrganizationTypeDto, OrganizationType>();

        // Expert Identifier
        CreateMap<ExpertIdentifierDto, DinhDanhChuyenGia>();
        CreateMap<ExpertDto, ChuyenGia>();

        // Mission Identifier
        CreateMap<MissionIdentifierDto, DinhDanhNhiemVu>();

        // File Status
        CreateMap<FileStatusDto, TrangThaiHoSo>();

        // Ownership Form
        CreateMap<HinhThucSoHuuDto, HinhThucSoHuu>();

        // Degree
        CreateMap<DegreeDto, Degree>();

        // Country
        CreateMap<CountryDto, QuocGia>();

        // Education Level
        CreateMap<TrinhDoDaoTaoDto, TrinhDoDaoTao>();

        // Participation Role
        CreateMap<VaiTroThamGiaDto, VaiTroThamGia>();

        // Publication Type
        CreateMap<LoaiCongBoDto, LoaiCongBo>();

        // Mission Level
        CreateMap<MissionLevelDto, CapDoNhiemVu>();

        // Mission Type
        CreateMap<MissionTypeDto, LoaiNhiemVu>();

        // Mission Status
        CreateMap<TrangThaiNhiemVuDto, TrangThaiNhiemVu>();

        // Project Type
        CreateMap<LoaiDuAnDto, LoaiDuAn>();

        // Officer Identifier
        CreateMap<DinhDanhCanBoDto, DinhDanhCanBo>();

        // Province
        CreateMap<ProvinceDto, TinhThanh>();

        // District
        CreateMap<DistrictDto, District>();

        // Ward
        CreateMap<WardDto, Ward>();

        // Organization
        CreateMap<ToChucDto, ToChuc>();
        CreateMap<ToChuc, ToChucResponse>();

        CreateMap<NhanSuToChucDto, NhanSuToChuc>();
        CreateMap<OrganizationPartnerDto, DoiTacToChuc>();

        // Mission
        CreateMap<MissionInformationDto, NhiemVu>();
        CreateMap<MissionProcessingDto, NhiemVu>();
        CreateMap<MissionResultDto, NhiemVu>();
        CreateMap<MissionApplicationDto, NhiemVu>();
        CreateMap<NhiemVu, NhiemVuResponse>();
        CreateMap<NhiemVuDto, NhiemVu>();

        // Company
        CreateMap<DoanhNghiepDto, DoanhNghiep>();

        // Officer
        CreateMap<CanBoDto, CanBo>();

        CreateMap<CanBo, CanBoResponse>();

        CreateMap<HocVanCanBoDto, HocVanCanBo>();
        CreateMap<HocVanCanBo, HocVanCanBoResponse>();

        CreateMap<NhiemVuCanBoDto, NhiemVuCanBo>();
        CreateMap<NhiemVuCanBo, NhiemVuCanBoResponse>();

        CreateMap<CanBoCongTacDto, CanBoCongTac>();
        CreateMap<CanBoCongTac, CanBoCongTacResponse>();

        CreateMap<KQHDToChucDto, KQHDToChuc>();
        CreateMap<XQuangDto, XQuang>();
        CreateMap<BucXaDto, BucXa>();

        CreateMap<WorkHistoryDto, WorkHistory>();

        CreateMap<CongBoKHCNDto, CongBoKHCN>();
        CreateMap<ThongKeDto,ThongKe>();

        // ActivityLog
        CreateMap<ActivityLogDto, ActivityLog>();

        //Module
        CreateMap<ModuleModel, Module>();
        CreateMap<SoHuuTriTueDto, SoHuuTriTue>();
        CreateMap<AnToanDto, AnToan>();
        CreateMap<ThongTinDto, ThongTin>();
        CreateMap<LGSPDto, ThongTin>();
        //CreateMap<StepActionModel, StepAction>();
        //CreateMap<StepUserModel, StepUser>();
        //CreateMap<StepStatusModel, StepStatus>();
        //CreateMap<StepHasStatusModel, StepHasStatus>();


        // Role & PerMission
        CreateMap<RoleModel, Role>();
        CreateMap<PermissionModel, Permission>();
       
        CreateMap<CongBoDto, CongBo>();
        CreateMap<AnToanDto, AnToan>();

        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();
        CreateMap<User, UserResponse>();

        CreateMap<NhomDto, Nhom>().ReverseMap();

        // Workflow & Step
        CreateMap<WorkflowCategoryModel, WorkflowCategory>();
        CreateMap<WorkflowModel, Workflow>();
        CreateMap<WorkflowStepTemplateModel, WorkflowTemplateStep>();
        CreateMap<WorkflowStepModel, WorkflowStep>();
        CreateMap<WorkflowTemplateModel, WorkflowTemplate>();

        CreateMap<WorkflowStepLogModel, WorkflowStepLog>();

        CreateMap<Workflow, WorkflowDto>();
        CreateMap<DuAnModel, DuAn>();
        CreateMap<GiaoDienModel, GiaoDien>();

        CreateMap<ThamQuyenThanhLapDto, ThamQuyenThanhLap>();
        CreateMap<DoiTacToChucDto, DoiTacToChuc>();
        CreateMap<DoTuoiDto, DoTuoi>();
        CreateMap<HinhThucChuyenGiaoDto, HinhThucChuyenGiao>();
        CreateMap<HinhThucThanhLapDto, HinhThucThanhLap>();
        CreateMap<LoaiHinhToChucDNDto, LoaiHinhToChucDN>();
        CreateMap<LoaiHinhToChucDto, LoaiHinhToChuc>();
        CreateMap<LinhVucKHCNDto, LinhVucKHCN>();
        CreateMap<MucTieuKTXHDto, MucTieuKTXH>();
        CreateMap<QuyChuanKTDto, QuyChuanKT>();
        CreateMap<NguonCapKinhPhiDto, NguonCapKinhPhi>();
        CreateMap<HinhThucHTQTDto, HinhThucHTQT>();
        CreateMap<LinhVucDaoTaoKHCNDto, LinhVucDaoTaoKHCN>();
        CreateMap<LinhVucNCHTQTDto, LinhVucNCHTQT>();
        CreateMap<LinhVucUngDungDto, LinhVucUngDung>();
        CreateMap<MauPhuongTienDoDto, MauPhuongTienDo>();
        CreateMap<NguonKPHTQTDto, NguonKPHTQT>();
        CreateMap<DoiTacQTDto, DoiTacQT>();
        CreateMap<ChucDanhDto, ChucDanh>();
        CreateMap<QuocTichDto, QuocTich>();
        CreateMap<TrinhDoChuyenMonDto, TrinhDoChuyenMon>();
        CreateMap<LoaiHinhNhiemVuDto, LoaiHinhNhiemVu>();
        CreateMap<CapQuanLyDto, CapQuanLy>();
        CreateMap<LinhVucNghienCuu, LinhVucNghienCuu>();
        CreateMap<CapQuanLyHTQTDto, CapQuanLyHTQT>();
        CreateMap<LinhVucNghienCuuTTQTDto, LinhVucNghienCuuTTQT>();
        CreateMap<NguonCapKPEnumDto, NguonCapKPEnum>();
        CreateMap<LoaiHinhToChucDto, LoaiHinhToChuc>();

        CreateMap<BieuDoMauDto, BieuDoMau>();
        CreateMap<BieuDoDto, BieuDo>();
        CreateMap<DuLieuBieuDoDto, DuLieuBieuDo>();

        CreateMap<TieuChuanDto, TieuChuan>();
        CreateMap<ActivityLogUserDto, ActivityLogUser>();
    }
}