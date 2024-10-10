using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPICrud.Models;

namespace WebAPICrud.Data;

public partial class DbCrudContext : DbContext
{
    private readonly string _connectionString;
    public DbCrudContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbCrudContext(DbContextOptions<DbCrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK__Usuarios__430A673CF725270A");

            entity.Property(e => e.UsuId).HasColumnName("usu_id");
            entity.Property(e => e.UsuApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu_apellido");
            entity.Property(e => e.UsuCedula).HasColumnName("usu_cedula");
            entity.Property(e => e.UsuContrasenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu_contrasenia");
            entity.Property(e => e.UsuCorreo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu_correo");
            entity.Property(e => e.UsuNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu_nombre");
            entity.Property(e => e.UsuUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
