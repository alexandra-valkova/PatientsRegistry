using DataAccess.Entities;
using System.Data.Entity;

namespace DataAccess
{
    public class PatientsRegistryDB<T> : DbContext where T : BaseEntity
    {
        public PatientsRegistryDB() : base("name=PatientsRegistryDB")
        {
        }

        public DbSet<T> Items { get; set; }
    }
}