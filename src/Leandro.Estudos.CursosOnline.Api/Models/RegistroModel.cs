using System.ComponentModel.DataAnnotations;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public class RegistroModel
  {
    [Required]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Senha { get; set; }

    [Compare(nameof(Senha), ErrorMessage = "O campo {0} deve ser igual ao campo Senha")]
    public string ConfirmaSenha { get; set; }
  }
}