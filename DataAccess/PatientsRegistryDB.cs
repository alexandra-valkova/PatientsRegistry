using DataAccess.Entities;
using System.Data.Entity;

namespace DataAccess
{
    public class PatientsRegistryDB : DbContext
    {
        public PatientsRegistryDB() : base("name=PatientsRegistryDB")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasRequired(m => m.Patient).WithMany().HasForeignKey(m => m.PatientID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Appointment>().HasRequired(m => m.Doctor).WithMany().HasForeignKey(m => m.DoctorID).WillCascadeOnDelete(false);
        }
    }
}