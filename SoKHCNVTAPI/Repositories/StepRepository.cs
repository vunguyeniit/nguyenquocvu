using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models.Base;

using Action = SoKHCNVTAPI.Entities.Action;
using StepHasStatus = SoKHCNVTAPI.Entities.StepHasStatus;

namespace SoKHCNVTAPI.Repositories;

public interface IStepRepository
{
    Task<(IEnumerable<Step>?, int)> FilterAsync(StepFilter model);
    Task<Step> GetByIdAsync(long id);
    Task CreateAsync(StepModel model, long createdBy);
    Task UpdateAsync(long id, StepModel model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
    Task UpdateStepAction(long id, List<ActionStepDto> actions, StepModel model);
    Task UpdateStepUser(long id, List<long> users, StepModel model);
    Task UpdateStepHasStatus(long id, List<string> statuses, StepModel model);
    Task<IEnumerable<ActionResponse>> GetActions(long stepId);
}

public class StepRepository : IStepRepository
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Action> _actionRepository;
    private readonly IRepository<Step> _stepRepository;
    private readonly IRepository<StepUser> _stepUserRepository;
    private readonly IRepository<StepAction> _stepActionRepository;
    private readonly IRepository<StepHasStatus> _stepHasStatusRepository;
    private readonly IStepStatusRepository _stepStatusRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "Quy trình con";

    public StepRepository(
            IMapper mapper,
            IUserRepository userRepository,
            IRepository<Action> actionRepository,
            IRepository<Step> stepRepository,
            IRepository<StepUser> stepUserRepository,
            IRepository<StepAction> stepActionRepository,
            IRepository<StepHasStatus> stepHasStatusRepository,
            IStepStatusRepository stepStatusRepository,
            IActivityLogRepository activityLogRepository
        )
	{
        _mapper = mapper;
        _userRepository = userRepository;
        _actionRepository = actionRepository;
        _stepRepository = stepRepository;
        _stepHasStatusRepository = stepHasStatusRepository;
        _stepActionRepository = stepActionRepository;
        _stepUserRepository = stepUserRepository;
        _activityLogRepository = activityLogRepository;
        _stepStatusRepository = stepStatusRepository;
    }

    public async Task CreateAsync(StepModel model, long createdBy)
    {

        var newItem = _mapper.Map<Step>(model);
        _stepRepository.Insert(newItem);

       await _stepRepository.SaveChangesAsync();

        var StepId = newItem.Id;

        if(StepId == 0)
        {
            return;
        }

        List<ActionStepDto>? actionList = model.Action;
        List<string>? statusList = model.Statuses;
        //List<long>? userList = model.UserId;

        if (actionList?.Count > 0)
        {
            foreach (ActionStepDto action in actionList)
            {
                List<long>? users = action.UserId;
                if (users != null)
                {
                    foreach (long user in users)
                    {
                        var isCheckUser = await _userRepository.GetByIdAsync(user);
                        if (isCheckUser is null)
                        {
                            break;
                        }
                        var actionStep = new StepActionModel
                        {
                            StepId = StepId,
                            RoleId = 0,
                            ActionId = action.Id,
                            UserId = user,
                            Status = 1,
                        };
                        var stepActon = _mapper.Map<StepAction>(actionStep);
                        _stepActionRepository.Insert(stepActon);
                        await _stepActionRepository.SaveChangesAsync();

                    }
                }

            }
        }

        //if (userList?.Count > 0)
        //{
        //    foreach (long user in userList)
        //    {
        //        //var isCheckUser = await _userRepository.GetByIdAsync(user);
        //        //if (isCheckUser is null)
        //        //{
        //        //    break;
        //        //}
        //        var actionStep = new StepUserModel
        //        {
        //            StepId = StepId,
        //            UserId = user,
        //            Status = 1
        //        };
        //        var stepUser = _mapper.Map<StepUser>(actionStep);
        //        _stepUserRepository.Insert(stepUser);
        //        await _stepUserRepository.SaveChangesAsync();
        //    }
        //}


        if(statusList.Count > 0)
        {
            foreach(string status in statusList)
            {
                var stepHasStatusModel = new StepHasStatusModel { Name = status, StepId = StepId, Status = 1 };

                var newStepHasStatus = _mapper.Map<StepHasStatus>(stepHasStatusModel);
                _stepHasStatusRepository.Insert(newStepHasStatus);
                await _stepHasStatusRepository.SaveChangesAsync();

            }

        }




        //// ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"{Label} {newItem.Name}",
            Params = JsonConvert.SerializeObject(newItem),
            Target = "Step",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id);
        _stepRepository.Delete(item);
        await _stepRepository.SaveChangesAsync();

        await _stepStatusRepository.Delete(item.Id, item.ModuleId);

        // ____________ Action ____________


        List<StepAction> stepActions = await _stepActionRepository.Select().Where(x => x.StepId == item.Id).ToListAsync();

        foreach(StepAction stepAction in stepActions)
        {
            _stepActionRepository.Delete(stepAction);
           await _stepActionRepository.SaveChangesAsync();
        }


        // ____________ User ____________



        List<StepUser> stepUsers = await _stepUserRepository.Select().Where(x => x.StepId == item.Id).ToListAsync();

        foreach (StepUser stepUser in stepUsers)
        {
            _stepUserRepository.Delete(stepUser);
          await  _stepUserRepository.SaveChangesAsync();
        }

        // ____________ Log ____________



        var log = new ActivityLogDto
        {
            Contents = $"{Label} {item.Name}",
            Params = JsonConvert.SerializeObject(item),
            Target = "Step",
            TargetCode = item.Id.ToString(),
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }

    public async Task<(IEnumerable<Step>?, int)> FilterAsync(StepFilter model)
    {
        var query =
            _stepRepository
            .Select()
            .Where(p => p.IsDeleted == false);

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(p =>
                p.Tag.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Name.ToLower().Contains(model.Keyword.ToLower()));
        }



        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;


        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .Where(x => x.ModuleId == model.ModuleId)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<Step> GetByIdAsync(long id)
    {
        var query = _stepRepository
            .Select(true)
            .Include(x => x.StepStatus)
            .Include(X => X.StatusList)
            .Where(p => p.IsDeleted == false);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task UpdateAsync(long id, StepModel model, long updatedBy)
    {
        var item = await GetByIdAsync(id);
        var isExist = await _stepRepository
            .Select(true)
            .Where(p => p.IsDeleted == false)
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower() == model.Name.ToLower() ||
                p.Tag.ToLower() == model.Tag.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");


        var newModel = new StepModel
        {
            ModuleId = model.ModuleId,
            Name = model.Name,
            Description = model.Description,
            Status = 1,
            RoleId = 0,
            Tag = model.Tag
        };

       var updatedStep =  _mapper.Map(newModel, item);
       await _stepRepository.SaveChangesAsync();


        List<ActionStepDto>? actionList = model.Action;
        List<long>? userList = model.UserId;
        
        if (actionList != null)
        {
            await UpdateStepAction(id, actionList, model);
        }

        List<string> statuses = model.Statuses;
        if (statuses != null)
        {
            await UpdateStepHasStatus(id, statuses, model);
        }

        //await UpdateStepUser(id, userList,model);


        // ____________ Log ____________

        var log = new ActivityLogDto
        {
            Contents = $"{Label} {item.Name}",
            Params = JsonConvert.SerializeObject(item),
            Target = "Step",
            TargetCode = item.Id.ToString(),
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task UpdateStepAction(long StepId , List<ActionStepDto> actions, StepModel model)
    {
        List<StepAction> stepActionList = await _stepActionRepository.Select().Where(x => x.StepId == StepId).ToListAsync();


        var oldList = new List<long>();
        var newList = new List<long>();

        foreach(StepAction action in stepActionList)
        {
            oldList.Add(action.ActionId);
        }

        foreach(ActionStepDto actionStep in actions)
        {
            newList.Add(actionStep.Id);
        }

        ResultTwoValCompare<long> checkUpdateAction = Utils.CompareTwoDiffArray(oldList, newList);

        if (checkUpdateAction.firstArray.Count > 0) 
        {
            foreach(long item in checkUpdateAction.firstArray)
            {
                var stepActionItem = await _stepActionRepository.Select(true).FirstOrDefaultAsync(x => x.ActionId == item);

                if(stepActionItem != null)
                {
                    _stepActionRepository.Delete(stepActionItem);
                    await _stepActionRepository.SaveChangesAsync();
                }
                
            }


            stepActionList = stepActionList.Where(x => !checkUpdateAction.firstArray.Contains(x.ActionId)).ToList();

        }


        // Update RoleId


        //List<StepAction> checkRoleIdChange = stepActionList.FindAll(x => x.RoleId == model.RoleId).ToList();

        //if (checkRoleIdChange.Count <= 0)
        //{
        //    foreach (StepAction it in stepActionList)
        //    {
        //        StepAction? stepAction = await _stepActionRepository.Select(true).Where(x => x.Id == it.Id).SingleOrDefaultAsync();
        //        stepAction.RoleId = model.RoleId;

        //       await _stepActionRepository.SaveChangesAsync();
        //    }
        //}


        if (checkUpdateAction.secondArray.Count > 0)
        {
            foreach(long item in checkUpdateAction.secondArray)
            {
                var currentAction = actions.Find(x => x.Id == item);
                var users = currentAction.UserId;

                foreach (long user in users)
                {
                    var actionStep = new StepActionModel
                    {
                        StepId = StepId,
                        RoleId = 0,
                        ActionId = item,
                        UserId = user,
                        Status = 1,
                    };
                    var stepActon = _mapper.Map<StepAction>(actionStep);
                    _stepActionRepository.Insert(stepActon);
                    await _stepActionRepository.SaveChangesAsync();
                }

            }
            
        }


    }

    public async Task UpdateStepUser(long StepId, List<long> users, StepModel model)
    {
        List<StepUser> stepUserList = await _stepUserRepository.Select().Where(x => x.StepId == StepId).ToListAsync();

        var oldList = new List<long>();
        var newList = new List<long>();

        foreach (StepUser action in stepUserList)
        {
            oldList.Add(action.UserId);
        }

        foreach (long actionStep in users)
        {
            newList.Add(actionStep);
        }

        ResultTwoValCompare<long> checkUpdateUser = Utils.CompareTwoDiffArray(oldList, newList);

        if (checkUpdateUser.firstArray.Count > 0)
        {
            foreach (long item in checkUpdateUser.firstArray)
            {
                var stepActionItem = await _stepUserRepository.Select(true).FirstOrDefaultAsync(x => x.UserId == item);

                if (stepActionItem != null)
                {
                    _stepUserRepository.Delete(stepActionItem);
                    await _stepUserRepository.SaveChangesAsync();
                }

            }
        }

        if (checkUpdateUser.secondArray.Count > 0)
        {
            foreach (long item in checkUpdateUser.secondArray)
            {

               var actionStep = new StepUserModel
                {
                    StepId = StepId,
                    UserId = item,
                    Status = 1
                };
                var stepUser = _mapper.Map<StepUser>(actionStep);
                _stepUserRepository.Insert(stepUser);
                await _stepUserRepository.SaveChangesAsync();
            }
        }
    }


    public async Task UpdateStepHasStatus(long StepId, List<string> statuses, StepModel model)
    {
        List<StepHasStatus> stepStatusList = await _stepHasStatusRepository.Select().Where(x => x.StepId == StepId).ToListAsync();

        var oldList = new List<string>();
        var newList = new List<string>();

        foreach (StepHasStatus status in stepStatusList)
        {
            oldList.Add(status.Name);
        }

        foreach (string status in statuses)
        {
            newList.Add(status);
        }

        ResultTwoValCompare<string> checkUpdateUser = Utils.CompareTwoDiffArray(oldList, newList);

        if (checkUpdateUser.firstArray.Count > 0)
        {
            foreach (string item in checkUpdateUser.firstArray)
            {
                var stepActionItem = await _stepHasStatusRepository.Select(true).FirstOrDefaultAsync(x => x.Name == item);

                if (stepActionItem != null)
                {
                    _stepHasStatusRepository.Delete(stepActionItem);
                    await _stepHasStatusRepository.SaveChangesAsync();
                }

            }
        }

        if (checkUpdateUser.secondArray.Count > 0)
        {
            foreach (string item in checkUpdateUser.secondArray)
            {

                var actionStep = new StepHasStatusModel
                {
                    StepId = StepId,
                    Name = item,
                    Status = 1
                };
                var stepUser = _mapper.Map<StepHasStatus>(actionStep);
                _stepHasStatusRepository.Insert(stepUser);
                await _stepHasStatusRepository.SaveChangesAsync();

            }

        }

    }

    public async Task<IEnumerable<ActionResponse>> GetActions(long stepId)
    {
        var actions = new List<ActionResponse>();

        List<StepAction> stepActions = await _stepActionRepository.Select().Where(x => x.StepId == stepId).ToListAsync();
        foreach (StepAction stepAction in stepActions)
        {
            if (stepAction != null)
            {
                var action = await _actionRepository.Select().SingleOrDefaultAsync(x => x.Id == stepAction.ActionId);
                if (action is null) break;

                var isCheckActionExist = actions.Find(x => x.Id == action.Id);

                if (isCheckActionExist is null)
                {
                    if (stepAction.UserId > 0)
                    {
                        UserResponse? user = await _userRepository.GetByIdAsync((long)stepAction.UserId);
                        var users = new List<UserResponse>();

                        if (user != null)
                        {
                            users.Add(user);
                        }
                        var actionResponse = new ActionResponse { Id = action.Id, Name = action.Name, Description = action.Description, Tag = action.Tag ?? "", Status = action.Status, Users = users };
                        actions.Add(actionResponse);
                    }
                }
                else
                {
                    UserResponse? user = await _userRepository.GetByIdAsync((long)stepAction.UserId);
                    var users = new List<UserResponse>();


                    if (user != null)
                    {
                        users.Add(user);
                        isCheckActionExist.Users.Add(user);
                    }
                }
            }
        }
        return actions;
    }
}

