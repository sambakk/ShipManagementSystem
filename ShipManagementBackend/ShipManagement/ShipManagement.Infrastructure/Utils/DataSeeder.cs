namespace ShipManagement.Infrastructure.Utils;

using ShipManagement.Domain.Entities;
using ShipManagement.Infrastructure.Context;

public static class DataSeeder
{
    public static void SeedData(ShipManagementDbContext context)
    {
        // Seed data if the database is empty
        if (!context.Ships.Any())
        {
            context.Ships.AddRange(
                new Ship { id = Guid.NewGuid(), name = "Emma Maersk", length = 397.7, width = 56.4, code = "EMRK-2317-D5" },
                new Ship { id = Guid.NewGuid(), name = "CMA CGM Marco Polo", length = 396, width = 53.6, code = "CCMP-4812-J7" },
                new Ship { id = Guid.NewGuid(), name = "MSC Gülsün", length = 399.9, width = 61.5, code = "MSCG-1930-Z2" },
                new Ship { id = Guid.NewGuid(), name = "Ever Golden", length = 400, width = 58.8, code = "EVGN-6754-X3" },
                new Ship { id = Guid.NewGuid(), name = "OOCL Hong Kong", length = 399.9, width = 58.8, code = "OOHK-1290-R6" }
            );
            context.SaveChanges();
        }
    }
}
