using CoachingManager.Api.Data;
using CoachingManager.Api.DTOs.Batches;
using CoachingManager.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachingManager.Api.Services;

public class BatchService : IBatchService
{
    private readonly AppDbContext _db;

    public BatchService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<BatchResponse>> GetAllAsync(int instituteId)
    {
        var query = _db.Batches
            .AsNoTracking()
            .Where(b => b.InstituteId == instituteId)
            .OrderBy(b => b.Name);

        return await ToResponse(query).ToListAsync();
    }

    public async Task<BatchResponse> GetByIdAsync(int instituteId, int id)
    {
        var query = _db.Batches
            .AsNoTracking()
            .Where(b => b.InstituteId == instituteId && b.Id == id);

        var batch = await ToResponse(query).FirstOrDefaultAsync();

        return batch ?? throw new KeyNotFoundException("Batch not found.");
    }

    public async Task<BatchResponse> CreateAsync(int instituteId, BatchRequest request)
    {
        await ValidateAsync(instituteId, request);

        var batch = new Batch
        {
            InstituteId = instituteId,
            Name = request.Name.Trim(),
            Subject = request.Subject.Trim(),
            ScheduleDays = request.ScheduleDays.Trim(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            MonthlyFee = request.MonthlyFee,
            TeacherId = request.TeacherId
        };

        _db.Batches.Add(batch);
        await _db.SaveChangesAsync();

        return await GetByIdAsync(instituteId, batch.Id);
    }

    public async Task<BatchResponse> UpdateAsync(int instituteId, int id, BatchRequest request)
    {
        var batch = await _db.Batches
            .FirstOrDefaultAsync(b => b.Id == id && b.InstituteId == instituteId)
            ?? throw new KeyNotFoundException("Batch not found.");

        await ValidateAsync(instituteId, request);

        batch.Name = request.Name.Trim();
        batch.Subject = request.Subject.Trim();
        batch.ScheduleDays = request.ScheduleDays.Trim();
        batch.StartTime = request.StartTime;
        batch.EndTime = request.EndTime;
        batch.MonthlyFee = request.MonthlyFee;
        batch.TeacherId = request.TeacherId;

        await _db.SaveChangesAsync();

        return await GetByIdAsync(instituteId, id);
    }

    public async Task DeactivateAsync(int instituteId, int id)
    {
        var batch = await _db.Batches
            .FirstOrDefaultAsync(b => b.Id == id && b.InstituteId == instituteId)
            ?? throw new KeyNotFoundException("Batch not found.");

        batch.IsActive = false;
        await _db.SaveChangesAsync();
    }

    private async Task ValidateAsync(int instituteId, BatchRequest request)
    {
        if (request.EndTime <= request.StartTime)
            throw new InvalidOperationException("End time must be later than start time.");

        if (request.TeacherId is not null)
        {
            var teacherOk = await _db.Users.AnyAsync(u =>
                u.Id == request.TeacherId
                && u.InstituteId == instituteId
                && u.IsActive);

            if (!teacherOk)
                throw new InvalidOperationException("Teacher not found in your institute.");
        }
    }

    private static IQueryable<BatchResponse> ToResponse(IQueryable<Batch> query)
    {
        return query.Select(b => new BatchResponse
        {
            Id = b.Id,
            Name = b.Name,
            Subject = b.Subject,
            ScheduleDays = b.ScheduleDays,
            StartTime = b.StartTime,
            EndTime = b.EndTime,
            MonthlyFee = b.MonthlyFee,
            TeacherId = b.TeacherId,
            TeacherName = b.Teacher != null ? b.Teacher.Name : null,
            StudentCount = b.StudentBatches.Count(sb => sb.LeftOn == null),
            IsActive = b.IsActive
        });
    }
}