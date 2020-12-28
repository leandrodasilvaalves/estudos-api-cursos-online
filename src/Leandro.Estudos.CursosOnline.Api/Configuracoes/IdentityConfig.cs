using System;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Extensoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class IdentityConfig
  {
    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<IdentityAppContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
              .AddIdentity<AppUser, IdentityRole<Guid>>()
              .AddEntityFrameworkStores<IdentityAppContext>()
              .AddErrorDescriber<IdentityMensagensPortugues>()
              .AddDefaultTokenProviders();
      return services;
    }

    public static IApplicationBuilder UseIdentityConfig(this IApplicationBuilder app)
    {
      app.UseAuthentication();
      app.UseAuthorization();
      return app;
    }
  }
}