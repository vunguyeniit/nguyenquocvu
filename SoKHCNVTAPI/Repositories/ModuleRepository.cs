using System.Reflection.Emit;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using SoKHCNVTAPI.Enums;
using Action = SoKHCNVTAPI.Entities.Action;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SoKHCNVTAPI.Repositories;

 public interface IModuleRepository
{
    Task<(IEnumerable<Module>?, int)> FilterAsync(ModuleFilter model);
    Task<Module> GetByIdAsync(long id);
    Task<Module> GetByIdAsync(string Code , long TargetId);
    Task<Module> CreateAsync(ModuleModel model, long createdBy);
    Task UpdateAsync(long id, ModuleModel model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

    public class ModuleRepository : IModuleRepository
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<Module> _moduleRepository;
        //private readonly IRepository<StepAction> _stepActionRepository;
        //private readonly IRepository<StepStatus> _stepStatusRepository;
        private readonly IActivityLogRepository _activityLogRepository;
        //private readonly IStepRepository _stepRepository;
        private readonly IUserRepository _userRepository;

        private const string Label = "phân hệ";

    public ModuleRepository(
            IMapper mapper,
            IRepository<Action> actionRepository,
            IRepository<Module> moduleRepository,
            IUserRepository userRepository,
            IActivityLogRepository activityLogRepository
            //IStepRepository stepRepository,
            //IRepository<StepAction> stepActionLogRepository,
            //IRepository<StepStatus> stepStatusLogRepository
        )
        {
            _mapper = mapper;
            _actionRepository = actionRepository;
            _moduleRepository = moduleRepository;
            _activityLogRepository = activityLogRepository;
            //_stepActionRepository = stepActionLogRepository;
           // _stepRepository = stepRepository;
           // _stepStatusRepository = stepStatusLogRepository;
            _userRepository = userRepository;
        }

    public async Task<Module> CreateAsync(ModuleModel model, long createdBy)
    {
        var query = _moduleRepository
           .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower() ||
                p.Code.ToLower().ToLower() == model.Code.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");

        var newItem = _mapper.Map<Module>(model);
        newItem.CreatedBy = createdBy;
        _moduleRepository.Insert(newItem);

        await _moduleRepository.SaveChangesAsync();

        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"{Label} với mã #{newItem.Code} và tên: {newItem.Name} thành công.",
            Params = newItem.Code,
            Target = "Module",
            TargetCode = newItem.Code,
            UserId = createdBy
        };
       await _activityLogRepository.SaveLogAsync(log,createdBy,LogMode.Create);

        return newItem;

    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id);
        await _moduleRepository.SaveChangesAsync();


        //StepFilter filter = new StepFilter { ModuleId = id };

        //// Tuple type
        //var (items, record) = await _stepRepository.FilterAsync(filter);
        //if (items != null)
        //{
        //    foreach (Step step in items)
        //    {
        //        await _stepRepository.DeleteAsync(step.Id, deletedBy);
        //    }
        //}

        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"{Label} với mã #{item.Code} và tên: {item.Name} thành công.",
            Params = item.Code,
            Target = "Module",
            TargetCode = item.Code,
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }

    public async Task<(IEnumerable<Module>?,int)> FilterAsync(ModuleFilter model)
    {
        var query =
            _moduleRepository
            .Select();

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p =>
                p.Code.ToLower().Contains(model.TuKhoa.ToLower()) ||
                p.Name.ToLower().Contains(model.TuKhoa.ToLower()));           
        }

        query = model.TrangThai.HasValue ? query.Where(p => p.Status == model.TrangThai) : query;

        if (!string.IsNullOrEmpty(model.Ma))
        {
           query = query.Where(x => x.Code == model.Ma);
        }


        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var modules = new List<Module>();

        if (long.IsPositive(model.TargetId))
        {
            foreach (Module module in items)
            {
                Module currentModel = await GetByIdAsync(module.Code, model.TargetId);
                modules.Add(currentModel);
            }
        }
        else
        {
            foreach (Module module in items)
            {
                Module currentModel = await GetByIdAsync(module.Id);
                modules.Add(currentModel);
            }
        }

        var records = await query.CountAsync();
        return (modules, records);

    }

    public async Task<Module> GetByIdAsync(string ModuleCode , long TargetId)
    {
        var query = _moduleRepository
            .Select(true);
        Module? item = await query
       // .Include(m => m.Steps)
        //.ThenInclude(x => x.StatusList)
        .SingleOrDefaultAsync(p => p.Code == ModuleCode);

        if (item == null)
        {
            throw new ArgumentException($"Không tìm thấy {Label}!");
        }

        //List<Step> steps = new List<Step>();
        //foreach (Step step in item.Steps)
        //{
        //    StepStatus? stepStatus = await _stepStatusRepository
        //        .Select()
        //        .Where(x => x.TargetId == TargetId)
        //        .Where(x => x.StepId == step.Id)
        //        .Where(x => x.ModuleId == item.Id)
        //        .FirstOrDefaultAsync();
        //    if (stepStatus != null)
        //    {
        //        var actions = await _stepRepository.GetActions(step.Id);
        //        var stepss = new Step
        //        {
        //            Id = step.Id,
        //            Name = step.Name,
        //            Description = step.Description,
        //            StepStatus = stepStatus,
        //            ModuleId = step.ModuleId,
        //            StatusList = step.StatusList,
        //            Tag = step.Tag,
        //            Actions = (List<ActionResponse>)actions,
        //        };

        //        steps.Add(stepss);
        //    }
        //}

        //item.Steps = steps;

        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");

        return item;
    }

    public async Task<Module> GetByIdAsync(long id)
    {
        var query = _moduleRepository
            .Select(true); 
        Module? item = await query
        //.Include(x => x.Steps)
        //.Include(x => x.StatusList)
        .SingleOrDefaultAsync(p => p.Id == id);


        //List<Step> steps = new List<Step>();
        //foreach (Step step in item.Steps)
        //{
        //    StepStatus? stepStatus = await _stepStatusRepository
        //        .Select()
        //        .Where(x => x.StepId == step.Id)
        //        .Where(x => x.ModuleId == item.Id)
        //        .FirstOrDefaultAsync();

        //    var actions = await _stepRepository.GetActions(step.Id);


        //    var stepss = new Step
        //    {

        //        Id = step.Id,
        //        Name = step.Name,
        //        Description = step.Description,
        //        StepStatus = stepStatus,
        //        ModuleId = step.ModuleId,
        //        Tag = step.Tag,
        //        StatusList = step.StatusList,
        //        Actions = (List<ActionResponse>)actions,
        //    };

        //    steps.Add(stepss);
        //}

        //item.Steps = steps;

        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");

        return item;
    }

    public async Task UpdateAsync(long id, ModuleModel model, long updatedBy)
    {
        var item = await GetByIdAsync(id);
        var isExist = await _moduleRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower() ||
                p.Code.ToLower().ToLower() == model.Code.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");

        _mapper.Map(model, item);
        _moduleRepository.Update(item);
        await _moduleRepository.SaveChangesAsync();

        // ____________ Log ____________
       
        var log = new ActivityLogDto
        {
            Contents = $"{Label} {item.Name}",
            Params = JsonConvert.SerializeObject(item),
            Target = "Module",
            TargetCode = item.Id.ToString(),
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
}


