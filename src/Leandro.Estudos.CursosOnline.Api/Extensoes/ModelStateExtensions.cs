using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Leandro.Estudos.CursosOnline.Api.Extensoes
{
  public static class ModelStateExtensions
  {
    public static bool IsInvalid(this ModelStateDictionary modelState)
        => !modelState.IsValid;
  }
}