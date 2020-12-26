using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class SwaggerConfig
  {
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
        {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leandro.Estudos.CursosOnline.Api", Version = "v1" });
        });
      return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leandro.Estudos.CursosOnline.Api v1"));
      return app;
    }
  }
}