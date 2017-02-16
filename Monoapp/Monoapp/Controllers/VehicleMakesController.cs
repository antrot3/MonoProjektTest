using System.Linq;
using System.Net;
using System.Web.Mvc;
using Monoapp.Data.Repositories;
using Monoapp.Data.ViewModels;
using PagedList;

namespace Monoapp.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleMakesRepository _vehicleMakesRepository;
        public VehicleMakesController()
        {
            _vehicleMakesRepository = new VehicleMakesRepository();
        }

        // GET: VehicleMakes
        public ActionResult Index(string sortOrder, string currentFilter, string searchByName, int? page)
        {
           
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NamesortParm2 = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.abrv2 = string.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
             int pageSize = 3;
            int pageNumber = (page ?? 1);
            var vehicleMakes = _vehicleMakesRepository.GetVehicleByMakeSearch(sortOrder,currentFilter, searchByName, page);
            
            
            return View(vehicleMakes.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: VehicleMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMake = _vehicleMakesRepository.GetVehicleMakeById((int)id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakeId,Name,Abrv")] VehicleMakeViewModel vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _vehicleMakesRepository.AddNewVehicleMake(vehicleMake);
                return RedirectToAction("Index");
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMake = _vehicleMakesRepository.GetVehicleMakeById((int)id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MakeId,Name,Abrv")] VehicleMakeViewModel vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _vehicleMakesRepository.EditVehicleMake(vehicleMake);
                return RedirectToAction("Index");
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMake = _vehicleMakesRepository.GetVehicleMakeById((int)id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _vehicleMakesRepository.DeleteVehicleMake(id);
            return RedirectToAction("Index");
        }
    }
}
