using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public static class JwtConfig
  {
    public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
    {
      var settings = ConfigSection
                      .GetSection<JwtSettings>(
                          nameof(JwtSettings), services, configuration);

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(settings.EncodeSecret),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = settings.ValidoEm,
          ValidIssuer = settings.Emissor,
        };
      });

      return services;
    }
  }

  public class JwtSettings
  {
    public string Secret { get; set; }
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; }
    public string ValidoEm { get; set; }
    public byte[] EncodeSecret { get => Encoding.ASCII.GetBytes(Secret); }
  }
}