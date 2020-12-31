using System;
using AutoMapper;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Leandro.Estudos.CursosOnline.Api.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class InjecaoDependeciaConfig
  {
    public static IServiceCollection AddInjecaoDependenciaConfig(this IServiceCollection services)
    {
      ConfigurarContextos(services);
      ConfigurarServicos(services);
      ConfigurarRepositorios(services);
      ConfiguracoesGerais(services);
      return services;
    }

    private static void ConfiguracoesGerais(IServiceCollection services)
    {
      services.AddScoped<INotificador, Notificador>();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
      services.AddSingleton(provider => new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new AutoMapperConfig(provider.GetService<IHttpContextAccessor>()));
      }).CreateMapper());
    }

    private static void ConfigurarContextos(IServiceCollection services)
    {
      services.AddScoped(typeof(CursoContext));
    }

    public static void ConfigurarServicos(IServiceCollection services)
    {
      services.AddScoped<IAlunoServico, AlunoServico>();
      services.AddScoped<ICursoServico, CursoServico>();
      services.AddScoped<IJwtServico, JwtServico>();
      services.AddScoped<IContaServico, ContaServico>();
    }
    public static void ConfigurarRepositorios(IServiceCollection services)
    {
      services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
      services.AddScoped<ICursoRepositorio, CursoRepositorio>();
    }
  }
}