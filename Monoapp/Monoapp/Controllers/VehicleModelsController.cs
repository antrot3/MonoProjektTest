using System.Linq;
using System.Net;
using System.Web.Mvc;
using Monoapp.Data.Repositories.VehicleMakes;
using Monoapp.Data.Repositories.VehicleModel;
using Monoapp.Data.ViewModels;
using PagedList;

namespace Monoapp.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleModelsQueries _vehicleModelsQueries;
        private readonly IVehicleModelsCommands _vehicleModelsCommands;
        private readonly IVehicleMakesQueries _vehicleMakesQueries;

        public VehicleModelsController()
        {
            _vehicleModelsCommands = new VehicleModelsCommands();
            _vehicleModelsQueries = new VehicleModelsQueries();
            _vehicleMakesQueries = new VehicleMakesQueries();
        }

        // GET: VehicleModels
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NamesortParm = string.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.ConectionsortParam= string.IsNullOrEmpty(sortOrder)? "Connection_Desc" :"";
            ViewBag.Abrv = string.IsNullOrEmpty(sortOrder) ? "Abrv_Desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.currentFilter = searchString;

            var VehicleModels = _vehicleModelsQueries.GetAllVehicleModels();
            if (!string.IsNullOrEmpty(searchString))
            {
                VehicleModels = VehicleModels.Where(v => v.VehicleMake.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_Desc":
                    VehicleModels = VehicleModels.OrderByDescending(v => v.Name);
                    break;
                case "Connection_Desc":
                    VehicleModels = VehicleModels.OrderByDescending(v => v.VehicleMake.Name);
                    break;
                case "Abrv_Desc":
                    VehicleModels = VehicleModels.OrderByDescending(v => v.Abrv);
                    break;
                default:
                    VehicleModels = VehicleModels.OrderBy(v => v.Name);
                    break;
            }

            const int PageSize = 3;
            int PageNumber = (page ?? 1);
            return View(VehicleModels.ToPagedList(PageNumber, PageSize));
        }

        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = _vehicleModelsQueries.GetVehicleModelById((int) id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(_vehicleMakesQueries.GetAllVehicleMakes(), "MakeId", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModelId,MakeId,Name,Abrv")] VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _vehicleModelsCommands.AddNewVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }

            ViewBag.MakeId = new SelectList(_vehicleMakesQueries.GetAllVehicleMakes(), "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = _vehicleModelsQueries.GetVehicleModelById((int)id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeId = new SelectList(_vehicleMakesQueries.GetAllVehicleMakes(), "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModelId,MakeId,Name,Abrv")] VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _vehicleModelsCommands.EditVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }
            ViewBag.MakeId = new SelectList(_vehicleMakesQueries.GetAllVehicleMakes(), "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = _vehicleModelsQueries.GetVehicleModelById((int)id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _vehicleModelsCommands.DeleteVehicleModel(id);
            return RedirectToAction("Index");
        }
    }
}
