using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
  public class AppUser : IdentityUser<Guid>
  {
  }

  public class RegistroModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }

    [Compare(nameof(Senha))]
    public string ConfirmaSenha { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
  }
}