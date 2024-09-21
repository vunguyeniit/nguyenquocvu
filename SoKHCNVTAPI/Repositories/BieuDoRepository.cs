using System;
using System.Reflection.Emit;
using System.Security;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Repositories;

public interface IBieuDoRepository
{
    Task<(IEnumerable<BieuDoMau>?, int)> GetBieuDoMaus(BieuDoMauFilter model);
    Task<BieuDoMau> GetBieuDoMau(long id);
    Task<BieuDoMau> TaoBieuDoMau(BieuDoMauDto model, long createdBy);
    Task CapNhatBieuDoMau(long id, BieuDoMauDto model, long updatedBy);
    Task XoaBieuDoMau(long id, long deletedBy);
    // BIEU DO
    Task<(IEnumerable<BieuDo>?, int)> GetBieuDos(BieuDoFilter model);
    Task<BieuDo> TaoBieuDo(BieuDoDto model, long createdBy);
    Task XoaBieuDo(long id, long deletedBy);
    Task CapNhatBieuDo(long id, BieuDoDto model, long updatedBy);
    Task<BieuDo> GetBieuDo(long id);

    // DU LIEU BIEU DO
    Task<bool> TaoDuLieu(DuLieuBieuDoDto model, long updatedBy);
    Task CapNhatDuLieuBieuDo(long id, DuLieuBieuDoDto model, long updatedBy);
}

public class BieuDoRepository : IBieuDoRepository
{
    private string Label = "Biểu đồ";
    private readonly IMapper _mapper;
    private readonly IRepository<BieuDoMau> _bieuDoMauRepository;
    private readonly IRepository<BieuDo> _bieuDoRepository;
    private readonly IRepository<DuLieuBieuDo> _duLieuBieuDoRepository;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;


    public BieuDoRepository (
        IMapper mapper,
        IRepository<BieuDoMau> bieuDoMauRepository,
        IRepository<BieuDo> bieuDoRepository,
        IRepository<DuLieuBieuDo> duLieuBieuDoRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _bieuDoMauRepository = bieuDoMauRepository;
        _bieuDoRepository = bieuDoRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _duLieuBieuDoRepository = duLieuBieuDoRepository;

    }
    public async Task<BieuDoMau> TaoBieuDoMau(BieuDoMauDto model, long createdBy)
    {
        var query = _bieuDoMauRepository.Select().AsNoTracking();

        var item = await query.FirstOrDefaultAsync(p =>
                p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException($"Mã biểu đồ mẫu đã tồn tại!");

        var newItem = _mapper.Map<BieuDoMau>(model);
        newItem.NgayCapNhat = Utils.getCurrentDate();
        newItem.NguoiCapNhat = createdBy;
        newItem.Ma = model.Ma.ToLower();
        _bieuDoMauRepository.Insert(newItem);

        await _bieuDoMauRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"biểu đồ mẫu với mã #{newItem.Ma}",
            Params = JsonConvert.SerializeObject(newItem),
            Target = "BieuDoMau",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task XoaBieuDoMau(long id, long deletedBy)
    {
        var item = await GetBieuDoMau(id);
        _bieuDoMauRepository.Delete(item);
        await _bieuDoMauRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"biểu đồ mẫu với mã #{item.Ma} thành công.",
            Params = JsonConvert.SerializeObject(item),
            Target = "BieuDoMau",
            TargetCode = item.Ma,
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }

     public async Task<(IEnumerable<BieuDoMau>?, int)> GetBieuDoMaus(BieuDoMauFilter model)
    {
        var query = _bieuDoMauRepository.Select().AsNoTracking();

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p => p.Ma.ToLower().Contains(model.TuKhoa.ToLower()) || p.Ten.ToLower().Contains(model.TuKhoa.ToLower()));
        }

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderBy(p => p.Ma)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<BieuDoMau> GetBieuDoMau(long id)
    {
        BieuDoMau? item = await _bieuDoMauRepository.Select(false).AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            item.BieuDos = await _bieuDoRepository.Select().AsNoTracking().Where(p => p.BieuDoMauId == id).ToListAsync();
        }
        return item == null ? throw new ArgumentException($"Không tìm thấy {Label}!") : item;
    }

    public async Task CapNhatBieuDoMau(long id, BieuDoMauDto model, long updatedBy)
    {
        BieuDoMau item = await GetBieuDoMau(id);

        var isExist = await _bieuDoMauRepository
            .Select(true)
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());

        if (isExist != null) throw new ArgumentException($"Mã biểu đồ mẫu đã tồn tại!");

        _mapper.Map(model, item);
        _bieuDoMauRepository.Update(item);
        await _bieuDoMauRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"biểu đồ mẫu với mã #{item.Ma}",
            Params = JsonConvert.SerializeObject(item),
            Target = "BieuDoMau",
            TargetCode = item.Ma
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    // BIEU DO

    public async Task<BieuDo> TaoBieuDo(BieuDoDto model, long createdBy)
    {
        var newItem = _mapper.Map<BieuDo>(model);
        newItem.NgayCapNhat = Utils.getCurrentDate();
        newItem.NguoiCapNhat = createdBy;
        _bieuDoRepository.Insert(newItem);

        await _bieuDoRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"biểu đồ với mã #{newItem.TieuDe}",
            Params = newItem.Id.ToString(),
            Target = "BieuDo",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task XoaBieuDo(long id, long deletedBy)
    {
        var item = await GetBieuDo(id);
        if(item != null)
        {
            if(item.DuLieuBieuDos != null && item.DuLieuBieuDos.Count > 0)
            {
                foreach(DuLieuBieuDo dlbd in item.DuLieuBieuDos)
                {
                    _duLieuBieuDoRepository.Delete(dlbd);
                }
            }
            _bieuDoRepository.Delete(item);
            await _duLieuBieuDoRepository.SaveChangesAsync();
            await _bieuDoRepository.SaveChangesAsync();

            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"biểu đồ với tiêu đề #{item.TieuDe} thành công.",
                Params = item.Id.ToString(),
                Target = "BieuDo",
                TargetCode = item.Id.ToString(),
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
        } 
    }

    public async Task<(IEnumerable<BieuDo>?, int)> GetBieuDos(BieuDoFilter model)
    {
        var query = _bieuDoRepository.Select().AsNoTracking();

        if (model.Ma != null && model.Ma != "")
        {
            query = query.Where(p => p.Ma.ToLower() == model.Ma.ToLower());
        }

        if (model.TuKhoa != null && model.TuKhoa != "")
        {
            query = query.Where(p => p.TieuDe.ToLower().Contains(model.TuKhoa.ToString().ToLower()) 
            || p.Ma.ToLower().Contains(model.TuKhoa.ToLower()) 
            || p.MoTa.ToLower().Contains(model.TuKhoa.ToLower()));
        }

        query = model.BieuDoMauId > 0 ? query.Where(p => p.BieuDoMauId == model.BieuDoMauId) : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderBy(p => p.NgayCapNhat)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<BieuDo> GetBieuDo(long id)
    {
        BieuDo? item = await _bieuDoRepository.Select(false).AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            item.DuLieuBieuDos = await _duLieuBieuDoRepository.Select().AsNoTracking().Where(p => p.BieuDoId == id).ToListAsync();
        }
        return item == null ? throw new ArgumentException($"Không tìm thấy biểu đồ!") : item;
    }

    public async Task CapNhatBieuDo(long id, BieuDoDto model, long updatedBy)
    {
        BieuDo item = await GetBieuDo(id);

        _mapper.Map(model, item);

        _bieuDoRepository.Update(item);
        item.NguoiCapNhat = updatedBy;
        item.NgayCapNhat = Utils.getCurrentDate();
        await _bieuDoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"biểu đồ với mã #{item.TieuDe}",
            Params = item.Id.ToString(),
            Target = "BieuDo",
            TargetCode = item.Id.ToString()
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    // DU LIEU BIEU DO

    public async Task<bool> TaoDuLieu(DuLieuBieuDoDto model, long updatedBy)
    {
        if (model.BieuDoId > 0)
        {
            BieuDo? bieuDo = await _bieuDoRepository.Select().FirstOrDefaultAsync(p => p.Id == model.BieuDoId);
            if (bieuDo == null) throw new ArgumentException($"Dữ liệu biểu đồ không tìm thấy.");

            DuLieuBieuDo? duLieuBieuDo = await _duLieuBieuDoRepository.Select().AsNoTracking().Where(p => p.MaDuLieu.ToLower() == model.MaDuLieu.ToLower()).FirstOrDefaultAsync();

            if (duLieuBieuDo != null) throw new ArgumentException($"Mã dữ liệu biểu đồ đã tồn tại.");

            var newItem = _mapper.Map<DuLieuBieuDo>(model);
            newItem.NgayCapNhat = Utils.getCurrentDate();
            newItem.NguoiCapNhat = updatedBy;

            _duLieuBieuDoRepository.Insert(newItem);
            await _duLieuBieuDoRepository.SaveChangesAsync();


            var log = new ActivityLogDto
            {
                Contents = $"dữ liệu biểu đồ cho {newItem.MaDuLieu}",
                Params = newItem.MaDuLieu,
                Target = "DuLieuBieuDo",
                TargetCode = newItem.MaDuLieu
            };

            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);

            return true;
        }
        return false;
    }

    public async Task CapNhatDuLieuBieuDo(long id, DuLieuBieuDoDto model, long updatedBy)
    {
        DuLieuBieuDo? item = await _duLieuBieuDoRepository.Select().AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (item == null) throw new ArgumentException($"Dữ liệu biểu đồ không tìm thấy");
        var isExist = await _duLieuBieuDoRepository
            .Select(true)
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.MaDuLieu.ToLower() == model.MaDuLieu.ToLower());

        if (isExist != null) throw new ArgumentException($"Mã dữ liệu biểu đồ đã tồn tại!");

        _mapper.Map(model, item);
        item.NgayCapNhat = Utils.getCurrentDate();
        _duLieuBieuDoRepository.Update(item);
        await _duLieuBieuDoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"{Label} với mã #{item.MaDuLieu}",
            Params = item.MaDuLieu,
            Target = "DuLieuBieuDo",
            TargetCode = item.MaDuLieu
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
}
