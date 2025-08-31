using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace AudioBooksLibrary.Api.Controllers;

[ApiController]
[Route("api/markers")]
public class MarkersController(IMarkerService service) : ControllerBase
{
    private Guid GetUserId() =>
        Guid.TryParse(Request.Headers["X-Demo-UserId"], out var id) ? id : Guid.Parse("00000000-0000-0000-0000-000000000001");

    [HttpGet("book/{bookId:guid}")]
    public async Task<ActionResult<PagedResult<TimelineMarkerDto>>> GetByBook(Guid bookId, [FromQuery] Guid? partId, [FromQuery] int skip = 0, [FromQuery] int take = 100, CancellationToken ct = default)
    {
        var res = await service.GetByBookAsync(bookId, partId, skip, Math.Clamp(take, 1, 200), ct);
        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<TimelineMarkerDto>> Create([FromBody] CreateMarkerRequest request, CancellationToken ct)
    {
        var res = await service.CreateAsync(GetUserId(), request, ct);
        return CreatedAtAction(nameof(GetByBook), new { bookId = res.AudiobookId }, res);
    }

    [HttpPost("{markerId:guid}/like")]
    public async Task<ActionResult> Like(Guid markerId, CancellationToken ct)
    {
        var ok = await service.LikeAsync(GetUserId(), markerId, ct);
        return ok ? NoContent() : Conflict("Already liked");
    }
}