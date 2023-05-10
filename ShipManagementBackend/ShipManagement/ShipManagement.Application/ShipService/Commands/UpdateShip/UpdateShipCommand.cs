using System.ComponentModel.DataAnnotations;
using MediatR;
using ShipManagement.Application.Constants;

namespace ShipManagement.Application.ShipService.Commands.UpdateShip;

public record UpdateShipCommand(
    [Required] Guid id,
    string name,
    [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.shipLengthValidationMessage)] double length,
    [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.shipWidthValidationMessage)] double width,
    [RegularExpression(@"^[A-Za-z]{4}-\d{4}-[A-Za-z]\d$", ErrorMessage = ValidationMessages.shipCodeValidationMessage)] string code) : IRequest<Unit>;
