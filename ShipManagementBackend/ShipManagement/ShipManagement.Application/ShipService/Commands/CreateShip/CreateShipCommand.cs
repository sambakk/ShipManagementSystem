using System.ComponentModel.DataAnnotations;
using MediatR;
using ShipManagement.Application.Constants;
using ShipManagement.Application.Dtos;

namespace ShipManagement.Application.ShipService.Commands.CreateShip;

public record CreateShipCommand(
        [Required] string name,
        [Required][Range(1, double.MaxValue, ErrorMessage = ValidationMessages.shipLengthValidationMessage)] double length,
        [Required][Range(1, double.MaxValue, ErrorMessage = ValidationMessages.shipWidthValidationMessage)] double width,
        [Required][RegularExpression(@"^[A-Za-z]{4}-\d{4}-[A-Za-z]\d$", ErrorMessage = ValidationMessages.shipCodeValidationMessage)] string code): IRequest<ShipDto>;