using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.Filters;
using PatientsRegistry.Models;
using PatientsRegistry.ViewModels;
using PatientsRegistry.ViewModels.Patient;
using System;
using System.Data.Entity;
using System.Web.Mvc;

namespace PatientsRegistry.Controllers
{
    [Patient]
    [Authenticate]
    public class PatientController : BaseController<Appointment, AppointmentsListVM, AppointmentDetailsPatient>
    {
        // Repo
        public override BaseRepository<Appointment> GetRepo()
        {
            return new AppointmentRepository();
        }

        // Index
        public override AppointmentsListVM PopulateListVM(AppointmentsListVM model)
        {
            TryUpdateModel(model);

            BaseRepository<Appointment> appRepo = GetRepo();
            model.Appointments = appRepo.GetAll(a => a.PatientID == AuthenticationManager.LoggedUser.ID);

            return model;
        }

        // Details
        public override AppointmentDetailsPatient PopulateDetailsVM(AppointmentDetailsPatient model)
        {
            BaseRepository<Appointment> appRepo = GetRepo();
            Appointment app = appRepo.GetByID(model.ID);

            model.Date = app.Date;
            model.Status = app.Status;
            model.Doctor = app.Doctor.FullName;

            return model;
        }

        // Request GET
        [HttpGet]
        public new ActionResult Request()
        {
            AppointmentRequest model = new AppointmentRequest()
            {
                Date = DateTime.Now
            };
            return View(model);
        }

        // Request POST
        [HttpPost]
        public new ActionResult Request(AppointmentRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Appointment app = new Appointment()
            {
                ID = model.ID,
                Date = model.Date,
                DoctorID = model.DoctorID,
                PatientID = AuthenticationManager.LoggedUser.ID
            };

            AppointmentRepository appRepo = new AppointmentRepository();

            // check if appointment already exists
            Appointment findApp = appRepo.GetFirst(a => app.Date >= a.Date &&
                                                        app.Date <= DbFunctions.AddMinutes(a.Date, 30) &&
                                                        app.DoctorID == a.DoctorID);
            if (findApp == null)
            {
                appRepo.Save(app);
            }

            else
            {
                ModelState.AddModelError(string.Empty, "There is already an appointment scheduled for this date and hour!");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // Reschedule GET
        [HttpGet]
        public ActionResult Reschedule(int? id)
        {
            AppointmentRepository appRepo = new AppointmentRepository();
            Appointment app = appRepo.GetByID(id.Value);

            AppointmentReschedule model = new AppointmentReschedule()
            {
                Date = app.Date
            };

            return View(model);
        }

        // Reschedule POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reschedule(AppointmentReschedule model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppointmentRepository appRepo = new AppointmentRepository();
            Appointment app = appRepo.GetByID(model.ID);

            app.Date = model.Date;
            app.Status = StatusEnum.Unknown;
            appRepo.Save(app);

            return RedirectToAction("Index");
        }
    }
}