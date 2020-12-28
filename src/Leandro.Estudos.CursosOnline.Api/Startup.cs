using Leandro.Estudos.CursosOnline.Api.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Leandro.Estudos.CursosOnline.Api.Configuracoes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

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
              .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

      services.Configure<ApiBehaviorOptions>(options
        => options.SuppressModelStateInvalidFilter = true);

      services.AddInjecaoDependenciaConfig();
      services.AddSwaggerConfig();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwaggerConfig();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseIdentityConfig();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
