using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Monoapp.DAL;
using Monoapp.Models;
using PagedList;

namespace Monoapp.Controllers
{
    public class VehicleModelsController : Controller
    {
        private AutiContext db = new AutiContext();

        // GET: VehicleModels
        public ActionResult Index(string Sortorder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = Sortorder;
            ViewBag.NamesortParm = String.IsNullOrEmpty(Sortorder) ? "name_desc" : "";
            ViewBag.ConectionsortParam=String.IsNullOrEmpty(Sortorder)? "connection_desc" :"";
            ViewBag.abrv = String.IsNullOrEmpty(Sortorder) ? "abrv_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var vehicleModel = from v in db.VehicleModel select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModel = vehicleModel.Where(v => v.VehicleMake.Name.Contains(searchString)
                                       || v.VehicleMake.Name.Contains(searchString));
            }
            switch (Sortorder)
            {
                case "name_desc":
                    vehicleModel = vehicleModel.OrderByDescending(v => v.name);
                    break;
                case "connection_desc":
                    vehicleModel = vehicleModel.OrderByDescending(v => v.VehicleMake.Name);
                    break;
                case "abrv_desc":
                    vehicleModel = vehicleModel.OrderByDescending(v => v.abrv);
                    break;
                default:
                    vehicleModel = vehicleModel.OrderBy(v => v.name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(vehicleModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModel.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(db.VehicleMake, "MakeId", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModelId,MakeId,name,abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.VehicleModel.Add(vehicleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakeId = new SelectList(db.VehicleMake, "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModel.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeId = new SelectList(db.VehicleMake, "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModelId,MakeId,name,abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakeId = new SelectList(db.VehicleMake, "MakeId", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModel.Find(id);
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
            VehicleModel vehicleModel = db.VehicleModel.Find(id);
            db.VehicleModel.Remove(vehicleModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
