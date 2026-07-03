using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class Dsw2026Ej15DbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        public Dsw2026Ej15DbContext(DbContextOptions<Dsw2026Ej15DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors");
                entity.Property(doctor => doctor.Name).HasMaxLength(100).IsRequired();
                entity.Property(doctor => doctor.LicenseNumber).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("Specialities");
                entity.Property(speciality => speciality.Name).HasMaxLength(100).IsRequired();
                entity.Property(speciality => speciality.Description).HasMaxLength(300).IsRequired();
            });
        }
    }
}
