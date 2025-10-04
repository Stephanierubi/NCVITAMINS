using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCVITAMINS.Models.bd;

namespace NCVITAMINS.Controllers
{
    public class COMPANIAsController : Controller
    {
        private db_a54362_mngtunittestEntities1 db = new db_a54362_mngtunittestEntities1();

        // GET: COMPANIAs
        public ActionResult Index()
        {
            var cOMPANIAs = db.COMPANIAs.Include(c => c.Usuario).Include(c => c.Direccion1).Include(c => c.Usuario1).Include(c => c.Telefono1);
            return View(cOMPANIAs.ToList());
        }

        // GET: COMPANIAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANIA cOMPANIA = db.COMPANIAs.Find(id);
            if (cOMPANIA == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANIA);
        }

        // GET: COMPANIAs/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1");
            ViewBag.DIRECCION = new SelectList(db.Direccions, "ID", "Direccion1");
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1");
            ViewBag.TELEFONO = new SelectList(db.Telefonoes, "ID", "Telefono1");
            return View();
        }

        // POST: COMPANIAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,DESCRIPCION,IDENTIFICACION,DIRECCION,TELEFONO,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] COMPANIA cOMPANIA)
        {
            if (ModelState.IsValid)
            {
                db.COMPANIAs.Add(cOMPANIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", cOMPANIA.CreatedBy);
            ViewBag.DIRECCION = new SelectList(db.Direccions, "ID", "Direccion1", cOMPANIA.DIRECCION);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", cOMPANIA.ModifiedBy);
            ViewBag.TELEFONO = new SelectList(db.Telefonoes, "ID", "Telefono1", cOMPANIA.TELEFONO);
            return View(cOMPANIA);
        }

        // GET: COMPANIAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANIA cOMPANIA = db.COMPANIAs.Find(id);
            if (cOMPANIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", cOMPANIA.CreatedBy);
            ViewBag.DIRECCION = new SelectList(db.Direccions, "ID", "Direccion1", cOMPANIA.DIRECCION);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", cOMPANIA.ModifiedBy);
            ViewBag.TELEFONO = new SelectList(db.Telefonoes, "ID", "Telefono1", cOMPANIA.TELEFONO);
            return View(cOMPANIA);
        }

        // POST: COMPANIAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,DESCRIPCION,IDENTIFICACION,DIRECCION,TELEFONO,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] COMPANIA cOMPANIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMPANIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", cOMPANIA.CreatedBy);
            ViewBag.DIRECCION = new SelectList(db.Direccions, "ID", "Direccion1", cOMPANIA.DIRECCION);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", cOMPANIA.ModifiedBy);
            ViewBag.TELEFONO = new SelectList(db.Telefonoes, "ID", "Telefono1", cOMPANIA.TELEFONO);
            return View(cOMPANIA);
        }

        // GET: COMPANIAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANIA cOMPANIA = db.COMPANIAs.Find(id);
            if (cOMPANIA == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANIA);
        }

        // POST: COMPANIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COMPANIA cOMPANIA = db.COMPANIAs.Find(id);
            db.COMPANIAs.Remove(cOMPANIA);
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
