using AutoMapper;
using MediatR;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.Persistence;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Application.ShipService.Commands.DeleteShip;

public class DeleteShipCommandHandler : IRequestHandler<DeleteShipCommand, Unit>
{
    private readonly IShipRepository _iShipRepository;
    private readonly IMapper _mapper;


    public DeleteShipCommandHandler(IShipRepository iShipRepository, IMapper mapper)
    {
        _iShipRepository = iShipRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteShipCommand command, CancellationToken cancellationToken)
    {
        _iShipRepository.DeleteShip(command.id);
        return await Task.FromResult(Unit.Value);
    }


}