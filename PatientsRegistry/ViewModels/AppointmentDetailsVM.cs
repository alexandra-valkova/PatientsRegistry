using DataAccess.Entities;
using System;

namespace PatientsRegistry.ViewModels
{
    public class AppointmentDetailsVM : BaseVM
    {
        public DateTime Date { get; set; }

        public StatusEnum Status { get; set; }
    }
}