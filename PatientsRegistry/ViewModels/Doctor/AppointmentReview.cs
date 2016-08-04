using DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsRegistry.ViewModels.Doctor
{
    public class AppointmentReview : BaseVM
    {
        [Required]
        public StatusEnum Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public int PatientID { get; set; }

        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }

        public User Patient { get; set; }
    }
}