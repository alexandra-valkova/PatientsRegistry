using PatientsRegistry.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsRegistry.ViewModels.Patient
{
    public class AppointmentReschedule : BaseVM
    {
        [Required]
        [MinDateToday]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}