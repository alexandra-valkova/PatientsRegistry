using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using PatientsRegistry.ViewModels;

namespace PatientsRegistry.Controllers
{
    public abstract class BaseController<TEntity, TList, TDetails> : Controller
        where TEntity : BaseEntity, new()
        where TList : AppointmentsListViewModel, new()
        where TDetails : AppointmentDetailsViewModel, new()
    {
        public abstract BaseRepository<TEntity> GetRepository();

        public abstract TList PopulateListViewModel(TList model);

        public abstract TDetails PopulateDetailsViewModel(TDetails model);

        public ActionResult Index()
        {
            TList model = new TList();
            PopulateListViewModel(model);

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            TDetails model = new TDetails()
            {
                ID = id.Value
            };
            PopulateDetailsViewModel(model);

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            BaseRepository<TEntity> entityRepository = GetRepository();
            TEntity entity = entityRepository.GetByID(id.Value);

            if (entity == null)
            {
                return HttpNotFound();
            }

            entityRepository.Delete(entity);

            return RedirectToAction("Index");
        }
    }
}