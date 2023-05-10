using System.ComponentModel.DataAnnotations;
using MediatR;
using ShipManagement.Application.Constants;

namespace ShipManagement.Application.ShipService.Commands.DeleteShip;

public record DeleteShipCommand(
    [Required] Guid id
    ) : IRequest<Unit>;
