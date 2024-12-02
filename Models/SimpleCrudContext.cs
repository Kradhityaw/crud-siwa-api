using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleCRUD.Models;

public partial class SimpleCrudContext : DbContext
{
    public SimpleCrudContext()
    {
    }

    public SimpleCrudContext(DbContextOptions<SimpleCrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsalSekolah> AsalSekolahs { get; set; }

    public virtual DbSet<Siswa> Siswas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=SimpleCRUD;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsalSekolah>(entity =>
        {
            entity.ToTable("AsalSekolah");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Siswa>(entity =>
        {
            entity.ToTable("Siswa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AsalSekolahId).HasColumnName("AsalSekolahID");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.AsalSekolah).WithMany(p => p.Siswas)
                .HasForeignKey(d => d.AsalSekolahId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Siswa_AsalSekolah");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
