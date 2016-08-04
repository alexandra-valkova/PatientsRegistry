using DataAccess.Entities;
using System.Data.Entity;

namespace PatientsRegistry
{
    public class AppContext : DbContext
    {
        public AppContext() : base("name=PatientsRegistryDB")
        {
        }

        public DbSet<User> Users { get; set; }

        //public DbSet<Patient> Patients { get; set; }

        //public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasRequired(m => m.Patient).WithMany().HasForeignKey(m => m.PatientID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Appointment>().HasRequired(m => m.Doctor).WithMany().HasForeignKey(m => m.DoctorID).WillCascadeOnDelete(false);

            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}