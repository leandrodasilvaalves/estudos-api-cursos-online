using Leandro.Estudos.CursosOnline.Api.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Leandro.Estudos.CursosOnline.Api.Configuracoes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Leandro.Estudos.CursosOnline.Api.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Leandro.Estudos.CursosOnline.Api.HealthChecks;


namespace Leandro.Estudos.CursosOnline.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<CursoContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentityConfig(Configuration);
      services.AddJwtConfig(Configuration);

      services.AddControllers()
              .AddNewtonsoftJson(x =>
                 x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

      services.AddApiConfig();
      services.AddHealthCheckConfig(Configuration);
      services.AddInjecaoDependenciaConfig();
      services.AddLogConfig();
      services.AddSwaggerConfig();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwaggerConfig(provider);
      }
      app.UseExcptionMiddleware();
      app.UseHttpsRedirection();
      app.UseLogConfig(Configuration);

      app.UseRouting();
      app.UseIdentityConfig();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHealthChecksControllers();
      });
    }
  }
}
