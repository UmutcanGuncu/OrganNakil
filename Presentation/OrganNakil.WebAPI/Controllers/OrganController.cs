using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganNakil.Application.Mediatr.Commands.OrganCommands;

namespace OrganNakil.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class OrganController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrganController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrgan(CreateOrganCommand createOrganCommand)
    {
        var value = await _mediator.Send(createOrganCommand);
        if (value.Code == "Success")
        {
            return Ok(value);
        }

        return BadRequest(value);
    }
}