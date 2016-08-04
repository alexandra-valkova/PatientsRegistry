using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.ViewModels;
using System.Web.Mvc;

namespace PatientsRegistry.Controllers
{
    public abstract class BaseController<T, L, D> : Controller
        where T : BaseEntity, new()
        where L : AppointmentsListVM, new()
        where D : AppointmentDetailsVM, new()
    {
        public abstract BaseRepository<T> GetRepo();

        public abstract L PopulateListVM(L model);

        public abstract D PopulateDetailsVM(D model);

        public ActionResult Index()
        {
            L model = new L();
            PopulateListVM(model);
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            D model = new D()
            {
                ID = id.Value
            };
            PopulateDetailsVM(model);

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            BaseRepository<T> entityRepo = GetRepo();
            T entity = entityRepo.GetByID(id.Value);

            if (entity == null)
            {
                return HttpNotFound();
            }

            entityRepo.Delete(entity);

            return RedirectToAction("Index");
        }
    }
}