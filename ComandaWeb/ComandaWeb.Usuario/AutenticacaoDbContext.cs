using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.DAL.Usuario
{
    public class AutenticacaoDbContext : IdentityDbContext<Usuario>
    {
        public AutenticacaoDbContext(DbContextOptions<AutenticacaoDbContext> options)
          : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration<Usuario>(new UsuarioConfiguracao());
        }
    }
}
