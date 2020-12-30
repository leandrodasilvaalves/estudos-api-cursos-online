using System;
using System.Net.Http;
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
      var settings = ConfigSection.GetSection<HealthSettings>("HealthCheck", services, configuration);
      services.AddHealthChecks()
        .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "SqlServerLib")
        .AddCheck("SqlServerCustom", new SqlServerCustomHealthCheck(configuration.GetConnectionString("DefaultConnection")))
        .AddUrlGroup(new Uri(settings.Github), name: "Github")
        .AddCheck("JsonPlaceHolder", new ApiExternaHealthCheck(new Uri(settings.JsonPlace), HttpMethod.Get));
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

  public class HealthSettings
  {
    public string Github { get; set; }
    public string JsonPlace { get; set; }
  }
}
