using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCVITAMINS.Models;

namespace NCVITAMINS.Models.bd
{
    public class ExistenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Existencias
        public ActionResult Index()
        {
            return View(db.Existencias.ToList());
        }

        // GET: Existencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencia existencia = db.Existencias.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // GET: Existencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Existencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDProducto,IDAlmacen,Cantidad,CreatetedOn,CreatedBy")] Existencia existencia)
        {
            if (ModelState.IsValid)
            {
                db.Existencias.Add(existencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(existencia);
        }

        // GET: Existencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencia existencia = db.Existencias.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // POST: Existencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDProducto,IDAlmacen,Cantidad,CreatetedOn,CreatedBy")] Existencia existencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(existencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(existencia);
        }

        // GET: Existencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencia existencia = db.Existencias.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // POST: Existencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Existencia existencia = db.Existencias.Find(id);
            db.Existencias.Remove(existencia);
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
