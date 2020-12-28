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
          c.SwaggerDoc("v1", new OpenApiInfo
          {
            Title = "CursosOnline [Api]",
            Description = "Esta é uma api com cenário simples, cuja finalide é apenas estudos",
            Contact = new OpenApiContact
            {
              Email = "leandro.silva.alves86@gmail.com",
              Name = "Leandro da Silva Alves",
              Url = new System.Uri("https://github.com/leandrodasilvaalves/"),
            },
            Version = "v1"
          });
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