using System.ComponentModel.DataAnnotations;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public class LoginModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
  }
}