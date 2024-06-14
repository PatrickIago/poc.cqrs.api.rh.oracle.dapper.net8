using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poc.Contract.Command.Region.Interfaces;
using Poc.Contract.Query.Region.Interfaces;
using Poc.Oracle.CommandStore;
using Poc.Oracle.Context;
using Poc.Oracle.QueryStore;

namespace Poc.Oracle;

public class OracleInitializer
{
    public static void Initialize(IServiceCollection services, IConfiguration configuration)
    {
        // Configuração da string de conexão
        var oracleConnectionString = configuration.GetConnectionString("OracleHRDatabase");
        services.AddScoped<OracleDbContext>(_ => new OracleDbContext(oracleConnectionString));

        services.AddTransient<IRegionsReadOnlyRepository, RegionsReadOnlyRepository>();
        services.AddTransient<IRegionWriteOnlyRepository, RegionsWriteOnlyRepository>();
    }
}
