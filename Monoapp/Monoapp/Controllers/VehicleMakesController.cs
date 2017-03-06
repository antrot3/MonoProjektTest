using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Monoapp.Data.Database.Models.Entities;
using Monoapp.Data.Repositories;
using Monoapp.ViewModels;
using PagedList;
using System.Linq;

namespace Monoapp.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleMakesRepository _vehicleMakesRepository;
        public VehicleMakesController()
        {
            _vehicleMakesRepository = new VehicleMakesRepository();
        }
        public static class Constant
        {
            public static int PageSize = 3;
           
        }
        // GET: VehicleMakes
        public ActionResult Index(string sortOrder, string currentFilter, string searchByName, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.search = searchByName;
            ViewBag.NamesortParm2 = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.abrv2 = string.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
            var pageNumber = (page ?? 1);
            ViewBag.page = pageNumber;
            var vehicleMake= Mapper.Map<IEnumerable<VehicleMakeViewModel>>(
                    _vehicleMakesRepository.GetVehicleByMakeSearch(sortOrder, currentFilter, searchByName, pageNumber, Constant.PageSize)
                    );
            var totalnumber = _vehicleMakesRepository.GetAllVehicleMakes().Count();
            if (searchByName != null)
            {
                totalnumber = _vehicleMakesRepository.GetAllVehicleMakes().Where(c => c.Name.Contains(searchByName)).Count();
            }
            var vehicleMakes = new StaticPagedList<VehicleMakeViewModel>(vehicleMake, pageNumber, Constant.PageSize, totalnumber);
            return View(vehicleMakes);
        }
        
        // GET: VehicleMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMake = Mapper.Map<VehicleMakeViewModel>(
                _vehicleMakesRepository.GetVehicleMakeById((int)id)
                );

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
                _vehicleMakesRepository.AddNewVehicleMake(
                    Mapper.Map<VehicleMake>(vehicleMake)
                    );
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
            var vehicleMake = Mapper.Map<VehicleMakeViewModel>(
                _vehicleMakesRepository.GetVehicleMakeById((int)id)
                );
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
                _vehicleMakesRepository.EditVehicleMake(
                    Mapper.Map<VehicleMake>(vehicleMake));
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
            var vehicleMake = Mapper.Map<VehicleMakeViewModel>(
                _vehicleMakesRepository.GetVehicleMakeById((int)id)
                );
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
