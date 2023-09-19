using CandidateTesting.JuanMatheusLopes.Application.Interfaces;
using CandidateTesting.JuanMatheusLopes.Application.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTesting.JuanMatheusLopes.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddModule(new DomainModule());
        services.AddModule(new InfrastructureModule());
        services.AddModule(new ApplicationModule());

        return services;
    }

    private static IServiceCollection AddModule(this IServiceCollection services, IDependencyModule module)
    {
        module.RegisterDependencies(services);

        return services;
    }
}
