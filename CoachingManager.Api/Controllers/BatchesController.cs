using CoachingManager.Api.DTOs.Batches;
using CoachingManager.Api.Extensions;
using CoachingManager.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachingManager.Api.Controllers;

[ApiController]
[Route("api/batches")]
[Authorize]
public class BatchesController : ControllerBase
{
    private readonly IBatchService _batchService;

    public BatchesController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var batches = await _batchService.GetAllAsync(User.GetInstituteId());
        return Ok(batches);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var batch = await _batchService.GetByIdAsync(User.GetInstituteId(), id);
            return Ok(batch);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Create([FromBody] BatchRequest request)
    {
        try
        {
            var batch = await _batchService.CreateAsync(User.GetInstituteId(), request);
            return CreatedAtAction(nameof(GetById), new { id = batch.Id }, batch);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Update(int id, [FromBody] BatchRequest request)
    {
        try
        {
            var batch = await _batchService.UpdateAsync(User.GetInstituteId(), id, request);
            return Ok(batch);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Deactivate(int id)
    {
        try
        {
            await _batchService.DeactivateAsync(User.GetInstituteId(), id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}