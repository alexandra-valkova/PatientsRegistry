using System.Data.Entity;
using DataAccess.Entities;

namespace DataAccess
{
    public class PatientsRegistryDbContext : DbContext
    {
        public PatientsRegistryDbContext() : base("name=PatientsRegistryDB")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                        .HasRequired(appointment => appointment.Patient)
                        .WithMany()
                        .HasForeignKey(appointment => appointment.PatientID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Appointment>()
                        .HasRequired(appointment => appointment.Doctor)
                        .WithMany()
                        .HasForeignKey(appointment => appointment.DoctorID)
                        .WillCascadeOnDelete(false);
        }
    }
}