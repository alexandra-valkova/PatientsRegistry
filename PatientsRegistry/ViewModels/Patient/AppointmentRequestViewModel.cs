using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.Attributes;

namespace PatientsRegistry.ViewModels.Patient
{
    public class AppointmentRequestViewModel : BaseViewModel
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
                UserRepository userRepository = new UserRepository();
                List<User> doctors = userRepository.GetAll(user => user.IsDoctor);

                var doctorsList = new List<SelectListItem>();
                foreach (User doctor in doctors)
                {
                    doctorsList.Add(new SelectListItem
                    {
                        Text = doctor.FullName,
                        Value = doctor.ID.ToString()
                    });
                }

                return doctorsList;
            }
        }

        public int PatientID { get; set; }
    }
}