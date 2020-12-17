using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.CPF).IsRequired().HasMaxLength(30);
            builder.HasIndex(f => f.CPF).IsUnique();
            builder.Property(f => f.Foto).IsRequired();
            builder.Property(f => f.PrimeiroAcesso).IsRequired();
            builder.Property(f => f.Status).IsRequired();

            builder.HasMany(u => u.ProprietariosApartamentos).WithOne(u => u.Proprietario);
            builder.HasMany(u => u.MoradoresApartamentos).WithOne(u => u.Morador);
            builder.HasMany(u => u.Veiculos).WithOne(u => u.Usuario);
            builder.HasMany(u => u.Eventos).WithOne(u => u.Usuario);
            builder.HasMany(u => u.Pagamentos).WithOne(u => u.Usuario);
            builder.HasMany(u => u.Servicos).WithOne(u => u.Usuario);

            builder.ToTable("Usuarios");


        }
         
    }
}