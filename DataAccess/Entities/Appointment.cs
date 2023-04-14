using System;

namespace DataAccess.Entities
{
    public class Appointment : BaseEntity
    {
        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public virtual User Patient { get; set; }

        public virtual User Doctor { get; set; }

        public DateTime Date { get; set; }

        public StatusEnum Status { get; set; }
    }

    public enum StatusEnum
    {
        Pending,
        Approved,
        Unapproved
    }
}