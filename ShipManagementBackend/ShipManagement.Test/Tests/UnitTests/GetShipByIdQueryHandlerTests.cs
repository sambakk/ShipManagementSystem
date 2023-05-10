namespace ShipManagement.Test.Tests;

using Moq;
using AutoMapper;
using ShipManagement.Application.Persistence;
using ShipManagement.Application.ShipService.Queries.GetShipById;
using ShipManagement.Application.Dtos;
using ShipManagement.Domain.Entities;

public class GetShipByIdQueryHandlerTests
{
    private readonly Mock<IShipRepository> _shipRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetShipByIdQueryHandler _handler;
    private readonly CancellationToken _cancellationToken;

    public GetShipByIdQueryHandlerTests()
    {
        _shipRepositoryMock = new Mock<IShipRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetShipByIdQueryHandler(_shipRepositoryMock.Object, _mapperMock.Object);
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public async Task Handle_ShouldReturnShipDto()
    {
        // Arrange
        var shipId = Guid.NewGuid();
        var ship = new Ship { id = shipId, name = "Emma Maersk", length = 397.7, width = 56.4, code = "EMRK-2317-D5" };
        var shipDto = new ShipDto(shipId, "Emma Maersk", 397.7, 56.4, "EMRK-2317-D5");

        _shipRepositoryMock.Setup(x => x.GetShipById(shipId)).Returns(ship);
        _mapperMock.Setup(x => x.Map<ShipDto>(ship)).Returns(shipDto);

        var query = new GetShipByIdQuery(shipId);

        // Act
        var result = await _handler.Handle(query, _cancellationToken);

        // Assert
        Assert.Equal(shipDto, result);
    }
    
}
