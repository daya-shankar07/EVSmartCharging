using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Application.SmartCharging.EFCore.Models
{
    public partial class evsolutionContext : DbContext
    {
        public evsolutionContext()
        {
        }

        public evsolutionContext(DbContextOptions<evsolutionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Connector> Connectors { get; set; }
        public virtual DbSet<Cstation> Cstations { get; set; }
        public virtual DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO- keyvault integration and fetch from keyvault 
                optionsBuilder.UseSqlServer("Server=tcp:smartcharging.database.windows.net,1433;Initial Catalog=evsolution;Persist Security Info=False;User ID=centraluser;Password=Welcome@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Connector>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CstationId });

                entity.ToTable("Connector");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CstationId).HasColumnName("CStationId");

                entity.HasOne(d => d.Cstation)
                    .WithMany(p => p.Connectors)
                    .HasForeignKey(d => d.CstationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CStation_Id");
            });

            modelBuilder.Entity<Cstation>(entity =>
            {
                entity.HasKey(e => e.StationId);

                entity.ToTable("CStation");

                entity.Property(e => e.StationId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Cstations)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Group_Id");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
