using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
  public class AppUser : IdentityUser<Guid>
  {
  }
}