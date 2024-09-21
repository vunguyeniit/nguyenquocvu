using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Enums;


namespace SoKHCNVTAPI.Repositories;

public interface IStepStatusRepository
{
    Task CreateAsyncStepStatus(StepStatusModel model);
    Task UpdateStepStatus(long stepId, StepStatusModel model, long updatedBy);
    Task Delete(long StepId, long ModuleId);
    Task Delete(long TargetId);


};

public class StepStatusRepository : IStepStatusRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<Step> _stepRepository;
    private readonly IRepository<StepStatus> _stepStatusRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public StepStatusRepository(

            IRepository<StepStatus> stepStatusRepository,
            IRepository<Step> stepRepository,
            IActivityLogRepository activityLogRepository,
            IMapper mapper

          )
    {
        _mapper = mapper;
        _stepStatusRepository = stepStatusRepository;
        _activityLogRepository = activityLogRepository;
        _stepRepository = stepRepository;
	}

    public async Task CreateAsyncStepStatus(StepStatusModel model)
    {
        var stepStatus = _mapper.Map<StepStatus>(model);
        _stepStatusRepository.Insert(stepStatus);
        await _stepStatusRepository.SaveChangesAsync();
    }

    public async Task Delete(long StepId, long ModuleId)
    {
        var stepStatus = await _stepStatusRepository
            .Select()
            .Where(x => x.StepId == StepId)
            .Where(x => x.ModuleId == ModuleId)
            .FirstOrDefaultAsync();

        if (stepStatus != null)
        {
            _stepStatusRepository.Delete(stepStatus);
            await _stepStatusRepository.SaveChangesAsync();
        }
    }

    public async Task Delete(long TargetId)
    {
        var stepStatus = await _stepStatusRepository
            .Select()
            .Where(x => x.TargetId == TargetId)
            .FirstOrDefaultAsync();

        if (stepStatus != null)
        {
            _stepStatusRepository.Delete(stepStatus);
            await _stepStatusRepository.SaveChangesAsync();
        }
    }


    public async Task UpdateStepStatus(long id, StepStatusModel model, long updatedBy)
    {
        bool isExistStatus = Enum.IsDefined(typeof(StepStatusEnum), (int)model.Status);

        if (!isExistStatus) return;

        StepStatus? item = await _stepStatusRepository
            .Select(true)
            .Where(x => x.StepId == id)
            .Where(x => x.ModuleId == model.ModuleId)
            .Where(x => x.TargetId == model.TargetId)
            .FirstOrDefaultAsync();


        if (item is null)
        {
             item = _mapper.Map<StepStatus>(model);
            _stepStatusRepository.Insert(item);
            await _stepStatusRepository.SaveChangesAsync();

        }
        else
        {

            _mapper.Map(model, item);
            await _stepStatusRepository.SaveChangesAsync();
        };


        // ____________ Log ____________

        var step = await _stepRepository.Select().Where(x => x.Id == id).FirstOrDefaultAsync();

        var log = new ActivityLogDto
        {
            Contents = $"Quy trình con {step.Name}",
            Params = JsonConvert.SerializeObject(item),
            Target = "Step",
            TargetCode = item.Id.ToString(),
        };

        switch (model.Status)
        {
            case (short)StepStatusEnum.Draft:

                break;
            case (short)StepStatusEnum.New:

                break;
            case (short)StepStatusEnum.Approve:
                break;
            case (short)StepStatusEnum.Pending:

                break;
            case (short)StepStatusEnum.Denied:

                break;
            default:
                break;
        }

        //await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);

    }
}

