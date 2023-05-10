namespace ShipManagement.Test.Tests.IntegrationTests;

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShipManagement.Domain.Entities;
using ShipManagement.Infrastructure.Context;
using ShipManagement.Infrastructure.Persistence;
using ShipManagement.Infrastructure.Exceptions;
using Xunit;

public class ShipRepositoryTests : IDisposable
{
    private readonly ShipRepository _shipRepository;
    private readonly ShipManagementDbContext _dbContext;

    public ShipRepositoryTests()
    {
        // In-memory database for testing
        var options = new DbContextOptionsBuilder<ShipManagementDbContext>()
            .UseInMemoryDatabase(databaseName: "ShipManagementDbTest")
            .Options;

        _dbContext = new ShipManagementDbContext(options);
        _shipRepository = new ShipRepository(_dbContext);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public void GetShips_ReturnsAllShips()
    {
        // Arrange
        SeedShips();

        // Act
        var ships = _shipRepository.GetShips();

        // Assert
        Assert.Equal(2, ships.Count());
    }

    [Fact]
    public void GetShipById_ReturnsShip()
    {
        // Arrange
        var shipId = Guid.NewGuid();
        var ship = new Ship
        {
            id = shipId,
            name = "Emma Maersk",
            length = 397.7,
            width = 56.4,
            code = "EMRK-2317-D5"
        };
        _dbContext.Ships.Add(ship);
        _dbContext.SaveChanges();

        // Act
        var result = _shipRepository.GetShipById(shipId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(shipId, result?.id);
    }

    [Fact]
    public void GetShipById_ThrowsKeyNotFoundException_WhenShipNotFound()
    {
        // Arrange
        var shipId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _shipRepository.GetShipById(shipId));
    }

    [Fact]
    public void InsertShip_AddsShipToDatabase()
    {
        // Arrange
        var ship = new Ship
        {
            id = Guid.NewGuid(),
            name = "Test Ship",
            length = 300.0,
            width = 50.0,
            code = "TSTS-1234-X1"
        };

        // Act
        _shipRepository.InsertShip(ship);
        var insertedShip = _shipRepository.GetShipById(ship.id);

        // Assert
        Assert.NotNull(insertedShip);
        Assert.Equal(ship.id, insertedShip?.id);
    }

    [Fact]
    public void InsertShip_ThrowsInvalidOperationException_WhenShipCodeExists()
    {
        // Arrange
        SeedShips();
        var ship = new Ship
        {
            id = Guid.NewGuid(),
            name = "Test Ship",
            length = 300.0,
            width = 50.0,
            code = "EMRK-2317-D5" // Existing ship code
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _shipRepository.InsertShip(ship));
    }

    [Fact]
    public void UpdateShip_UpdatesShipProperties()
    {
        // Arrange
        SeedShips();
        var shipId = _dbContext.Ships.First().id;
        var updatedShip = new Ship
        {
            id = shipId,
            name = "Updated Ship",
            length = 400.0,
            width = 60.0,
            code = "UPDT-9876-Y1"
        };

        // Act
        _shipRepository.UpdateShip(shipId, updatedShip);
        var result = _shipRepository.GetShipById(shipId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedShip.name, result?.name);
        Assert.Equal(updatedShip.length, result?.length);
        Assert.Equal(updatedShip.width, result?.width);
        Assert.Equal(updatedShip.code, result?.code);
    }

    [Fact]
    public void DeleteShip_RemovesShipFromDatabase()
    {
        // Arrange
        SeedShips();
        var shipId = _dbContext.Ships.First().id;

        // Act
        _shipRepository.DeleteShip(shipId);
        var result = _dbContext.Ships.Find(shipId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteShip_ThrowsKeyNotFoundException_WhenShipNotFound()
    {
        // Arrange
        var shipId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _shipRepository.DeleteShip(shipId));
    }

    [Fact]
    public void UpdateShip_ThrowsInvalidOperationException_WhenUpdatedShipCodeExists()
    {
        // Arrange
        SeedShips();
        var shipId = _dbContext.Ships.First().id;
        var existingShipCode = _dbContext.Ships.Skip(1).First().code;
        var updatedShip = new Ship
        {
            id = shipId,
            name = "Updated Ship",
            length = 400.0,
            width = 60.0,
            code = existingShipCode // Existing ship code
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _shipRepository.UpdateShip(shipId, updatedShip));
    }

    [Fact]
    public void UpdateShip_AllowsUpdatingShipWithSameCode()
    {
        // Arrange
        SeedShips();
        var shipId = _dbContext.Ships.First().id;
        var existingShipCode = _dbContext.Ships.First().code;
        var updatedShip = new Ship
        {
            id = shipId,
            name = "Updated Ship",
            length = 400.0,
            width = 60.0,
            code = existingShipCode // Same ship code
        };

        // Act
        _shipRepository.UpdateShip(shipId, updatedShip);
        var result = _shipRepository.GetShipById(shipId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedShip.name, result?.name);
        Assert.Equal(updatedShip.length, result?.length);
        Assert.Equal(updatedShip.width, result?.width);
        Assert.Equal(updatedShip.code, result?.code);
    }

    private void SeedShips()
    {
        _dbContext.Ships.Add(new Ship
        {
            id = Guid.NewGuid(),
            name = "Emma Maersk",
            length = 397.7,
            width = 56.4,
            code = "EMRK-2317-D5"
        });
        _dbContext.Ships.Add(new Ship
        {
            id = Guid.NewGuid(),
            name = "MSC Gülsün",
            length = 399.9,
            width = 61.5,
            code = "MSCG-1930-Z2"
        });

        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}