using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.Filters;
using PatientsRegistry.Models;
using PatientsRegistry.ViewModels;
using PatientsRegistry.ViewModels.Doctor;
using System.Web.Mvc;

namespace PatientsRegistry.Controllers
{
    [Doctor]
    [Authenticate]
    public class DoctorController : BaseController<Appointment, AppointmentsListVM, AppointmentDetailsDoctor>
    {
        public override BaseRepository<Appointment> GetRepo()
        {
            return new AppointmentRepository();
        }

        // Index
        public override AppointmentsListVM PopulateListVM(AppointmentsListVM model)
        {
            TryUpdateModel(model);

            BaseRepository<Appointment> appRepo = GetRepo();
            model.Appointments = appRepo.GetAll(a => a.DoctorID == AuthenticationManager.LoggedUser.ID);

            return model;
        }

        // Details
        public override AppointmentDetailsDoctor PopulateDetailsVM(AppointmentDetailsDoctor model)
        {
            BaseRepository<Appointment> appRepo = GetRepo();
            Appointment app = appRepo.GetByID(model.ID);

            model.Date = app.Date;
            model.Status = app.Status;
            model.Patient = app.Patient.FullName;

            return model;
        }

        // Review GET
        [HttpGet]
        public ActionResult Review(int? id)
        {
            AppointmentRepository appRepo = new AppointmentRepository();
            Appointment app = appRepo.GetByID(id.Value);

            AppointmentReview model = new AppointmentReview()
            {
                ID = app.ID,
                PatientID = app.PatientID,
                DoctorID = app.DoctorID,
                Patient = app.Patient,
                Date = app.Date,
                Status = app.Status
            };

            return View(model);
        }

        // Review POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(AppointmentReview model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppointmentRepository appRepo = new AppointmentRepository();
            Appointment app = appRepo.GetByID(model.ID);

            if (Request.Form["Approve"] != null)
            {
                model = Approve(model);
            }
            else if (Request.Form["Decline"] != null)
            {
                model = Decline(model);
            }

            app.Status = model.Status;

            appRepo.Save(app);

            return RedirectToAction("Index");
        }

        // Approve
        private AppointmentReview Approve(AppointmentReview model)
        {
            model.Status = StatusEnum.Approved;
            return model;
        }

        // Decline
        private AppointmentReview Decline(AppointmentReview model)
        {
            model.Status = StatusEnum.Unapproved;
            return model;
        }
    }
}