namespace ShipManagement.Controllers.V1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.ShipService.Commands.CreateShip;
using ShipManagement.Application.ShipService.Commands.UpdateShip;
using ShipManagement.Application.ShipService.Commands.DeleteShip;
using ShipManagement.Application.ShipService.Queries.GetAllShips;
using ShipManagement.Application.ShipService.Queries.GetShipById;
using ShipManagement.Controllers.Constants;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.ShipApiRoute)]
public class ShipController : ControllerBase
{

    private readonly ISender _mediator;
    public ShipController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetAllShipsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipDto>> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetShipByIdQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<ShipDto>> Post(CreateShipCommand createShipCommand)
    {
        var res = await _mediator.Send(createShipCommand);
        return CreatedAtAction(nameof(GetById), new { id = res.id }, res);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateShipCommand updateShipCommand)
    {
        await _mediator.Send(new UpdateShipCommand(
            id,
            updateShipCommand.name,
            updateShipCommand.length,
            updateShipCommand.width, updateShipCommand.code));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteShipCommand(id));
        return NoContent();
    }

}