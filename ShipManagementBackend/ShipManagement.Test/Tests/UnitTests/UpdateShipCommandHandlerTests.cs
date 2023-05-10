namespace ShipManagement.Test.Tests;

using Moq;
using ShipManagement.Domain.Entities;
using ShipManagement.Application.Persistence;
using ShipManagement.Application.ShipService.Commands.UpdateShip;
using AutoMapper;

public class UpdateShipCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IShipRepository> _mockRepository;

    public UpdateShipCommandHandlerTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new ShipManagement.Application.Profiles.ShipProfile()));
        _mapper = config.CreateMapper();
        _mockRepository = new Mock<IShipRepository>();
    }

    [Fact]
    public async void Handle_ValidShipUpdate_ShipIsUpdated()
    {
        // Arrange
        var ship = new Ship
        {
            id = Guid.NewGuid(),
            name = "Old Name",
            length = 100,
            width = 50,
            code = "ABCD-1234-A1"
        };

        _mockRepository.Setup(repo => repo.GetShipById(ship.id)).Returns(ship);
        var command = new UpdateShipCommand(ship.id, "New Name", 200, 100, "EFGH-5678-B2");
        var handler = new UpdateShipCommandHandler(_mockRepository.Object, _mapper);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(repo => repo.UpdateShip(ship.id, It.Is<Ship>(s => s.name == "New Name" && s.length == 200 && s.width == 100 && s.code == "EFGH-5678-B2")), Times.Once);
    }

    [Fact]
    public async void Handle_InvalidShipId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        var ship = new Ship
        {
            id = Guid.NewGuid(),
            name = "Old Name",
            length = 100,
            width = 50,
            code = "ABCD-1234-A1"
        };
        _mockRepository.Setup(repo => repo.UpdateShip(invalidId, It.IsAny<Ship>())).Throws<KeyNotFoundException>();
        var command = new UpdateShipCommand(invalidId, "New Name", 200, 100, "EFGH-5678-B2");
        var handler = new UpdateShipCommandHandler(_mockRepository.Object, _mapper);


        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        _mockRepository.Verify(repo => repo.UpdateShip(invalidId, It.IsAny<Ship>()), Times.Once);

    }
}