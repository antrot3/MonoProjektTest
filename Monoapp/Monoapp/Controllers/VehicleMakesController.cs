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
    public class VehicleMakesController : Controller
    {
        private AutiContext db = new AutiContext();

        // GET: VehicleMakes
        public ActionResult Index(string Sortorder2, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = Sortorder2;
            ViewBag.NamesortParm2 = String.IsNullOrEmpty(Sortorder2) ? "name_desc" : "";
            ViewBag.abrv2 = String.IsNullOrEmpty(Sortorder2) ? "abrv_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var VehicleMake = from b in db.VehicleMake select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                VehicleMake = VehicleMake.Where(v => v.Name.Contains(searchString)
                                       || v.Name.Contains(searchString));
            }
            switch (Sortorder2)
            {
                case "name_desc":
                    VehicleMake = VehicleMake.OrderByDescending(b => b.Name);
                    break;
               
                case "abrv_desc":
                    VehicleMake = VehicleMake.OrderByDescending(b => b.Abrv);
                    break;
                default:
                    VehicleMake = VehicleMake.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(VehicleMake.ToPagedList(pageNumber, pageSize));
        }

        // GET: VehicleMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = db.VehicleMake.Find(id);
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
        public ActionResult Create([Bind(Include = "MakeId,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                db.VehicleMake.Add(vehicleMake);
                db.SaveChanges();
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
            VehicleMake vehicleMake = db.VehicleMake.Find(id);
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
        public ActionResult Edit([Bind(Include = "MakeId,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleMake).State = EntityState.Modified;
                db.SaveChanges();
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
            VehicleMake vehicleMake = db.VehicleMake.Find(id);
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
            VehicleMake vehicleMake = db.VehicleMake.Find(id);
            db.VehicleMake.Remove(vehicleMake);
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
