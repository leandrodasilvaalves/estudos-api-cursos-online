using System.ComponentModel.DataAnnotations;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
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
}