using System.Data.Entity.Migrations;
using DataAccess;
using DataAccess.Entities;

namespace PatientsRegistry.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PatientsRegistryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PatientsRegistryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var patient = new User
            {
                ID = 1,
                Username = "alexandra",
                Password = "password",
                FirstName = "Alexandra",
                LastName = "Valkova",
                IsDoctor = false
            };

            var doctor = new User
            {
                ID = 2,
                Username = "yosifova",
                Password = "password",
                FirstName = "Diana",
                LastName = "Yosifova",
                IsDoctor = true
            };

            context.Users.AddOrUpdate(user => user.ID, patient, doctor);
        }
    }
}
