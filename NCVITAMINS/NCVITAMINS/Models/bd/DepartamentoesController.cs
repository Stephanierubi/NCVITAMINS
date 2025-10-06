using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NCVITAMINS.Models.bd
{
    public class DepartamentoesController : Controller
    {
        private db_a54362_mngtunittestEntities1 db = new db_a54362_mngtunittestEntities1();

        // GET: Departamentoes
        public ActionResult Index()
        {
            var departamentoes = db.Departamentoes.Include(d => d.Usuario).Include(d => d.Usuario1).Include(d => d.Pai);
            return View(departamentoes.ToList());
        }

        // GET: Departamentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamentoes.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamentoes/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1");
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1");
            ViewBag.Pais = new SelectList(db.Pais, "ID", "Nombre");
            return View();
        }

        // POST: Departamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Pais,Nombre,Comentario,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Departamentoes.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", departamento.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", departamento.ModifiedBy);
            ViewBag.Pais = new SelectList(db.Pais, "ID", "Nombre", departamento.Pais);
            return View(departamento);
        }

        // GET: Departamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamentoes.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", departamento.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", departamento.ModifiedBy);
            ViewBag.Pais = new SelectList(db.Pais, "ID", "Nombre", departamento.Pais);
            return View(departamento);
        }

        // POST: Departamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Pais,Nombre,Comentario,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Usuarios, "Id", "Usuario1", departamento.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Usuarios, "Id", "Usuario1", departamento.ModifiedBy);
            ViewBag.Pais = new SelectList(db.Pais, "ID", "Nombre", departamento.Pais);
            return View(departamento);
        }

        // GET: Departamentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamentoes.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamentoes.Find(id);
            db.Departamentoes.Remove(departamento);
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
