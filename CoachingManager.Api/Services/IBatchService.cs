using CoachingManager.Api.DTOs.Batches;
namespace CoachingManager.Api.Services;

public interface IBatchService
{
    Task<List<BatchResponse>> GetAllAsync(int instituteId);
    Task<BatchResponse> GetByIdAsync(int instituteId, int id);
    Task<BatchResponse> CreateAsync(int instituteId, BatchRequest request);
    Task<BatchResponse> UpdateAsync(int instituteId, int id, BatchRequest request);
    Task DeactivateAsync(int instituteId, int id);

}