using System;
using System.ComponentModel.DataAnnotations;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public class ContaTrocaSenhaModel
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string SenhaAtual { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string NovaSenha { get; set; }

    [Compare(nameof(NovaSenha), ErrorMessage = "O campo {0} deve ser igual ao campo nova senha")]
    public string ConfirmaNovaSenha { get; set; }
  }
}