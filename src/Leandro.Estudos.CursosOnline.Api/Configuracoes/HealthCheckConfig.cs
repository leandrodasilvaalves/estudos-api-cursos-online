using System;
using HealthChecks.UI.Client;
using Leandro.Estudos.CursosOnline.Api.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class HealthCheckConfig
  {
    public static IServiceCollection AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddHealthChecks()
        //.AddSqlServer(Configuration.GetConnectionString("DefaultConnection"), name: "SqlServer")
        .AddCheck("SqlServer", new SqlServerCustomHealthCheck(configuration.GetConnectionString("DefaultConnection")));
      return services;
    }

    public static IEndpointConventionBuilder MapHealthChecksControllers(this IEndpointRouteBuilder endpoints)
    {
      return endpoints.MapHealthChecks("/api/hc", new HealthCheckOptions()
      {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });
    }
  }
}
