namespace ShipManagement.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using ShipManagement.Application.Persistence;
using ShipManagement.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection iServiceCollection)
    {
        iServiceCollection.AddScoped<IShipRepository, ShipRepository>();
        return iServiceCollection;
    }
}
