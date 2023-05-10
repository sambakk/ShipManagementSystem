using System.ComponentModel.DataAnnotations;
using MediatR;
using ShipManagement.Application.Dtos;

namespace ShipManagement.Application.ShipService.Queries.GetShipById;

public record GetShipByIdQuery(
        [Required] Guid id) : IRequest<ShipDto>;