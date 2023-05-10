using ContactEase.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContactEase.Application;

public static class ConfigureApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonServices, PersonService>();
        services.AddScoped<IContactService, ContacteService>();

        return services;
    }
}
