using AutoMapper;
using MediatR;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.Persistence;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Application.ShipService.Commands.CreateShip;

public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, ShipDto>
{
    private readonly IShipRepository _iShipRepository;
    private readonly IMapper _mapper;


    public CreateShipCommandHandler(IShipRepository iShipRepository, IMapper mapper)
    {
        _iShipRepository = iShipRepository;
        _mapper = mapper;
    }

    public async Task<ShipDto> Handle(CreateShipCommand command, CancellationToken cancellationToken)
    {
        Ship ship = _iShipRepository.InsertShip(_mapper.Map<Ship>(command));
        return await Task.FromResult(_mapper.Map<ShipDto>(ship));
    }


}