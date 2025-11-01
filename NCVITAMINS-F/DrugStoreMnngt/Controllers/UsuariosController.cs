using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCustomization.CustomClasses;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class UsuariosController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = (from
                            a in db.Usuarios
                            join b in db.SUCURSALs on a.SUCURSAL equals b.ID into bjoin
                            from sucursal in bjoin.DefaultIfEmpty()
                            join c in db.CatalogDetail on a.Store equals c.Id into cjoin
                            from tienda in cjoin.DefaultIfEmpty()
                            select new UsuariosCustom

                            {
                                Id = a.Id,
                                Usuario = a.Usuario,
                                ACTIVO = a.ACTIVO,
                                Contrasenia = a.Contrasenia,
                                Store = a.Store,
                                SUCURSAL = a.SUCURSAL,
                                Tipo = a.Tipo,
                                nombreSucursal = sucursal == null ? "No Asignada" : sucursal.NOMBRE,
                                nombreTienda = tienda == null ? "Todas" : tienda.Name
                            }).ToList();
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            int idUsusaruioLogeado = InformationIdentity.GetID();

            var usuario = db.Usuarios.Where(z=>z.Id == idUsusaruioLogeado).FirstOrDefault();
            var sucursalLog= db.SUCURSALs.Where(z=>z.ID == usuario.SUCURSAL).FirstOrDefault();

            var listaSucursales = db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA).ToList();

            List<int> sucursalesId = (from a in db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA)
                select a).Select(x=>x.ID).ToList();

            List<int> tienda = (from a in db.CONFIGURACIONTIENDASUCURSALs.Where(z =>
            sucursalesId.Contains(z.SUCURSAL??0)) select a).Select(x=>x.TIENDA??0).ToList();


            List<CatalogDetail> tiendas = new List<CatalogDetail>();
            CatalogDetail t = new CatalogDetail();
            t.Id = 0;
            t.Name = "Todas";
            tiendas.Add(t);
            tiendas.AddRange(db.CatalogDetail.Where(z => tienda.Contains(z.Id)));


            ViewBag.Sucursal = new SelectList(listaSucursales, "ID", "Nombre");
            ViewBag.Store = new SelectList(tiendas, "Id", "Name");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                usuarios.ACTIVO = true;
                if (usuarios.Store == 0)
                    usuarios.Store = null;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int idUsusaruioLogeado = InformationIdentity.GetID();

            var usuario = db.Usuarios.Where(z => z.Id == idUsusaruioLogeado).FirstOrDefault();
            var sucursalLog = db.SUCURSALs.Where(z => z.ID == usuario.SUCURSAL).FirstOrDefault();

            var listaSucursales = db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA).ToList();

            List<int> sucursalesId = (from a in db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA)
                                      select a).Select(x => x.ID).ToList();

            List<int> tienda = (from a in db.CONFIGURACIONTIENDASUCURSALs.Where(z =>
            sucursalesId.Contains(z.SUCURSAL ?? 0))
                                select a).Select(x => x.TIENDA ?? 0).ToList();


            List<CatalogDetail> tiendas = new List<CatalogDetail>();
            CatalogDetail t = new CatalogDetail();
            t.Id = 0;
            t.Name = "Todas";
            tiendas.Add(t);
            tiendas.AddRange(db.CatalogDetail.Where(z => tienda.Contains(z.Id)));


            ViewBag.Sucursal = new SelectList(listaSucursales, "ID", "Nombre");
            ViewBag.Store = new SelectList(tiendas, "Id", "Name");
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            int idUsusaruioLogeado = InformationIdentity.GetID();

            var usuario = db.Usuarios.Where(z => z.Id == idUsusaruioLogeado).FirstOrDefault();
            var sucursalLog = db.SUCURSALs.Where(z => z.ID == usuario.SUCURSAL).FirstOrDefault();

            var listaSucursales = db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA).ToList();

            List<int> sucursalesId = (from a in db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA)
                                      select a).Select(x => x.ID).ToList();

            List<int> tienda = (from a in db.CONFIGURACIONTIENDASUCURSALs.Where(z =>
            sucursalesId.Contains(z.SUCURSAL ?? 0))
                                select a).Select(x => x.TIENDA ?? 0).ToList();


            List<CatalogDetail> tiendas = new List<CatalogDetail>();
            CatalogDetail t = new CatalogDetail();
            t.Id = 0;
            t.Name = "Todas";
            tiendas.Add(t);
            tiendas.AddRange(db.CatalogDetail.Where(z => tienda.Contains(z.Id)));


            ViewBag.Sucursal = new SelectList(listaSucursales, "ID", "Nombre");
            ViewBag.Store = new SelectList(tiendas, "Id", "Name");
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if (usuarios.Store == 0)
                {
                    usuarios.Store = null;
                }
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int idUsusaruioLogeado = InformationIdentity.GetID();

            var usuario = db.Usuarios.Where(z => z.Id == idUsusaruioLogeado).FirstOrDefault();
            var sucursalLog = db.SUCURSALs.Where(z => z.ID == usuario.SUCURSAL).FirstOrDefault();

            var listaSucursales = db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA).ToList();

            List<int> sucursalesId = (from a in db.SUCURSALs.Where(z => z.COMPANIA == sucursalLog.COMPANIA)
                                      select a).Select(x => x.ID).ToList();

            List<int> tienda = (from a in db.CONFIGURACIONTIENDASUCURSALs.Where(z =>
            sucursalesId.Contains(z.SUCURSAL ?? 0))
                                select a).Select(x => x.TIENDA ?? 0).ToList();


            List<CatalogDetail> tiendas = new List<CatalogDetail>();
            CatalogDetail t = new CatalogDetail();
            t.Id = 0;
            t.Name = "Todas";
            tiendas.Add(t);
            tiendas.AddRange(db.CatalogDetail.Where(z => tienda.Contains(z.Id)));


            ViewBag.Sucursal = new SelectList(listaSucursales, "ID", "Nombre");
            ViewBag.Store = new SelectList(tiendas, "Id", "Name");
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        public ActionResult Desactivar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        [HttpPost]
         public ActionResult Desactivar(Usuarios user)
        {
            Usuarios usuarios = db.Usuarios.Find(user.Id);
            if (usuarios != null)
            {
                usuarios.ACTIVO = false;

                db.Usuarios.AddOrUpdate(usuarios);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        public ActionResult Activar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult Activar(Usuarios user)
        {
            Usuarios usuarios = db.Usuarios.Find(user.Id);
            if (usuarios != null)
            {
                usuarios.ACTIVO = true;

                db.Usuarios.AddOrUpdate(usuarios);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
 
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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
