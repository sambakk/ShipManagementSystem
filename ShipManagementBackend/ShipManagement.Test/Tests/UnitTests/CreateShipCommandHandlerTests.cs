namespace ShipManagement.Test.Tests.UnitTests;

using Xunit;
using Moq;
using ShipManagement.Application.ShipService.Commands.CreateShip;
using ShipManagement.Application.Persistence;
using ShipManagement.Domain.Entities;
using AutoMapper;
using System.Threading;
using ShipManagement.Application.Dtos;
using System.ComponentModel.DataAnnotations;

public class CreateShipCommandHandlerTests
{

    private Mock<IShipRepository> _shipRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private CreateShipCommandHandler _handler;

    public CreateShipCommandHandlerTests()
    {
        _shipRepositoryMock = new Mock<IShipRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateShipCommandHandler(_shipRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertShipAndReturnShipDto()
    {
        // Arrange

        var ship = new Ship
        {
            name = "Test Ship",
            length = 100,
            width = 100,
            code = "ABCD-1234-E5"
        };

        var shipDto = new ShipDto
        (Guid.NewGuid(),
            ship.name,
            ship.length,
            ship.width,
            ship.code
        );

        var command = new CreateShipCommand(ship.name, ship.length, ship.width, ship.code);

        _shipRepositoryMock.Setup(repo => repo.InsertShip(It.IsAny<Ship>())).Returns(ship);
        _mapperMock.Setup(mapper => mapper.Map<Ship>(It.IsAny<CreateShipCommand>())).Returns(ship);
        _mapperMock.Setup(mapper => mapper.Map<ShipDto>(It.IsAny<Ship>())).Returns(shipDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(shipDto, result);
        _shipRepositoryMock.Verify(repo => repo.InsertShip(It.IsAny<Ship>()), Times.Once());
    }


    [Fact]
    public async Task Handle_ShouldThrowException_WhenInsertShipFails()
    {
        // Arrange
        var command = new CreateShipCommand("Test Ship", 100, 200, "ABCD-1234-A1");
        var ship = new Ship { name = "Test Ship", length = 100, width = 200, code = "ABCD-1234-A1" };

        _mapperMock.Setup(m => m.Map<Ship>(command)).Returns(ship);
        _shipRepositoryMock.Setup(r => r.InsertShip(ship)).Throws(new Exception("Database Insert Failed"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        _shipRepositoryMock.Verify(r => r.InsertShip(ship), Times.Once);
        _mapperMock.Verify(m => m.Map<Ship>(command), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenShipCodeAlreadyExists()
    {
        // Arrange
        var command = new CreateShipCommand("Test Ship", 100, 200, "ABCD-1234-A1");
        var ship = new Ship { name = "Test Ship", length = 100, width = 200, code = "ABCD-1234-A1" };

        _mapperMock.Setup(m => m.Map<Ship>(command)).Returns(ship);
        _shipRepositoryMock.Setup(r => r.InsertShip(ship)).Throws(new InvalidOperationException("A ship with the specified code already exists"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        _shipRepositoryMock.Verify(r => r.InsertShip(ship), Times.Once);
        _mapperMock.Verify(m => m.Map<Ship>(command), Times.Once);
    }

    [Fact]
    public void Handle_ShouldThrowException_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new CreateShipCommand("name", 1, 1, "AAAA-8768-Y5"); // Invalid command

        // Act & Assert
        Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }


}
