using ContactEase.Application.Common.Interfaces.Persistence;
using ContactEase.Infrastructure.Persistence;
using ContactEase.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactEase.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((options) => options.UseInMemoryDatabase("InMemoryDb"));

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }
}