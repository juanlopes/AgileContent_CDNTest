using CandidateTesting.JuanMatheusLopes.Application.Interfaces;
using CandidateTesting.JuanMatheusLopes.Application.Orchestrators;
using CandidateTesting.JuanMatheusLopes.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTesting.JuanMatheusLopes.Application.Modules;

public class ApplicationModule : IDependencyModule
{
    public IServiceCollection RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<ILogFormatterOrchestrator, LogFormatterOrchestrator>();
        services.AddScoped<IUrlValidator, UrlValidator>();

        return services;
    }
}
