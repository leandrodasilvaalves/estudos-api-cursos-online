using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public class ConfigSection
  {
    public static T GetSection<T>(string section,
                                 IServiceCollection services,
                                 IConfiguration configuration) where T : class
    {
      var settingSection = configuration.GetSection(section);
      services.Configure<T>(settingSection);
      return settingSection.Get<T>();
    }
  }
}