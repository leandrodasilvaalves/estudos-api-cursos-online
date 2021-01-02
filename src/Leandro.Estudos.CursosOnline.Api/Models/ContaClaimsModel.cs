using System;
using System.Collections.Generic;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public class ContaClaimsModel : AppUser
  {
    public ContaClaimsModel(AppUser appUser, List<IdentityUserClaim<Guid>> claims)
    {
      Id = appUser.Id;
      UserName = appUser.UserName;
      Email = appUser.Email;
      PhoneNumber = appUser.PhoneNumber;
      PhoneNumberConfirmed = appUser.PhoneNumberConfirmed;
      TwoFactorEnabled = appUser.TwoFactorEnabled;
      LockoutEnabled = appUser.LockoutEnabled;
      AccessFailedCount = appUser.AccessFailedCount;
      Claims = claims;
    }
    public IEnumerable<IdentityUserClaim<Guid>> Claims { get; set; }
  }
}