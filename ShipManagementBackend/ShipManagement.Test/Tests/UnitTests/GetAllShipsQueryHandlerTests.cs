namespace ShipManagement.Test.Tests.UnitTests;

using Moq;
using ShipManagement.Application.Persistence;
using ShipManagement.Application.Dtos;
using ShipManagement.Domain.Entities;
using ShipManagement.Application.ShipService.Queries.GetAllShips;
using AutoMapper;

public class GetAllShipsQueryHandlerTests
{
    private readonly Mock<IShipRepository> _shipRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllShipsQueryHandler _handler;

    public GetAllShipsQueryHandlerTests()
    {
        _shipRepositoryMock = new Mock<IShipRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllShipsQueryHandler(_shipRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllShips()
    {
        // Arrange
        var ships = new List<Ship> 
        {
            new Ship { id = Guid.NewGuid(), name = "Ship1", length = 100, width = 50, code = "ABCD-1234-A1" },
            new Ship { id = Guid.NewGuid(), name = "Ship2", length = 200, width = 100, code = "EFGH-5678-B2" }
        };
        var shipDtos = new List<ShipDto> 
        {
            new ShipDto(ships[0].id, ships[0].name, ships[0].length, ships[0].width, ships[0].code),
            new ShipDto(ships[1].id, ships[1].name, ships[1].length, ships[1].width, ships[1].code)
        };

        _shipRepositoryMock.Setup(x => x.GetShips()).Returns(ships);
        _mapperMock.Setup(x => x.Map<IEnumerable<ShipDto>>(ships)).Returns(shipDtos);

        var query = new GetAllShipsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(shipDtos, result);
        _shipRepositoryMock.Verify(x => x.GetShips(), Times.Once);
        _mapperMock.Verify(x => x.Map<IEnumerable<ShipDto>>(ships), Times.Once);
    }
}
