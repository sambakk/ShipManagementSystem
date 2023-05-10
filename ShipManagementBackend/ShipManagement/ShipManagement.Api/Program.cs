using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using ShipManagement.Application;
using ShipManagement.Infrastructure;
using ShipManagement.Infrastructure.Context;
using ShipManagement.Infrastructure.Midllewares;
using ShipManagement.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add all services from application layer
builder.Services.AddApplication()
.AddInfrastructure();

// Add AutoMpper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add DB Context
builder.Services.AddDbContext<ShipManagementDbContext>(options =>
    options.UseInMemoryDatabase("ShipManagementDb"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
    
// Add Api Versionning
builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
        opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                        new HeaderApiVersionReader("x-api-version"),
                                                        new MediaTypeApiVersionReader("x-api-version"));
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed some dummy data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShipManagementDbContext>();
    DataSeeder.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add exceptions middleware
app.UseMiddleware(typeof(GlobalExceptionHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
