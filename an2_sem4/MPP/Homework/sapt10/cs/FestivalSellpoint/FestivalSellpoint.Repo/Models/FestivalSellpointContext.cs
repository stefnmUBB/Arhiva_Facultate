using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FestivalSellpoint.Repo.Models
{
    public partial class FestivalSellpointContext : DbContext
    {
        public FestivalSellpointContext()
        {                     
        }

        public FestivalSellpointContext(DbContextOptions<FestivalSellpointContext> options)
            : base(options)
        {            
        }

        public virtual DbSet<Angajat> Angajat { get; set; }
        public virtual DbSet<Bilet> Bilet { get; set; }
        public virtual DbSet<Spectacol> Spectacol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=C:\\Users\\Stefan\\Desktop\\mpp-proiect-csharp-stefnmUBB\\FestivalSellpoint\\FestivalSellpoint.db");
                SQLitePCL.Batteries.Init();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajat>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();                    

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("TEXT (50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("TEXT (128)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("TEXT (50)");
            });

            modelBuilder.Entity<Bilet>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NrLocuri).HasColumnName("nrLocuri");

                entity.Property(e => e.NumeCumparator)
                    .IsRequired()
                    .HasColumnName("numeCumparator")
                    .HasColumnType("TEXT (50)");

                entity.Property(e => e.Spectacol).HasColumnName("spectacol");

                entity.HasOne(d => d.SpectacolNavigation)
                    .WithMany(p => p.Bilet)
                    .HasForeignKey(d => d.Spectacol)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Spectacol>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Artist)
                    .IsRequired()
                    .HasColumnName("artist")
                    .HasColumnType("TEXT (50)");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Locatie)
                    .IsRequired()
                    .HasColumnName("locatie")
                    .HasColumnType("TEXT (100)");

                entity.Property(e => e.NrLocuriDisponibile).HasColumnName("nrLocuriDisponibile");

                entity.Property(e => e.NrLocuriVandute).HasColumnName("nrLocuriVandute");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
