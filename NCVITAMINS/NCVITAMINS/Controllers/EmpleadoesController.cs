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
    public class EmpleadoesController : Controller
    {
        private db_a54362_mngtunittestEntities1 db = new db_a54362_mngtunittestEntities1();

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleadoes = db.Empleadoes.Include(e => e.CatalogDetail).Include(e => e.CatalogDetail1).Include(e => e.Direccion1).Include(e => e.Usuario).Include(e => e.Usuario1).Include(e => e.Telefono1);
            return View(empleadoes.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.CatalogDetails, "Id", "Name");
            ViewBag.Genero = new SelectList(db.CatalogDetails, "Id", "Name");
            ViewBag.Direccion = new SelectList(db.Direccions, "ID", "Direccion1");
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1");
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1");
            ViewBag.Telefono = new SelectList(db.Telefonoes, "ID", "Telefono1");
            return View();
        }

        // POST: Empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombres,Apellidos,Direccion,Telefono,Activo,Estado,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,FechaNacimiento,Genero")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleadoes.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.CatalogDetails, "Id", "Name", empleado.Estado);
            ViewBag.Genero = new SelectList(db.CatalogDetails, "Id", "Name", empleado.Genero);
            ViewBag.Direccion = new SelectList(db.Direccions, "ID", "Direccion1", empleado.Direccion);
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", empleado.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", empleado.ModifiedBy);
            ViewBag.Telefono = new SelectList(db.Telefonoes, "ID", "Telefono1", empleado.Telefono);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.CatalogDetails, "Id", "Name", empleado.Estado);
            ViewBag.Genero = new SelectList(db.CatalogDetails, "Id", "Name", empleado.Genero);
            ViewBag.Direccion = new SelectList(db.Direccions, "ID", "Direccion1", empleado.Direccion);
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", empleado.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", empleado.ModifiedBy);
            ViewBag.Telefono = new SelectList(db.Telefonoes, "ID", "Telefono1", empleado.Telefono);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombres,Apellidos,Direccion,Telefono,Activo,Estado,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,FechaNacimiento,Genero")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.CatalogDetails, "Id", "Name", empleado.Estado);
            ViewBag.Genero = new SelectList(db.CatalogDetails, "Id", "Name", empleado.Genero);
            ViewBag.Direccion = new SelectList(db.Direccions, "ID", "Direccion1", empleado.Direccion);
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", empleado.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", empleado.ModifiedBy);
            ViewBag.Telefono = new SelectList(db.Telefonoes, "ID", "Telefono1", empleado.Telefono);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleadoes.Find(id);
            db.Empleadoes.Remove(empleado);
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
