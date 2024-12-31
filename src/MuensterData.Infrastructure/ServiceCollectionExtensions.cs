using Microsoft.Extensions.DependencyInjection;
using MuensterData.Domain.Common.Ports;
using MuensterData.Infrastructure.DataLoading;

namespace MuensterData.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICsvReader, CsvReader>();
    }
}
