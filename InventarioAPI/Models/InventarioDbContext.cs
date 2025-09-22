using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Models;

public partial class InventarioDbContext : DbContext
{
    public InventarioDbContext()
    {
    }

    public InventarioDbContext(DbContextOptions<InventarioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlimentoBebida> AlimentoBebidas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public IQueryable<Usuario> GetUsuarioPorLogin(string login)
       => FromExpression(() => GetUsuarioPorLogin(login));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlimentoBebida>(entity =>
        {
            entity.ToTable("AlimentoBebida");
            entity.HasKey(e => e.Id).HasName("PK__Alimento__3214EC07DEC27039");

            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC0784E51498");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Login, "UQ__Usuario__5E55825B7E0391DF").IsUnique();

            entity.Property(e => e.ContraseniaHash).HasMaxLength(200);
            entity.Property(e => e.ContraseniaSalt).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.HasDbFunction(() => GetUsuarioPorLogin(default!))
        .HasName("GetUsuarioPorLogin") 
        .HasSchema("dbo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
