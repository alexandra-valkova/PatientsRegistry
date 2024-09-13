using System;
using System.ComponentModel.DataAnnotations;
using PatientsRegistry.Attributes;

namespace PatientsRegistry.ViewModels.Patient
{
    public class AppointmentRescheduleViewModel : BaseViewModel
    {
        [Required]
        [MinDateToday]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}