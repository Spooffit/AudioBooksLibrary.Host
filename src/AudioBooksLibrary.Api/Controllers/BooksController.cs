using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace AudioBooksLibrary.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(IAudiobookService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<AudiobookDto>>> List([FromQuery] int skip = 0, [FromQuery] int take = 50, CancellationToken ct = default)
    {
        var result = await service.ListAsync(skip, Math.Clamp(take, 1, 200), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AudiobookDetailsDto>> Get(Guid id, CancellationToken ct)
    {
        var dto = await service.GetAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}