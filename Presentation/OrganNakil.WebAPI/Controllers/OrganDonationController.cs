using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganNakil.Application.Interfaces;
using OrganNakil.Application.Mediatr.Commands.OrganDonationRequestCommand;
using OrganNakil.Application.Mediatr.Queries.OrganDonationRequestQueries;

namespace OrganNakil.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class OrganDonationController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrganDonationController(IMediator mediator)
    {
        _mediator = mediator;
        
    }

    [HttpPost]
    public async Task<IActionResult> AddOrganDonation(CreateOrganDonationRequestCommand organDonationRequestCommand)
    {
        var value = await _mediator.Send(organDonationRequestCommand);
        if (value.Code == "Success")
        {
            return Ok(value);
        }
        return BadRequest(value);
    }

    [HttpGet("activeAllDonationList")]
    public async Task<IActionResult> GetActiveOrganDonationList()
    {
        var values = await _mediator.Send(new GetActiveOrganDonationRequestQuery());
        return Ok(values);
    }
}