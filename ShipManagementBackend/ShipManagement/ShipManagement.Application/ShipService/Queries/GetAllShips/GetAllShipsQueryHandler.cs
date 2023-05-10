using AutoMapper;
using MediatR;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.Persistence;

namespace ShipManagement.Application.ShipService.Queries.GetAllShips;

public class GetAllShipsQueryHandler : IRequestHandler<GetAllShipsQuery, IEnumerable<ShipDto>>
{
    private readonly IShipRepository _iShipRepository;
    private readonly IMapper _mapper;

    public GetAllShipsQueryHandler(IShipRepository iShipRepository, IMapper mapper)
    {
        _iShipRepository = iShipRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShipDto>> Handle(GetAllShipsQuery query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_mapper.Map<IEnumerable<ShipDto>>(_iShipRepository.GetShips()));
    }


}