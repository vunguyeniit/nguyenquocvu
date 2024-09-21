using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Enums;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SoKHCNVTAPI.Helpers;
using System.Globalization;

namespace SoKHCNVTAPI.Repositories;

 public interface IWorkflowRepository
{
    Task<(IEnumerable<WorkflowCategory>?, int)> GetCategoryAsync(WorkflowCategoryFilter model);
    Task<WorkflowCategory> GetCategoryByIdAsync(long id);
    Task<WorkflowCategory> CreateCategoryAsync(WorkflowCategoryModel model, long createdBy);
    Task UpdateCategoryAsync(long id, WorkflowCategoryModel model, long updatedBy);
    Task<bool> DeleteCategoryAsync(long id, long deletedBy);

    Task<(IEnumerable<WorkflowTemplate>?, int)> GetTemplateAsync(WorkflowFilter model);
    Task<WorkflowTemplate> GetTemplateByIdAsync(long id);
    Task<WorkflowTemplate> CreateTemplateAsync(WorkflowTemplateModel model, long createdBy);
    Task UpdateTemplateRoleAndAction(long id, WorkflowTemplateModel model, long updatedBy);
    Task UpdateTemplateAsync(long id, WorkflowTemplateModel model, long updatedBy);
    Task<bool> DeleteTemplateAsync(long id, long deletedBy);

    // Quy trinh dư liệu
    Task<(IEnumerable<Workflow>?, int)> FilterAsync(WorkflowFilter model);
    Task<Workflow> GetByIdAsync(long id);
    Task<Workflow> GetByIdAsync(string Ma);
    Task<Workflow> CreateWorkflowAsync(WorkflowModel model, long createdBy);
    Task UpdateAsync(long id, WorkflowModel model, long updatedBy);
    Task UpdateWorkflowAsync(long id, WorkflowStatusModel model, long updatedBy);
    Task<bool> DeleteAsync(long id, long deletedBy);

    Task<WorkflowTemplateStep> GetTemplateStepById(long id);
    Task<WorkflowTemplateStep> CreateTemplateStepAsync(WorkflowStepTemplateModel model, long createdBy);
    Task UpdateTemplateStepAsync(long id, WorkflowStepTemplateModel model, long updatedBy);
    Task<bool> DeleteStepAsync(long id, long deletedBy);

    Task<WorkflowStep> UpdateStepAsync(WorkflowStep workflowStep, WorkflowStepModel model, long createdBy, string fullname);
    Task<WorkflowStep> GetStepById(long id);

    Task<bool> CheckStepPermission(Workflow workflow, string quyen, string action, long updatedBy);
    Task<bool> DoAction(string actions, Workflow step);

    Task<(IEnumerable<DuAn>?, int)> GetProjectsAsync(DuAnFilter model);
    Task<DuAn> GetDuAnByIdAsync(long id);
    Task<DuAn> CreateDuAnAsync(DuAnModel model, long createdBy);
    Task<bool> DeleteDuAnAsync(long id, long deletedBy);
    Task UpdateDuAnAsync(long id, DuAnModel model, long updatedBy);
}

public class WorkflowRepository : IWorkflowRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<WorkflowCategory> _workflowCategoryRepository;
    private readonly IRepository<WorkflowTemplate> _workflowTemplateRepository;
    private readonly IRepository<WorkflowTemplateStep> _workflowTemplateStepRepository;
    private readonly IRepository<Workflow> _workflowRepository;
    private readonly IRepository<WorkflowStep> _workflowStepRepository;
    private readonly IRepository<WorkflowStepLog> _workflowStepLogRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<DuAn> _duAnRepository;
    private const string Label = "quy trình";

    public WorkflowRepository(
            IMapper mapper,
            IRepository<WorkflowCategory> workflowCategoryRepository,
            IRepository<WorkflowTemplateStep> workflowTemplateStepRepository,
            IRepository<Workflow> workflowRepository,
            IRepository<User> userRepository,
            IActivityLogRepository activityLogRepository,
            IRepository<WorkflowStep> workflowStepRepository,
            IRepository<WorkflowTemplate> workflowTemplateRepository,
            IRepository<WorkflowStepLog> workflowStepLogRepository,
            IRepository<DuAn> duAnRepository
        )
        {
        _mapper = mapper;
        _workflowTemplateStepRepository = workflowTemplateStepRepository;
        _workflowRepository = workflowRepository;
        _activityLogRepository = activityLogRepository;
        _workflowStepRepository = workflowStepRepository;
        _userRepository = userRepository;
        _workflowCategoryRepository = workflowCategoryRepository;
        _workflowTemplateRepository = workflowTemplateRepository;
        _workflowStepLogRepository = workflowStepLogRepository;
        _duAnRepository = duAnRepository;
        }

    #region Workflow data
    public async Task<Workflow> CreateWorkflowAsync(WorkflowModel model, long createdBy)
    {
        if(model.MaQuyTrinhMau <= 0)
        {
            throw new ArgumentException($"Mã mẫu quy trình không tồn tại!");
        }
        if (model.MaDMQT <= 0)
        {
            throw new ArgumentException($"Mã danh mục quy trình không tồn tại!");
        }

        var query = _workflowRepository.Select();
        var item = await query
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());

        if (item != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");

        //TODO: Check permission for add Quy trình

        WorkflowTemplate? workflowTemplate = await _workflowTemplateRepository.Select().Where(p => p.Id == model.MaQuyTrinhMau).SingleOrDefaultAsync();
        if(workflowTemplate != null)
        {
            Workflow newItem = _mapper.Map<Workflow>(model);
            newItem.DuAnId = model.DuAnId;
            newItem.WorkflowCategoryId = model.MaDMQT;
            newItem.WorkflowTemplateId = model.MaQuyTrinhMau;
            newItem.CapNhat = Utils.getCurrentDate();
            newItem.NgayTao = Utils.getCurrentDate();
            newItem.NguoiCapNhat = createdBy;
            newItem.BuocHienTai = 1;
            newItem.TrangThai = 1;
            newItem.TongBuoc = workflowTemplate.TongBuoc;
            newItem.NoiDung = workflowTemplate.NoiDung;
            newItem.SuKien = workflowTemplate.SuKien;
            newItem.Quyen = workflowTemplate.Quyen;
            _workflowRepository.Insert(newItem);
            await _workflowRepository.SaveChangesAsync();

            // Init all Step for workflow
            List<WorkflowTemplateStep> steps = await _workflowTemplateStepRepository.Select().Where(p => p.WorkflowTemplateId == model.MaQuyTrinhMau).ToListAsync();
            foreach (WorkflowTemplateStep step in steps)
            {
                // Them cac mau
                WorkflowStep workflowStep = new WorkflowStep
                {
                    WorkflowId = newItem.Id,
                    Ma = step.Ma,
                    Ten = step.Ten,
                    MaTK = createdBy,
                    Buoc = step.Buoc,
                    NoiDung = step.NoiDung ?? "",
                    //ThaoTac = step.ThaoTac,
                    //Quyen = step.Quyen,
                    ChuKy = step.ChuKy,
                    XuLy = createdBy,
                    TrangThai = 0,
                    CapNhat = DateTime.Now
                };

                _workflowStepRepository.Insert(workflowStep);
            }
            await _workflowStepRepository.SaveChangesAsync();
           
            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"dữ liệu quy trình mẫu với mã {newItem.Ma} và tên: {newItem.Ten} thành công.",
                Params = newItem.Ma,
                Target = "Workflow",
                TargetCode = newItem.Ma,
                UserId = createdBy
            };

            await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

            return newItem;
        }

        throw new ArgumentException($"Mẫu quy trình không tồn tại!");

    }

    // Xoa noi dung Workflow -> xoa all step + logs
    public async Task<bool> DeleteAsync(long id, long deletedBy)
    {
        Workflow item = await GetByIdAsync(id);

        // Remove all steps
        var steps = await _workflowStepRepository.Select().Where(p => p.WorkflowId.Equals(id)).ToListAsync();
        foreach (var step in steps)
        {
            await _workflowStepLogRepository.Select().Where(p => p.WorkflowStepId == step.Id).ExecuteDeleteAsync();
            _workflowStepRepository.Delete(step);
        }
       
        _workflowRepository.Delete(item);

        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"nội dung quy trình với mã #{item.Ma} và tên: {item.Ten} thành công.",
            Params = item.Ma,
            Target = "Workflow",
            TargetCode = item.Ma,
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
        return true;
    }

    // Lay danh sach quy trinh workflow data
    public async Task<(IEnumerable<Workflow>?,int)> FilterAsync(WorkflowFilter model)
    {
        var query = _workflowRepository.Select();

        if(model.MaDMQT > 0)
        {
            query = query.Where(p => p.WorkflowCategoryId == model.MaDMQT);
        }
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Ten)
           ? query.Where(p => p.Ten.ToLower().Contains(model.Ten.ToLower()))
           : query;

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(p => p.Ma.ToLower().Contains(model.Keyword.ToLower()) || p.Ten.ToLower().Contains(model.Keyword.ToLower()));
        }

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p => p.Ma.ToLower().Contains(model.TuKhoa.ToLower()) || p.Ten.ToLower().Contains(model.TuKhoa.ToLower()));
        }

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.capNhat))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.capNhat, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.CapNhat.HasValue && p.CapNhat.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.capNhat + "' is not valid for NgayCapNhat.");
            }
        }
       
        if (!string.IsNullOrEmpty(model.ngayTao))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.ngayTao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.NgayTao.HasValue && p.NgayTao.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.ngayTao + "' is not valid for NgayTao.");
            }
        }

        //sort by
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ma":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ma) : query.OrderBy(p => p.Ma);
                    break;
                case "ten":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ten) : query.OrderBy(p => p.Ten);
                    break;
                case "trangthai":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TrangThai) : query.OrderBy(p => p.TrangThai);
                    break;
                case "ngaytao":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.NgayTao) : query.OrderBy(p => p.NgayTao);
                    break;
                case "capnhat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.CapNhat) : query.OrderBy(p => p.CapNhat);
                    break;
                default:
                    query = query.OrderByDescending(p => p.NgayTao); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.NgayTao); }
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(x => x.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var workflows = new List<Workflow>();

        foreach (Workflow workflow in items)
        {
            workflow.WorkflowSteps = await _workflowStepRepository.Select().Where(p => p.WorkflowId == workflow.Id).ToListAsync();
            workflow.NoiDung = "";
            workflows.Add(workflow);
        }

        var records = await query.CountAsync();
        return (workflows, records);
    }

    // Lay chi tiet workflow 
    public async Task<Workflow> GetByIdAsync(string Ma)
    {
        Workflow? workflow = await _workflowRepository.Select().Where(p => p.Ma.Equals(Ma, StringComparison.Ordinal)).FirstOrDefaultAsync();
        if (workflow != null)
        {
            workflow.WorkflowSteps = await _workflowStepRepository.Select().Where(p => p.WorkflowId == workflow.Id).OrderBy(p => p.Buoc).ToListAsync();
           //if (workflow.WorkflowSteps.Count <= 0)
           //{

           //}
            return workflow;
        } else
        {
            throw new ArgumentException($"Không tìm thấy {Label}!");
        }
    }

    public async Task<Workflow> GetByIdAsync(long Id)
    {
        Workflow? workflow = await _workflowRepository.Select().AsNoTracking().Where(p => p.Id == Id).FirstOrDefaultAsync();
        if (workflow != null)
        {
            workflow.WorkflowSteps = await _workflowStepRepository.Select().Where(p => p.WorkflowId == Id).ToListAsync();
            //workflow.WorkflowStepLogs = await _workflowStepLogsRepository.Select().Where(p => p.MaQuyTrinh == workflow.Id).ToListAsync();

            return workflow;
        }
        else
        {
            throw new ArgumentException($"Không tìm thấy {Label}!");
        }
    }

    // Update noi dung quy trinh co NoiDung la du lieu cac step
    public async Task UpdateAsync(long id, WorkflowModel model, long updatedBy)
    {
        Workflow item = await GetByIdAsync(id);

        var isExist = await _workflowRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");

        //Kiem tra buoc hien tại -> check quyen buoc hien tai
        //if (!workflowStep.Quyen.IsNullOrEmpty())
        //{
        //    bool canUpdate = await CheckStepPermission(workflow, workflowStep.Quyen, "Add", createdBy);
        //    if (!canUpdate)
        //    {
        //        throw new ArgumentException($"Bạn không có quyền cập nhật bước quy trình");
        //    }
        //}

        _mapper.Map(model, item);
        item.CapNhat = Utils.getCurrentDate();
        _workflowRepository.Update(item);

        await _workflowRepository.SaveChangesAsync();
        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"dữ liệu của quy trình với mã #{item.Ma} và tên: {item.Ten} thành công.",
            Params = item.Ma,
            Target = "Workflow",
            TargetCode = item.Ma,
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task UpdateWorkflowAsync(long id, WorkflowStatusModel model, long updatedBy)
    {
        Workflow item = await GetByIdAsync(id);
        item.CapNhat = Utils .getCurrentDate();
        item.TrangThai = model.TrangThai;
        item.GhiChu = model.GhiChu;
        if(!model.Quyen.IsNullOrEmpty())
        {
            item.Quyen = model.Quyen;
        }
        if (!model.SuKien.IsNullOrEmpty())
        {
            item.SuKien = model.SuKien;
        }
        _workflowRepository.Update(item);

        await _workflowRepository.SaveChangesAsync();
        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"trạng thái dữ liệu cho quy trình với mã #{item.Ma} và tên: {item.Ten} thành công",
            Params = item.Ma,
            Target = "Workflow",
            TargetCode = item.Ma,
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    #endregion

    #region Workflow Template Step Buoc Quy trinh Mau

    public async Task<WorkflowTemplateStep> CreateTemplateStepAsync(WorkflowStepTemplateModel model, long createdBy)
    {
        var item = await _workflowTemplateStepRepository.Select().Where(p => p.WorkflowTemplateId == model.MaQuyTrinhMau).FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc mã bước quy trình đã tồn tại!");

        var quyTrinhMau = _workflowTemplateRepository.Select().Where(p => p.Id == model.MaQuyTrinhMau).FirstOrDefault();
        if (quyTrinhMau == null) throw new ArgumentException($"Quy trình mẫu không tồn tại!");

        var newItem = _mapper.Map<WorkflowTemplateStep>(model);
        newItem.CapNhat = DateTime.Now;
        newItem.Ngay = DateTime.Now;
        newItem.WorkflowTemplateId = model.MaQuyTrinhMau;

        _workflowTemplateStepRepository.Insert(newItem);
        await _workflowTemplateStepRepository.SaveChangesAsync();

        //quyTrinhMau.TongBuoc = _workflowTemplateStepRepository.Select().Where(p => p.WorkflowTemplateId == model.MaQuyTrinhMau).Count();

        //_workflowTemplateRepository.Update(quyTrinhMau);

        //await _workflowRepository.SaveChangesAsync();

        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"bước quy trình mẫu với mã #{newItem.Ma} và tên: {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "WorkflowTemplateStep",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };

        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task UpdateTemplateStepAsync(long id, WorkflowStepTemplateModel model, long updatedBy)
    {
        var item = await _workflowTemplateStepRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Bước quy trình không tồn tại!");

        var worklowTemplate = _workflowTemplateRepository.Select().Where(p => p.Id == model.MaQuyTrinhMau).FirstOrDefault();
        if (worklowTemplate == null) throw new ArgumentException($"Truy xuất mẫu quy trình không tồn tại!");

        var isExist = await _workflowTemplateStepRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc mã bước quy trình đã tồn tại!");
       
        _mapper.Map(model, item);
        item.CapNhat = DateTime.Now;
        item.WorkflowTemplateId = model.MaQuyTrinhMau;
        item.NguoiCapNhat = updatedBy;

        _workflowTemplateStepRepository.Update(item);

        await _workflowTemplateStepRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"bước quy tình mẫu với mã #{item.Ma} và tên: {item.Ten} thành công.",
            Params = item.Ma,
            Target = "WorkflowTemplateStep",
            TargetCode = item.Ma,
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task<WorkflowTemplateStep> GetTemplateStepById(long id)
    {
        WorkflowTemplateStep? workflowStep = await _workflowTemplateStepRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (workflowStep != null)
        {
           //workflowStep.WorkflowStepLogs = await _workflowStepLogsRepository.Select().Where(p => p.MaQTXL == workflowStep.Id).ToListAsync();
            return workflowStep;
        }
        else
        {
            throw new ArgumentException($"Không tìm thấy bước quy trình mẫu!");
        }
    }
    
    public async Task<bool> DeleteStepAsync(long id, long deletedBy)
    {
        WorkflowTemplateStep? item = _workflowTemplateStepRepository.Select().Where(p => p.Id == id).FirstOrDefault();
        if (item != null)
        {
            await _workflowStepLogRepository.Select().Where(p => p.WorkflowStepId == id).ExecuteDeleteAsync();

            _workflowTemplateStepRepository.Delete(item);
            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"bước quy trình mẫu với mã #{item.Ma} và tên: {item.Ten} thành công.",
                Params = item.Ma,
                Target = "WorkflowTemplateStep",
                TargetCode = item.Ma,
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
            return true;
        }
        return false;
    }
    #endregion

    #region Category
    public async Task<(IEnumerable<WorkflowCategory>?, int)> GetCategoryAsync(WorkflowCategoryFilter model)
    {
        var query = _workflowCategoryRepository.Select();

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p =>
                p.Ma.ToLower().Contains(model.TuKhoa.ToLower()) ||
                p.Ten.ToLower().Contains(model.TuKhoa.ToLower()));
        }

        if (!string.IsNullOrEmpty(model.ThongTu))
        {
            query = query.Where(p => p.ThongTu.ToLower().Contains(model.ThongTu.ToLower()));
        }

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        if (!string.IsNullOrEmpty(model.Ma))
        {
            query = query.Where(x => x.Ma == model.Ma);
        }


        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(x => x.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var workflows = new List<WorkflowCategory>();
        foreach (WorkflowCategory workflowCategory in items)
        {
            workflowCategory.WorkflowTemplates = await _workflowTemplateRepository.Select().Where(p => p.WorkflowCategoryId == workflowCategory.Id)
                .Select( p => new WorkflowTemplate()
                {
                    Id = p.Id,
                    WorkflowCategoryId = workflowCategory.Id,
                    Ten = p.Ten,
                    Ma = p.Ma,
                    TongBuoc = p.TongBuoc,
                    TrangThai = p.TrangThai,
                    CapNhat = p.CapNhat
                }
                ).OrderBy(p => p.Ma).ToListAsync();
            workflows.Add(workflowCategory);
        }

        var records = await query.CountAsync();
        return (workflows, records);
    }

    public async Task<WorkflowCategory> GetCategoryByIdAsync(long id)
    {
        WorkflowCategory? workflowCategory = await _workflowCategoryRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (workflowCategory != null)
        {
            workflowCategory.WorkflowTemplates = await _workflowTemplateRepository.Select().Where(p => p.WorkflowCategoryId == workflowCategory.Id).OrderBy(p => p.Ma).ToListAsync();
            return workflowCategory;
        }
        else
        {
            throw new ArgumentException($"Không tìm thấy {Label}!");
        }
    }

    public async Task<WorkflowCategory> CreateCategoryAsync(WorkflowCategoryModel model, long createdBy)
    {
        var query = _workflowCategoryRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());

        if (item != null) throw new ArgumentException($"Tên hoặc mã danh mục quy trình đã tồn tại!");

        var newItem = _mapper.Map<WorkflowCategory>(model);
        _workflowCategoryRepository.Insert(newItem);
        await _workflowCategoryRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"danh mục quy trình với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "WorkflowCategory",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
        return newItem;
    }

    public async Task UpdateCategoryAsync(long id, WorkflowCategoryModel model, long updatedBy)
    {
        var item = await GetCategoryByIdAsync(id);

        var isExist = await _workflowCategoryRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Ten.ToLower().ToLower() == model.Ten.ToLower() ||
                p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc mã danh mục quy trình đã tồn tại!");

        _mapper.Map(model, item);

        _workflowCategoryRepository.Update(item);

        await _workflowCategoryRepository.SaveChangesAsync();

        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"danh mục quy trình với mã #{item.Ma} và tên: {item.Ten} thành công",
            Params = item.Ma,
            Target = "WorkflowCategory",
            TargetCode = item.Ma,
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task<bool> DeleteCategoryAsync(long id, long deletedBy)
    {
        WorkflowCategory? item = _workflowCategoryRepository.Select().Where(p => p.Id == id).FirstOrDefault();
        if (item != null)
        {
            _workflowCategoryRepository.Delete(item);
            await _workflowRepository.Select().Where(p => p.WorkflowCategoryId == id).ExecuteDeleteAsync();
            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"danh mục quy trình với mã #{item.Ma} và tên: {item.Ten} thành công",
                Params = item.Ma,
                Target = "WorkflowCategory",
                TargetCode = item.Ma,
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
            return true;
        }
        return false;
    }

    #endregion

    #region Workflow Template
    public async Task<(IEnumerable<WorkflowTemplate>?, int)> GetTemplateAsync(WorkflowFilter model)
    {
        var query = _workflowTemplateRepository.Select();

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p =>
                p.Ma.ToLower().Contains(model.TuKhoa.ToLower()) ||
                p.Ten.ToLower().Contains(model.TuKhoa.ToLower()) ||
                p.GhiChu != null && p.GhiChu.ToLower().Contains(model.TuKhoa.ToLower()));

        }
        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        if (!string.IsNullOrEmpty(model.Ma))
        {
            query = query.Where(x => x.Ma == model.Ma);
        }

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .Distinct()
            .OrderBy(p => p.Ma)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var workflows = new List<WorkflowTemplate>();

        foreach (WorkflowTemplate workflowTemplate in items)
        {
            workflowTemplate.WorkflowTemplateSteps = await _workflowTemplateStepRepository.Select().Where(p => p.WorkflowTemplateId == workflowTemplate.Id)
                .OrderBy(p=>p.Buoc).ToListAsync();
            workflows.Add(workflowTemplate);
        }

        var records = await query.CountAsync();
        return (workflows, records);
    }

    public async Task<WorkflowTemplate> GetTemplateByIdAsync(long id)
    {
        WorkflowTemplate? workflowTemplate = await _workflowTemplateRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (workflowTemplate != null)
        {
            workflowTemplate.WorkflowTemplateSteps = await _workflowTemplateStepRepository.Select().Where(p => p.WorkflowTemplateId == workflowTemplate.Id).OrderBy(p => p.Buoc).ToListAsync();
            return workflowTemplate;
        }
        else
        {
            throw new ArgumentException($"Không tìm thấy {Label}!");
        }
    }

    public async Task<WorkflowTemplate> CreateTemplateAsync(WorkflowTemplateModel model, long createdBy)
    {
        if (model.MaDMQT <= 0) throw new ArgumentException($"Danh mục quy trình không tìm thấy");
        var query = _workflowTemplateRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());

        if (item != null) throw new ArgumentException($"Tên hoặc mã mẫu quy trình đã tồn tại!");

        var newItem = _mapper.Map<WorkflowTemplate>(model);
        newItem.CapNhat = DateTime.Now;
        newItem.WorkflowCategoryId = model.MaDMQT;
        _workflowTemplateRepository.Insert(newItem);
        await _workflowTemplateRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mẫu quy trình với mã #{newItem.Ma} và tên: {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "WorkflowCategory",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
        return newItem;
    }

    public async Task UpdateTemplateAsync(long id, WorkflowTemplateModel model, long updatedBy)
    {
        var item = await GetTemplateByIdAsync(id);

        var isExist = await _workflowTemplateRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());

        if (isExist != null) throw new ArgumentException($"Mẫu quy trình đã tồn tại!");

        _mapper.Map(model, item);
        item.WorkflowCategoryId = model.MaDMQT;
        _workflowTemplateRepository.Update(item);

        await _workflowTemplateRepository.SaveChangesAsync();
        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"mẫu quy trình với mã #{item.Ma} và tên: {item.Ten} thành công",
            Params = item.Ma,
            Target = "WorkflowTemplate",
            TargetCode = item.Ma,
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task UpdateTemplateRoleAndAction(long id, WorkflowTemplateModel model, long updatedBy)
    {
        var item = await GetTemplateByIdAsync(id);
        if(item != null)
        {
            item.WorkflowCategoryId = model.MaDMQT;
            item.SuKien = model.SuKien;
            item.Quyen = model.Quyen;
            item.WorkflowCategoryId = model.MaDMQT;
            _workflowTemplateRepository.Update(item);

            await _workflowTemplateRepository.SaveChangesAsync();
            // ____________ Log ____________

            var log = new ActivityLogDto
            {
                Contents = $"mẫu quy trình với mã #{item.Ma} và tên: {item.Ten} thành công",
                Params = item.Ma,
                Target = "WorkflowTemplate",
                TargetCode = item.Ma,
            };

            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
    }

    public async Task<bool> DeleteTemplateAsync(long id, long deletedBy)
    {
        WorkflowTemplate? item = _workflowTemplateRepository.Select().Where(p => p.Id == id).FirstOrDefault();
        if (item != null)
        {
            _workflowTemplateRepository.Delete(item);
            await _workflowTemplateStepRepository.Select().Where(p => p.WorkflowTemplateId == id).ExecuteDeleteAsync();
            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"mẫu quy trình với mã #{item.Ma} và tên: {item.Ten} thành công",
                Params = item.Ma,
                Target = "WorkflowTemplate",
                TargetCode = item.Ma,
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
            return true;
        }
        return false;
    }

    #endregion

    /// <summary>
    /// Lưu nội dung phản hồi bước quy trình
    /// </summary>
    /// <param name="model"></param>
    /// <param name="createdBy"></param>
    /// <param name="fullname"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<WorkflowStep> UpdateStepAsync(WorkflowStep workflowStep, WorkflowStepModel model, long createdBy, string fullname)
    {
        Workflow? workflow = await _workflowRepository.Select().AsNoTracking().Where(p=>p.Id == workflowStep.WorkflowId).FirstOrDefaultAsync();
        if (workflow == null)
        {
            throw new ArgumentException($"Quy trình không tồn tại.");
        }

        if (model.HoTen == "") model.HoTen = fullname;

        workflowStep.XuLy = createdBy;

        if (model.TrangThaiXL == (short)WorkflowStepStatus.Duyet) // Duyet va ky ten
        {
            //if (workflow.Quyen != null)
            //{
            //    bool canUpdate = await CheckStepPermission(workflow, workflow.Quyen, "Approve", createdBy);
            //    //Nếu không có
            //    if (!canUpdate)
            //    {
            //        throw new ArgumentException($"Bạn không có quyền cập nhật bước quy trình5");
            //    }
            //}

            // TODO: Xu ly Action
            if (workflow.SuKien != null)
            {
                await DoAction(workflow.SuKien, workflow);
            }
            // Neu ko co co yeu cau chu ky se tang BUOC 
            if (workflowStep.ChuKy == 0)
            {
                // Khi đã duyệt sẽ chuyển WF sang bước kế tiếp
                if (workflow.TongBuoc > workflowStep.Buoc)
                {
                    workflow.BuocHienTai = workflowStep.Buoc + 1;
                } else
                {

                }
                workflow.CapNhat = Utils.getCurrentDate();
                _workflowRepository.Update(workflow);
            }
        }
        else if (model.TrangThaiXL == (short)WorkflowStepStatus.DaKy)
        {
            if (workflow.Quyen != null)
            {
                bool canUpdate = await CheckStepPermission(workflow, workflow.Quyen, "Sign", createdBy);
                if (!canUpdate)
                {
                    throw new ArgumentException($"Bạn không có quyền cập nhật bước quy trình1");
                }
            }
            // Neu co yeu cau chu ky se tang BUOC 
            if (workflowStep.ChuKy == 1 && !model.MaChuKy.IsNullOrEmpty())
            {
                // WorkflowStep.KyNgay = Utils.getCurrentDate();
                // WorkflowStep.MaChuKy = model.MaChuKy;
                // Khi đã duyệt sẽ chuyển WF sang bước kế tiếp
                if (workflow.TongBuoc > workflowStep.Buoc)
                {
                    workflow.BuocHienTai = workflowStep.Buoc + 1;
                }

                workflow.CapNhat = Utils.getCurrentDate();

                _workflowRepository.Update(workflow);
            }
            else
            {
                throw new ArgumentException($"Yêu cầu chữ ký số trước khi qua bước kế tiếp");
            }
        } else if (model.TrangThaiXL == (short)WorkflowStepStatus.ChoDuyet)
        {   // Update step -> check permission
            //if (workflow.Quyen != null)
            //{
            //    bool canUpdate = await CheckStepPermission(workflow, workflow.Quyen, "Update", createdBy);
            //    if (!canUpdate)
            //    {
            //        throw new ArgumentException($"Bạn không có quyền cập nhật bước quy trình2");
            //    }
            //}
        }
        else
        {   // Add step -> check permission
            if (workflow.Quyen != null)
            {
                bool canUpdate = await CheckStepPermission(workflow, workflow.Quyen, "Add", createdBy);
                if (!canUpdate)
                {
                    throw new ArgumentException($"Bạn không có quyền cập nhật bước quy trình3");
                }
            }
        }

        WorkflowStepLog steplog = new WorkflowStepLog
        {
            Ma = Utils.GetUUID(),
            WorkflowStepId = workflowStep.Id,
            NgayTao = Utils.getCurrentDate(),
            NguoiTao = createdBy,
            NoiDung = model.PhanHoi,
            DinhKem = model.DinhKem != null ? model.DinhKem : "",
            HoTen = fullname,
            HanhDong = model.HanhDong, 
            MaChuKy = model.MaChuKy
        };

        _workflowStepLogRepository.Insert(steplog);
       
        workflowStep.NoiDung = model.NoiDung;
        workflowStep.CapNhat = Utils.getCurrentDate();
        workflowStep.TrangThai = model.TrangThaiXL;

        _workflowStepRepository.Update(workflowStep);

        await _workflowRepository.SaveChangesAsync();
        await _workflowStepLogRepository.SaveChangesAsync();
        await _workflowStepRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin phản hồi bước quy trình với mã #{workflowStep.Ma} và tên: {workflowStep.Ten}",
            Params = workflowStep.Ma,
            Target = "WorkflowStepLogs",
            TargetCode = workflowStep.Ma,
            UserId = createdBy
        };

        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Update);

        return workflowStep;
    }

    public async Task<bool> CheckStepPermission(Workflow workflow, string quyen, string action, long updatedBy)
    {
    
        try
        {
            //Neu updatedBy is owner of workflow or Admin
            User? user = await _userRepository.Select().FirstOrDefaultAsync(p => p.Id == updatedBy);
            if (user != null)
            {
                if (user.Role != null && user.Role.ToLower() == "admin")
                {
                    return await Task.FromResult(true);
                }
            }

            //Check Owner
            if (workflow != null)
            {
                if (workflow.NguoiCapNhat == updatedBy)
                {
                    return await Task.FromResult(true);
                }
            }

            //Check quyen
            if (!quyen.IsNullOrEmpty())
            {
                JObject? jsonQuyen = JsonConvert.DeserializeObject<JObject>(quyen);
                if (jsonQuyen != null)
                {
                    if (action == "Approve")
                    {
                        if (jsonQuyen.GetValue("Approve") != null)
                        {
                            string? canApproveIDs = jsonQuyen.GetValue("Approve")?.ToString();
                            if (canApproveIDs != null && canApproveIDs != "" && canApproveIDs != "[]")
                            {
                                if (canApproveIDs.Length > 0)
                                {
                                    string[] canUpdateIDArr = canApproveIDs.Split(",");
                                    foreach (string IdCheck in canUpdateIDArr)
                                    {
                                        if (Convert.ToDouble(IdCheck) == updatedBy)
                                        {
                                            return await Task.FromResult(true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (action == "Update")
                    {
                        if (jsonQuyen.GetValue("Update") != null)
                        {
                            string? canUpdateIDs = jsonQuyen.GetValue("Update")?.ToString();
                            if (canUpdateIDs != null && canUpdateIDs != "" && canUpdateIDs != "[]")
                            {
                                string[] canUpdateIDArr = canUpdateIDs.Split(",");
                                if (canUpdateIDArr.Length > 0)
                                {
                                    foreach (string IdCheck in canUpdateIDArr)
                                    {
                                        if (Convert.ToDouble(IdCheck) == updatedBy)
                                        {
                                            return await Task.FromResult(true);
                                        }
                                    }
                                }
                            }
                        }
                        //}
                        //else if (action == "Sign")
                        //{
                        //    if (jsonQuyen.GetValue("Sign") != null)
                        //    {
                        //        string? canSignIDs = jsonQuyen.GetValue("Sign")?.ToString();
                        //        if (canSignIDs != null)
                        //        {
                        //            string[] canUpdateIDArr = canSignIDs.Split(",");
                        //            foreach (string IdCheck in canUpdateIDArr)
                        //            {
                        //                if (Convert.ToDouble(IdCheck) == updatedBy)
                        //                {
                        //                    return await Task.FromResult(true);
                        //                } 
                        //            }
                        //        }
                        //    }
                    }
                    else if (action == "Add")
                    {
                        if (jsonQuyen.GetValue("Add") != null)
                        {
                            string? canAddIDs = jsonQuyen.GetValue("Add")?.ToString();
                            if (canAddIDs != null && canAddIDs != "" && canAddIDs != "[]")
                            {
                                string[] canUpdateIDArr = canAddIDs.Split(",");
                                if (canUpdateIDArr.Length > 0)
                                {
                                    foreach (string IdCheck in canUpdateIDArr)
                                    {
                                        if (Convert.ToDouble(IdCheck) == updatedBy)
                                        {
                                            return await Task.FromResult(true);
                                        }
                                    }
                                }
                            }
                        }
                    } else
                    {

                        if (jsonQuyen.GetValue("View") != null)
                        {
                            string? canViewIDs = jsonQuyen.GetValue("View")?.ToString();
                            if (canViewIDs != null && canViewIDs != "" && canViewIDs != "[]")
                            {
                                string[] canUpdateIDArr = canViewIDs.Split(",");
                                if (canUpdateIDArr.Length > 0)
                                {
                                    foreach (string IdCheck in canUpdateIDArr)
                                    {
                                        if (Convert.ToDouble(IdCheck) == updatedBy)
                                        {
                                            return await Task.FromResult(true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } catch(Exception ex)
        {
            SentrySdk.CaptureException(ex);
        }
        return await Task.FromResult(false);
    }

    public async Task<bool> DoAction(string actions, Workflow workflow)
    {
        JObject? jsonThaoTac = JsonConvert.DeserializeObject<JObject>(actions);
        if (jsonThaoTac != null)
        {
            //Email & Notification
            //UpdateModel:
            //SQL
            if (jsonThaoTac.GetValue("Email") != null)
            {
                string? emails = jsonThaoTac.GetValue("Email")?.ToString();
                if (emails != null)
                {
                    //List<string> emailCC = new List<string>();
                    string[] emailArr = emails.Split(",");
                    foreach (string email in emailArr)
                    {
                        //Check is email address, if not will be user code
                        if (Utils.IsValidEmail(email))
                        {
                            //emailCC.Add(email);
                            //Send email CC
                            EmailHelper emailHelper = new EmailHelper();
                            string subject = "Tiêu đề";
                            var model = new { Fullname = workflow.Ma, WorkflowName = workflow.Ten, WorkflowStepName = workflow.Ten, HoTen = workflow.Ten };
                            await emailHelper.SendEmailTemplate("workflow_step_action.cshtml", model, email, subject, "");
                        }
                        //else
                        //{
                        //    User? user = await _userRepository.Select().FirstOrDefaultAsync(p => p.Code == email);
                        //    if (user != null)
                        //    {
                        //        if (user.Email != null && Utils.IsValidEmail(user.Email))
                        //        {
                        //            emailCC.Add(user.Email);
                        //        }
                        //    }
                        //}
                    }
                }
            }
            else if (jsonThaoTac.GetValue("SQL") != null)
            {

            }
            // Thao tac khac or webhook
        }
        return true;
    }

    public async Task<WorkflowStep> GetStepById(long id)
    { 
        WorkflowStep? workflowStep = await _workflowStepRepository.Select().AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (workflowStep != null)
        {
            workflowStep.WorkflowStepLogs = await _workflowStepLogRepository.Select().Where(p => p.WorkflowStepId == workflowStep.Id).ToListAsync();
            return workflowStep;
        }
        else
        {
            throw new ArgumentException($"Không tìm thấy bước quy trình!");
        }
    }
    #region Du An
    public async Task<(IEnumerable<DuAn>?, int)> GetProjectsAsync(DuAnFilter model)
    {
        var query = _duAnRepository.Select();

        query = !string.IsNullOrEmpty(model.Ten)
            ? query.Where(p => p.Ten.ToLower().Contains(model.Ten.ToLower()))
            : query;

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(p =>
                p.Ma.ToLower().Contains(model.Keyword.ToLower()) || p.Ten.ToLower().Contains(model.Keyword.ToLower()));
        }
        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.UpdatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.UpdatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.UpdatedAt.HasValue && p.UpdatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.UpdatedAt + "' is not valid for NgayCapNhat.");
            }
        }
        if (!string.IsNullOrEmpty(model.CreatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.CreatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.CreatedAt.HasValue && p.CreatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.CreatedAt + "' is not valid for createAt.");
            }
        }
        //sort by column
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ma":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ma) : query.OrderBy(p => p.Ma);
                    break;
                case "ten":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ten) : query.OrderBy(p => p.Ten);
                    break;
                case "trangthai":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TrangThai) : query.OrderBy(p => p.TrangThai);
                    break;
                case "createdat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                    break;
                case "updatedat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }

        if (!string.IsNullOrEmpty(model.Ma))
        {
            query = query.Where(x => x.Ma == model.Ma);
        }
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        //var duans = new List<DuAn>();
        //foreach (DuAn duan in items)
        //{
        //    duan.Workflows = await _workflowRepository.Select().Where(p => p.DuAnId == duan.Id).ToListAsync();
        //    duans.Add(duan);
        //}

        var records = await query.CountAsync();
        return (items, records);
    }
   
    public async Task<DuAn> GetDuAnByIdAsync(long id)
    {
        DuAn? duan = await _duAnRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (duan != null)
        {
            duan.Workflows = await _workflowRepository.Select().Where(p => p.DuAnId == duan.Id).ToListAsync();
            return duan;
        }
        else
        {
            throw new ArgumentException($"Không tìm thấy {Label}!");
        }
    }

    public async Task<DuAn> CreateDuAnAsync(DuAnModel model, long createdBy)
    {
        var query = _duAnRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());

        if (item != null) throw new ArgumentException($"Tên hoặc mã dự án đã tồn tại!");

        var newItem = _mapper.Map<DuAn>(model);
        newItem.CreatedAt = DateTime.UtcNow;
        newItem.UpdatedAt = DateTime.UtcNow;
        newItem.UserId = createdBy;
        _duAnRepository.Insert(newItem);
        await _duAnRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"dự án với mã #{newItem.Ma} thành công.",
            Params = newItem.Ma,
            Target = "DuAn",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
       
        return newItem;
    }

    public async Task UpdateDuAnAsync(long id, DuAnModel model, long updatedBy)
    {
        var item = await GetDuAnByIdAsync(id);

        var isExist = await _duAnRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc mã dự án đã tồn tại!");

        _mapper.Map(model, item);
        item.UpdatedAt = DateTime.UtcNow; 
        item.UserId = updatedBy;
        _duAnRepository.Update(item);

        await _duAnRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"dự án với tên {item.Ten} thành công",
            Params = item.Ma,
            Target = "DuAn",
            TargetCode = item.Ma,
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task<bool> DeleteDuAnAsync(long id, long deletedBy)
    {
        DuAn? item = _duAnRepository.Select().Where(p => p.Id == id).FirstOrDefault();
        if (item != null)
        {
            _duAnRepository.Delete(item);
            await _workflowRepository.Select().Where(p => p.DuAnId == id).ExecuteDeleteAsync();
            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"dự án với #{item.Ma} thành công",
                Params = item.Ma,
                Target = "DuAn",
                TargetCode = item.Ma,
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
            return true;
        }
        return false;
    }

    #endregion
}