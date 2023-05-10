using System.ComponentModel.DataAnnotations;
using MediatR;
using ShipManagement.Application.Dtos;

namespace ShipManagement.Application.ShipService.Queries.GetAllShips;

public record GetAllShipsQuery() : IRequest<IEnumerable<ShipDto>>;