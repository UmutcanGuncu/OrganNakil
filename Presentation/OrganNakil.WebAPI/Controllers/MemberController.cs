using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganNakil.Application.Mediatr.Queries.MemberQueries;

namespace OrganNakil.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class MemberController : ControllerBase
{
    private IMediator _mediator;

    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getUserInfo/{id}")]
    public async Task<IActionResult> GetUserInfo(Guid id)
    {
        var value = await _mediator.Send(new GetMemberByIdQuery(id));
        return Ok(value);
    }
}