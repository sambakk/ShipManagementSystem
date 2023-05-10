namespace ShipManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ShipManagement.Domain.Entities;

public class ShipManagementDbContext : DbContext
{

    public ShipManagementDbContext(DbContextOptions<ShipManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ship> Ships { get; set; } 
}