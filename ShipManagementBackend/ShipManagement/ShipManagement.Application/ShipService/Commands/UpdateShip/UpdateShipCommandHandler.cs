using AutoMapper;
using MediatR;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.Persistence;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Application.ShipService.Commands.UpdateShip;

public class UpdateShipCommandHandler : IRequestHandler<UpdateShipCommand, Unit>
{
    private readonly IShipRepository _iShipRepository;
    private readonly IMapper _mapper;


    public UpdateShipCommandHandler(IShipRepository iShipRepository, IMapper mapper)
    {
        _iShipRepository = iShipRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateShipCommand command, CancellationToken cancellationToken)
    {
        _iShipRepository.UpdateShip(command.id, _mapper.Map<Ship>(command));
        return await Task.FromResult(Unit.Value);
    }


}