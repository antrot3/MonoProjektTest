using System.Linq;
using System.Net;
using System.Web.Mvc;
using Monoapp.Data.Repositories.VehicleMakes;
using Monoapp.Data.ViewModels;
using PagedList;

namespace Monoapp.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleMakesQueries _vehicleMakesQueries;
        private readonly IVehicleMakesCommands _vehicleMakesCommands;
        public VehicleMakesController()
        {
            _vehicleMakesQueries = new VehicleMakesQueries();
            _vehicleMakesCommands = new VehicleMakesCommands();
        }

        // GET: VehicleMakes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NamesortParm2 = string.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.Abrv2 = string.IsNullOrEmpty(sortOrder) ? "Abrv_Desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.currentFilter = searchString;
            var VehicleMakes = _vehicleMakesQueries.GetAllVehicleMakes();

            if (!string.IsNullOrEmpty(searchString))
            {
                VehicleMakes = VehicleMakes.Where(v => v.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_Desc":
                    VehicleMakes = VehicleMakes.OrderByDescending(b => b.Name);
                    break;
               
                case "Abrv_Desc":
                    VehicleMakes = VehicleMakes.OrderByDescending(b => b.Abrv);
                    break;
                default:
                    VehicleMakes = VehicleMakes.OrderBy(b => b.Name);
                    break;
            }
            const int PageSize = 3;
            int PageNumber = (page ?? 1);
            return View(VehicleMakes.ToPagedList(PageNumber, PageSize));
        }

        // GET: VehicleMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMake = _vehicleMakesQueries.GetVehicleMakeById((int)id);
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
                _vehicleMakesCommands.AddNewVehicleMake(vehicleMake);
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
            var vehicleMake = _vehicleMakesQueries.GetVehicleMakeById((int)id);
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
                _vehicleMakesCommands.EditVehicleMake(vehicleMake);
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
            var vehicleMake = _vehicleMakesQueries.GetVehicleMakeById((int)id);
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
            _vehicleMakesCommands.DeleteVehicleMake(id);
            return RedirectToAction("Index");
        }
    }
}
