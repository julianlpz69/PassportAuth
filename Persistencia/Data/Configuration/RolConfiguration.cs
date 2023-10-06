using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("rol");
            
            builder.Property(p => p.NombreRol)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();


            builder.HasData(
                new Rol {Id = 1, NombreRol = "Empleado"}
            );
        }
    }
}