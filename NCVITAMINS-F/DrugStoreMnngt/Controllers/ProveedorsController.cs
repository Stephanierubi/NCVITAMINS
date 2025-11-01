using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class ProveedorsController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: Proveedors
        public ActionResult Index()
        {
            return View(db.Proveedor.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "ID,Nombre,Direccion,Telefono,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                proveedor.CreatedBy = InformationIdentity.GetID();
                proveedor.CreatedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                proveedor.ModifiedBy = InformationIdentity.GetID();
                proveedor.ModifiedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                proveedor.Activo = true;
                db.Proveedor.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Edit([Bind(Include = "ID,Nombre,Direccion,Telefono,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                
                proveedor.ModifiedBy = InformationIdentity.GetID();
                proveedor.ModifiedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                proveedor.Activo = true;
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]        
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
            db.Proveedor.Remove(proveedor);
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
