namespace ShipManagement.Test.Tests.UnitTests;

using Moq;
using ShipManagement.Application.Persistence;
using ShipManagement.Application.ShipService.Commands.DeleteShip;
using AutoMapper;
using MediatR;

public class DeleteShipCommandHandlerTests
{
    private readonly Mock<IShipRepository> _shipRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly DeleteShipCommandHandler _handler;

    public DeleteShipCommandHandlerTests()
    {
        _shipRepositoryMock = new Mock<IShipRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new DeleteShipCommandHandler(_shipRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldDeleteShip()
    {
        // Arrange
        var shipId = Guid.NewGuid();
        var command = new DeleteShipCommand(shipId);
        _shipRepositoryMock.Setup(x => x.DeleteShip(shipId));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
        _shipRepositoryMock.Verify(x => x.DeleteShip(shipId), Times.Once);
    }
}
