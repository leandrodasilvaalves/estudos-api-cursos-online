using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class ApiConfig
  {
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
      services.AddApiVersioning(options =>
           {
             options.AssumeDefaultVersionWhenUnspecified = true;
             options.DefaultApiVersion = new ApiVersion(1, 0);
             options.ReportApiVersions = true;
           });
      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
      });
      services.Configure<ApiBehaviorOptions>(options
        => options.SuppressModelStateInvalidFilter = true);
      return services;
    }
  }
}