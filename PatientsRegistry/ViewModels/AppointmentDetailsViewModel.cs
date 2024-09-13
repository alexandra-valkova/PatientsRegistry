using System;
using DataAccess.Entities;

namespace PatientsRegistry.ViewModels
{
    public class AppointmentDetailsViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}