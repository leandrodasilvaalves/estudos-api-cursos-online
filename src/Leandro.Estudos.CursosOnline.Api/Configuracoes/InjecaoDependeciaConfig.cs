using System;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class InjecaoDependeciaConfig
  {
    public static IServiceCollection AddInjecaoDependenciaConfig(this IServiceCollection services)
    {
      ConfigurarContextos(services);
      ConfigurarServicos(services);
      ConfigurarRepositorios(services);
      return services;
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
    }
    public static void ConfigurarRepositorios(IServiceCollection services)
    {
      services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
      services.AddScoped<ICursoRepositorio, CursoRepositorio>();
    }
  }
}