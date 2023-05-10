namespace ShipManagement.Application.Dtos;

public record ShipDto(Guid id, string name, double length, double width, string code);
