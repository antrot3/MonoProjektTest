using System.Linq;
using System.Net;
using System.Web.Mvc;
using Monoapp.Data.Repositories;
using Monoapp.Data.ViewModels;
using PagedList;

namespace Monoapp.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IVehicleMakesRepository _vehicleMakesRepository;

        public VehicleModelsController()
        {
            _vehicleModelRepository = new VehicleModelRepository();
            _vehicleMakesRepository= new VehicleMakesRepository();
        }

        // GET: VehicleModels
        public ActionResult Index(string sortOrder, string currentFilter, string searchByName, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NamesortParm = string.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.ConectionsortParam = string.IsNullOrEmpty(sortOrder) ? "Connection_Desc" : "";
            ViewBag.Abrv = string.IsNullOrEmpty(sortOrder) ? "Abrv_Desc" : "";
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var vehicleModels = _vehicleModelRepository.GetVehicleByModelSearch(sortOrder, currentFilter, searchByName, page);


            return View(vehicleModels.ToPagedList(pageNumber, pageSize));

        }
        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = _vehicleModelRepository.GetVehicleModelById((int) id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(_vehicleMakesRepository.GetAllVehicleMakes(), "MakeId", "Name");
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
                _vehicleModelRepository.AddNewVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }

            ViewBag.MakeId = new SelectList(_vehicleMakesRepository.GetAllVehicleMakes(), "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = _vehicleModelRepository.GetVehicleModelById((int)id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeId = new SelectList(_vehicleMakesRepository.GetAllVehicleMakes(), "MakeId", "Name", vehicleModel.MakeId);
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
                _vehicleModelRepository.EditVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }
            ViewBag.MakeId = new SelectList(_vehicleMakesRepository.GetAllVehicleMakes(), "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = _vehicleModelRepository.GetVehicleModelById((int)id);
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
            _vehicleModelRepository.DeleteVehicleModel(id);
            return RedirectToAction("Index");
        }
    }
}
