using AutoMapper;
using MediatR;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.Persistence;

namespace ShipManagement.Application.ShipService.Queries.GetShipById;

public class GetShipByIdQueryHandler : IRequestHandler<GetShipByIdQuery, ShipDto>
{
    private readonly IShipRepository _iShipRepository;
    private readonly IMapper _mapper;

    public GetShipByIdQueryHandler(IShipRepository iShipRepository, IMapper mapper)
    {
        _iShipRepository = iShipRepository;
        _mapper = mapper;
    }

    public async Task<ShipDto> Handle(GetShipByIdQuery query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_mapper.Map<ShipDto>(_iShipRepository.GetShipById(query.id)));
    }


}