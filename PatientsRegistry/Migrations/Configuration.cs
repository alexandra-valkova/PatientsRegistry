namespace PatientsRegistry.Migrations
{
    using DataAccess;
    using DataAccess.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PatientsRegistryDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PatientsRegistryDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(
                  u => u.ID,
                  new User { ID = 1, Username = "alexandra", Password = "password", FirstName = "Alexandra", LastName = "Valkova", IsDoctor = false },
                  new User { ID = 2, Username = "yosifova", Password = "password", FirstName = "Diana", LastName = "Yosifova", IsDoctor = true }
                );
        }
    }
}
