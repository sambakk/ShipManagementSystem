namespace ShipManagement.Application.Persistence;

using ShipManagement.Domain.Entities;

public interface IShipRepository
{
    IEnumerable<Ship> GetShips();
    Ship? GetShipById(Guid id);
    Ship InsertShip(Ship ship);
    void UpdateShip(Guid id, Ship ship);
    void DeleteShip(Guid id);
}
