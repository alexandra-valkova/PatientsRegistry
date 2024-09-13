using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.Filters;
using PatientsRegistry.Models;
using PatientsRegistry.ViewModels;
using PatientsRegistry.ViewModels.Doctor;

namespace PatientsRegistry.Controllers
{
    [Doctor]
    [Authenticate]
    public class DoctorController : BaseController<Appointment, AppointmentsListViewModel, AppointmentDetailsDoctorViewModel>
    {
        public override BaseRepository<Appointment> GetRepository()
        {
            return new AppointmentRepository();
        }

        public override AppointmentsListViewModel PopulateListViewModel(AppointmentsListViewModel model)
        {
            TryUpdateModel(model);

            BaseRepository<Appointment> appointmentRepository = GetRepository();
            model.Appointments = appointmentRepository.GetAll(appointment => appointment.DoctorID == AuthenticationManager.LoggedUser.ID);

            return model;
        }

        public override AppointmentDetailsDoctorViewModel PopulateDetailsViewModel(AppointmentDetailsDoctorViewModel model)
        {
            BaseRepository<Appointment> appointmentRepository = GetRepository();
            Appointment appointment = appointmentRepository.GetByID(model.ID);

            model.Date = appointment.Date;
            model.Status = appointment.Status;
            model.Patient = appointment.Patient.FullName;

            return model;
        }

        [HttpGet]
        public ActionResult Review(int? id)
        {
            BaseRepository<Appointment> appointmentRepository = GetRepository();
            Appointment appointment = appointmentRepository.GetByID(id.Value);

            AppointmentReviewViewModel model = new AppointmentReviewViewModel()
            {
                ID = appointment.ID,
                PatientID = appointment.PatientID,
                DoctorID = appointment.DoctorID,
                Patient = appointment.Patient,
                Date = appointment.Date,
                Status = appointment.Status
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(AppointmentReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BaseRepository<Appointment> appointmentRepository = GetRepository();
            Appointment appointment = appointmentRepository.GetByID(model.ID);

            if (Request.Form["Approve"] != null)
            {
                model = Approve(model);
            }
            else if (Request.Form["Decline"] != null)
            {
                model = Decline(model);
            }

            appointment.Status = model.Status;

            appointmentRepository.Save(appointment);

            return RedirectToAction("Index");
        }

        private AppointmentReviewViewModel Approve(AppointmentReviewViewModel model)
        {
            model.Status = AppointmentStatus.Approved;
            return model;
        }

        private AppointmentReviewViewModel Decline(AppointmentReviewViewModel model)
        {
            model.Status = AppointmentStatus.Declined;
            return model;
        }
    }
}