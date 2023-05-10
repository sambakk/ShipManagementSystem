namespace ShipManagement.Application;

using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection iServiceCollection)
    {
        // Add all application services
        iServiceCollection.AddMediatR(typeof(DependencyInjection).Assembly);
        return iServiceCollection;
    }
}
