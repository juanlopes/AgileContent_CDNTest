using CandidateTesting.JuanMatheusLopes.Application.Interfaces;
using CandidateTesting.JuanMatheusLopes.Domain.Factories;
using CandidateTesting.JuanMatheusLopes.Domain.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTesting.JuanMatheusLopes.Application.Modules;

public class DomainModule : IDependencyModule
{
    public IServiceCollection RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<ILogFactory, LogFactory>();

        services.AddScoped<IMinhaCDNMapping, MinhaCDNMapping>();
        services.AddScoped<IAgoraCDNMapping, AgoraCDNMapping>();

        return services;
    }
}
