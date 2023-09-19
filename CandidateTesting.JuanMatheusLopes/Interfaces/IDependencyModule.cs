using Microsoft.Extensions.DependencyInjection;

namespace CandidateTesting.JuanMatheusLopes.Application.Interfaces;

public interface IDependencyModule
{
    IServiceCollection RegisterDependencies(IServiceCollection services);
}
