using System;
using System.Data.Entity;
using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.Filters;
using PatientsRegistry.Models;
using PatientsRegistry.ViewModels;
using PatientsRegistry.ViewModels.Patient;

namespace PatientsRegistry.Controllers
{
    [Patient]
    [Authenticate]
    public class PatientController : BaseController<Appointment, AppointmentsListViewModel, AppointmentDetailsPatientViewModel>
    {
        public override BaseRepository<Appointment> GetRepository()
        {
            return new AppointmentRepository();
        }

        public override AppointmentsListViewModel PopulateListViewModel(AppointmentsListViewModel model)
        {
            TryUpdateModel(model);

            BaseRepository<Appointment> appointmentRepository = GetRepository();
            model.Appointments = appointmentRepository.GetAll(appointment => appointment.PatientID == AuthenticationManager.LoggedUser.ID);

            return model;
        }

        public override AppointmentDetailsPatientViewModel PopulateDetailsViewModel(AppointmentDetailsPatientViewModel model)
        {
            BaseRepository<Appointment> appointmentRepository = GetRepository();
            Appointment appointment = appointmentRepository.GetByID(model.ID);

            model.Date = appointment.Date;
            model.Status = appointment.Status;
            model.Doctor = appointment.Doctor.FullName;

            return model;
        }

        [HttpGet]
        public new ActionResult Request()
        {
            AppointmentRequestViewModel model = new AppointmentRequestViewModel()
            {
                Date = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public new ActionResult Request(AppointmentRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Appointment appointment = new Appointment()
            {
                ID = model.ID,
                Date = model.Date,
                DoctorID = model.DoctorID,
                PatientID = AuthenticationManager.LoggedUser.ID
            };

            BaseRepository<Appointment> appointmentRepository = GetRepository();

            // check if appointment already exists
            Appointment existingAppointment = appointmentRepository.GetFirst(a => appointment.Date >= a.Date
                                                                                  && appointment.Date <= DbFunctions.AddMinutes(a.Date, 30)
                                                                                  && appointment.DoctorID == a.DoctorID);

            if (existingAppointment == null)
            {
                appointmentRepository.Save(appointment);
            }

            else
            {
                ModelState.AddModelError(string.Empty, "There is already an appointment scheduled for this date and hour!");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Reschedule(int? id)
        {
            BaseRepository<Appointment> appointmentRepository = GetRepository();
            Appointment appointment = appointmentRepository.GetByID(id.Value);

            AppointmentRescheduleViewModel model = new AppointmentRescheduleViewModel()
            {
                Date = appointment.Date
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reschedule(AppointmentRescheduleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            BaseRepository<Appointment> appointmentRepository = GetRepository();
            Appointment appointment = appointmentRepository.GetByID(model.ID);

            appointment.Date = model.Date;
            appointment.Status = AppointmentStatus.Pending;
            appointmentRepository.Save(appointment);

            return RedirectToAction("Index");
        }
    }
}