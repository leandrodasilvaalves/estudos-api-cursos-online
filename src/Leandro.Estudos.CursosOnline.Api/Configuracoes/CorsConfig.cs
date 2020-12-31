using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class CorsConfig
  {
    private static string dev = "Development";
    private static string prod = "Production";

    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(dev, builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        options.AddPolicy(prod, builder =>
            builder
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithOrigins("http://example.com", "http://www.contoso.com")
                .SetIsOriginAllowedToAllowWildcardSubdomains());
      });
      return services;
    }

    public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseCors(env.IsDevelopment() ? dev : prod);
      return app;
    }
  }
}