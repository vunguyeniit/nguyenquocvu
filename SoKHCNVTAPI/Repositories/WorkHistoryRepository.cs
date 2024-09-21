using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Repositories;

public interface IWorkHistoryRepository
{
    Task<(IEnumerable<WorkHistory>?, int)> PagingAsync(PaginationDto model);
    Task<WorkHistory> GetByIdAsync(long id);
    Task<WorkHistory> Create(WorkHistoryDto model);
    Task Update(long id, WorkHistoryDto patch);
    Task Delete(long id);
}

public class WorkHistoryRepository : IWorkHistoryRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public WorkHistoryRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<WorkHistory>?, int)> PagingAsync(PaginationDto model)
    {
        var query = _context
            .Set<WorkHistory>()
            .AsNoTracking();

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Id)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var total = await query.CountAsync();

        return (items, total);
    }

    public async Task<WorkHistory> GetByIdAsync(long id)
    {
        var item = await _context
            .Set<WorkHistory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return item;
    }

    public async Task<WorkHistory> Create(WorkHistoryDto model)
    {

        // check OfficerId
        var officer = await _context
            .Set<CanBo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.OfficerId);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        var item = _mapper.Map<WorkHistory>(model);
        _context.Set<WorkHistory>().Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task Update(long id, WorkHistoryDto model)
    {
        var query = _context
            .Set<WorkHistory>()
            .AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // check OfficerId
        var officer = await _context
            .Set<CanBo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.OfficerId);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        _mapper.Map(model, item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var query = _context
            .Set<WorkHistory>()
            .AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // pass
        _context.Set<WorkHistory>().Remove(item);
        await _context.SaveChangesAsync();
    }
}