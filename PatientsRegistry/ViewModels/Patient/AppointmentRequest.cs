using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PatientsRegistry.ViewModels.Patient
{
    public class AppointmentRequest : BaseVM
    {
        [Required]
        [MinDateToday]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }

        public List<SelectListItem> DoctorsList
        {
            get
            {
                UserRepository userRepo = new UserRepository();
                List<User> doctorsAll = userRepo.GetAll(u => u.IsDoctor == true);
                List<SelectListItem> doctors = new List<SelectListItem>();
                foreach (User doctor in doctorsAll)
                {
                    doctors.Add(new SelectListItem { Text = doctor.ToString(), Value = doctor.ID.ToString() });
                }
                return doctors;
            }
        }

        public int PatientID { get; set; }
    }
}