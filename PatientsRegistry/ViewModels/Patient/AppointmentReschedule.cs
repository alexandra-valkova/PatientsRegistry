using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsRegistry.ViewModels.Patient
{
    public class AppointmentReschedule : BaseVM
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}