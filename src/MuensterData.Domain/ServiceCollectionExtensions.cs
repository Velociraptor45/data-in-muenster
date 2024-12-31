using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MuensterData.Domain;

public static class ServiceCollectionExtensions
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddFluxor(config =>
        {
            config
                .ScanAssemblies(Assembly.GetExecutingAssembly())
#if DEBUG
                .UseReduxDevTools()
#endif
                ;
        });
    }
}