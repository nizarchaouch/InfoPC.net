using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InfoPC.Models.InfoPC;

public partial class InfoPcDbContext : DbContext
{
    public InfoPcDbContext()
    {
    }

    public InfoPcDbContext(DbContextOptions<InfoPcDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Marque> Marques { get; set; }

    public virtual DbSet<Ordinateur> Ordinateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:InfoPcCS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marque>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marque__3214EC07754EE252");

            entity.ToTable("Marque");

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Nationality).HasMaxLength(30);
        });

        modelBuilder.Entity<Ordinateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ordinate__3214EC073F65D52D");

            entity.Property(e => e.MarqueId).HasColumnName("marqueId");
            entity.Property(e => e.Prix).HasColumnName("prix");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Marque).WithMany(p => p.Ordinateurs)
                .HasForeignKey(d => d.MarqueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordinateurs_Marque");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
