using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class LoginUserConfiguration : IEntityTypeConfiguration<LoginUser>
    {
        public void Configure(EntityTypeBuilder<LoginUser> builder)
        {
            builder.ToTable("login_user");
            
            builder.Property(p => p.UserEmail)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();


            
        }
    }
}