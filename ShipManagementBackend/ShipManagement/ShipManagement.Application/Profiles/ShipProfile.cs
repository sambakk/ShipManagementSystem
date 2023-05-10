namespace ShipManagement.Application.Profiles;

using AutoMapper;
using ShipManagement.Application.Dtos;
using ShipManagement.Application.ShipService.Commands.CreateShip;
using ShipManagement.Application.ShipService.Commands.UpdateShip;
using ShipManagement.Domain.Entities;

public class ShipProfile : Profile
{
    public ShipProfile()
    {
        // Mapping Ship & ShipDto
        CreateMap<Ship, ShipDto>().ReverseMap();

        // Mapping Create, Update, Delete Commands & Ship
        CreateMap<CreateShipCommand, Ship>();
        CreateMap<UpdateShipCommand, Ship>();
        // CreateMap<DeleteShipCommand, Ship>();
    }
}