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
    public class RequizaComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequizaCompras
        public ActionResult Index()
        {
            return View(db.RequizaCompras.ToList());
        }

        // GET: RequizaCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequizaCompra requizaCompra = db.RequizaCompras.Find(id);
            if (requizaCompra == null)
            {
                return HttpNotFound();
            }
            return View(requizaCompra);
        }

        // GET: RequizaCompras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequizaCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Proveedor,Usuario,FechaCreacion,AlmacenId")] RequizaCompra requizaCompra)
        {
            if (ModelState.IsValid)
            {
                db.RequizaCompras.Add(requizaCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requizaCompra);
        }

        // GET: RequizaCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequizaCompra requizaCompra = db.RequizaCompras.Find(id);
            if (requizaCompra == null)
            {
                return HttpNotFound();
            }
            return View(requizaCompra);
        }

        // POST: RequizaCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Proveedor,Usuario,FechaCreacion,AlmacenId")] RequizaCompra requizaCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requizaCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requizaCompra);
        }

        // GET: RequizaCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequizaCompra requizaCompra = db.RequizaCompras.Find(id);
            if (requizaCompra == null)
            {
                return HttpNotFound();
            }
            return View(requizaCompra);
        }

        // POST: RequizaCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequizaCompra requizaCompra = db.RequizaCompras.Find(id);
            db.RequizaCompras.Remove(requizaCompra);
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
