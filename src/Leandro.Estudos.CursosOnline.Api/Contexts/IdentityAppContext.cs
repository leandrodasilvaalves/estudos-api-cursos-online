using System;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leandro.Estudos.CursosOnline.Api.Contexts
{
    public class IdentityAppContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options)
            : base(options) {}
    }
}