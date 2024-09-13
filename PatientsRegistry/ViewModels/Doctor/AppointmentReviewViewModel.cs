using System;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;

namespace PatientsRegistry.ViewModels.Doctor
{
    public class AppointmentReviewViewModel : BaseViewModel
    {
        [Required]
        public AppointmentStatus Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public int PatientID { get; set; }

        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }

        public User Patient { get; set; }
    }
}