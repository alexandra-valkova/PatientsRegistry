using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDoctor { get; set; }

        [Display(Name = "Name")]
        public virtual string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}