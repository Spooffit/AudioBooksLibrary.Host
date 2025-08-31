using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace AudioBooksLibrary.Api.Controllers;

[ApiController]
[Route("api/progress")]
public class ProgressController(IPlaybackService service) : ControllerBase
{
    private Guid GetUserId() =>
        Guid.TryParse(Request.Headers["X-Demo-UserId"], out var id) ? id : Guid.Parse("00000000-0000-0000-0000-000000000001");

    [HttpGet("{bookId:guid}")]
    public async Task<ActionResult<PlaybackProgressDto>> Get(Guid bookId, CancellationToken ct)
    {
        var res = await service.GetAsync(GetUserId(), bookId, ct);
        return res is null ? NotFound() : Ok(res);
    }

    [HttpPut]
    public async Task<ActionResult<PlaybackProgressDto>> Upsert([FromBody] UpdateProgressRequest request, CancellationToken ct)
    {
        var res = await service.UpsertAsync(GetUserId(), request, ct);
        return Ok(res);
    }
}