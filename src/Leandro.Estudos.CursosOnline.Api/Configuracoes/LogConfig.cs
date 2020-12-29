using System;
using System.Diagnostics;
using System.Text;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class LogConfig
  {
    public static IConfiguration Configuration { get; private set; }
    public static IServiceCollection AddLogConfig(this IServiceCollection services)
    {
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<ILogger>((context) =>
      {
        return Logger.Factory.Get();
      });
      return services;
    }
    public static IApplicationBuilder UseLogConfig(this IApplicationBuilder app, IConfiguration configuration)
    {
      Configuration = configuration;
      app.UseKissLogMiddleware(options =>
      {
        ConfigureKissLog(options);
      });
      return app;
    }
    private static void ConfigureKissLog(IOptionsBuilder options)
    {
      options.Options
          .AppendExceptionDetails((Exception ex) =>
          {
            StringBuilder sb = new StringBuilder();
            if (ex is System.NullReferenceException nullRefException)
            {
              sb.AppendLine("Important: check for null references");
            }
            return sb.ToString();
          });

      options.InternalLog = (message) =>
      {
        Debug.WriteLine(message);
      };

      RegisterKissLogListeners(options);
    }

    private static void RegisterKissLogListeners(IOptionsBuilder options)
    {
      options.Listeners.Add(new RequestLogsApiListener(new Application(
          Configuration["KissLog.OrganizationId"],
          Configuration["KissLog.ApplicationId"])
      )
      {
        ApiUrl = Configuration["KissLog.ApiUrl"]
      });
    }
  }
}