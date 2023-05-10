namespace ShipManagement.Infrastructure.Persistence;

using ShipManagement.Application.Persistence;
using ShipManagement.Domain.Entities;
using ShipManagement.Infrastructure.Context;

public class ShipRepository : IShipRepository
    {

        private ShipManagementDbContext _shipManagementDbContext;

        public ShipRepository(ShipManagementDbContext shipManagementDbContext)
        {
            _shipManagementDbContext = shipManagementDbContext;

        }
        public IEnumerable<Ship> GetShips()
        {
            return _shipManagementDbContext.Ships;
        }

        public Ship? GetShipById(Guid id)
        {
            return getShipByIdHelper(id);
        }

        public Ship InsertShip(Ship ship)
        {
            // Check if ship code already exists
            checkIfShipCodeAlreadyExists(ship);

            // Insert the ship object into ships table
            _shipManagementDbContext.Ships.Add(ship);

            // Save changes
            _shipManagementDbContext.SaveChanges();

            return ship;
        }

        public void UpdateShip(Guid id, Ship ship)
        {
            // Check if the ship exists in db
            Ship? oldShip = getShipByIdHelper(id);

            // Update the ship object into ships table
            if (oldShip != null)
            {

                if (oldShip.code != ship.code)
                {
                    // Check if ship code already exists
                    checkIfShipCodeAlreadyExists(ship);
                }

                oldShip.name = ship.name;
                oldShip.length = ship.length;
                oldShip.width = ship.width;
                oldShip.code = ship.code;
                // Save changes
                _shipManagementDbContext.SaveChanges();
            }
        }

        public void DeleteShip(Guid id)
        {
            // Check if the ship exists in db
            Ship? ship = getShipByIdHelper(id);

            if (ship != null)
            {
                // Remove the ship from the table
                _shipManagementDbContext.Ships.Remove(ship);

                // Save changes
                _shipManagementDbContext.SaveChanges();
            }
        }

        // helper methods
        private Ship? getShipByIdHelper(Guid id)
        {
            var ship = _shipManagementDbContext.Ships.Find(id);
            if (ship == null) throw new Exceptions.KeyNotFoundException("The ship with the specified ID was not found");
            return ship;
        }

        private void checkIfShipCodeAlreadyExists(Ship ship)
        {
            if (_shipManagementDbContext.Ships.Any(s => s.code == ship.code))
                throw new InvalidOperationException("A ship with the specified code already exists");
        }
    }