using System;
using System.Diagnostics;
using System.Text;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.FlushArgs;
using Leandro.Estudos.CursosOnline.Api.Extensoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class LogConfig
  {
    public static IConfiguration Configuration { get; private set; }
    public static IServiceCollection Services { get; private set; }
    public static IServiceCollection AddLogConfig(this IServiceCollection services)
    {
      Services = services;
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<ILogger>(context => Logger.Factory.Get());
      return services;
    }
    public static IApplicationBuilder UseLogConfig(this IApplicationBuilder app, IConfiguration configuration)
    {
      Configuration = configuration;
      app.UseKissLogMiddleware(options => ConfigureKissLog(options));
      return app;
    }
    private static void ConfigureKissLog(IOptionsBuilder optionsBuilder)
    {
      optionsBuilder.Options
          .AppendExceptionDetails((Exception ex) =>
          {
            StringBuilder sb = new StringBuilder();
            if (ex is System.NullReferenceException nullRefException)
              sb.AppendLine("Important: check for null references");
            return sb.ToString();
          });

      optionsBuilder.InternalLog = message => Debug.WriteLine(message);
      optionsBuilder.Options.ShouldLogRequestInputStream((ILogListener listener, FlushLogArgs args) =>
      {
        var currentUrl = args.WebProperties.Request.Url.ToString();
        return (currentUrl.NotContains("/entrar") && currentUrl.NotContains("/registrar"));
      });
      RegisterKissLogListeners(optionsBuilder);
    }

    private static void RegisterKissLogListeners(IOptionsBuilder optionsBuilder)
    {

      var settings = ConfigSection
                      .GetSection<KissLog>(
                          nameof(KissLog), Services, Configuration);

      optionsBuilder.Listeners.Add(
        new RequestLogsApiListener(new Application(
          settings.OrganizationId,
          settings.ApplicationId))
        { ApiUrl = settings.ApiUrl });
    }
  }

  public class KissLog
  {
    public string OrganizationId { get; set; }
    public string ApplicationId { get; set; }
    public string ApiUrl { get; set; }
  }
}