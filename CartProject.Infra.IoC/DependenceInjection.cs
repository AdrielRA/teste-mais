using CartProject.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using CartProject.Infra.Data.Repositories;
using CartProject.Application.Services;
using CartProject.Infra.Data.Contexts;
using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

namespace CartProject.Infra.IoC;

public static class DependenceInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        services.AddDbContext<InMemoryContext>();
        services.AddRepositories();
        services.AddServices();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}