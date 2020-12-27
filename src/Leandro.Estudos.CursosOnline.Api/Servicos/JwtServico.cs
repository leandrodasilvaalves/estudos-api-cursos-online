using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Configuracoes;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Extensoes;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class JwtServico : IJwtServico
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly JwtSettings _settings;

    public JwtServico(UserManager<AppUser> userManager, IOptions<JwtSettings> settings)
    {
      _userManager = userManager;
      _settings = settings.Value;
    }
    public async Task<string> GerarToken(string email)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = _settings.Emissor,
        Audience = _settings.ValidoEm,
        Subject = await ObterClaimsAsync(email),
        Expires = DateTime.UtcNow.AddHours(_settings.ExpiracaoHoras),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(_settings.EncodeSecret),
                                     SecurityAlgorithms.HmacSha256Signature),
      });

      var encodedToken = tokenHandler.WriteToken(token);
      return encodedToken;
    }

    private async Task<ClaimsIdentity> ObterClaimsAsync(string email)
    {
      var usuario = await _userManager.FindByEmailAsync(email);
      var claims = await _userManager.GetClaimsAsync(usuario);

      claims.Add(new Claim(type: JwtRegisteredClaimNames.Sub, value: usuario.Id.ToString()));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Email, value: usuario.Email));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Nbf, value: DateTime.UtcNow.ToUnixEpocate().ToString()));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.UtcNow.ToUnixEpocate().ToString(), ClaimValueTypes.Integer64));

      var identityClaims = new ClaimsIdentity();
      identityClaims.AddClaims(claims);
      return identityClaims;
    }
  }
}