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
    public class ProductoesController : Controller
    {
        private db_a54362_mngtunittestEntities1 db = new db_a54362_mngtunittestEntities1();

        // GET: Productoes
        public ActionResult Index()
        {
            var productoes = db.Productoes.Include(p => p.CatalogDetail).Include(p => p.CatalogDetail1).Include(p => p.CatalogDetail2).Include(p => p.CatalogDetail3).Include(p => p.Proveedor);
            return View(productoes.ToList());
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.Almacen = new SelectList(db.CatalogDetails, "Id", "Name");
            ViewBag.Laboratorio = new SelectList(db.CatalogDetails, "Id", "Name");
            ViewBag.TipoPrecentacionID = new SelectList(db.CatalogDetails, "Id", "Name");
            ViewBag.TipoVentaID = new SelectList(db.CatalogDetails, "Id", "Name");
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ID", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Marca,DescripcionEspecifica,DescripcionGeneral,FechaVencimiento,TipoPrecentacionID,PrecioCompra,PrecioVenta,ProveedorID,CodigoBarra,CantidadDisponible,CantidadPrecentacion,TipoVentaID,Almacen,Laboratorio,Activo,IVA,StockMinimo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,CodigoProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productoes.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Almacen = new SelectList(db.CatalogDetails, "Id", "Name", producto.Almacen);
            ViewBag.Laboratorio = new SelectList(db.CatalogDetails, "Id", "Name", producto.Laboratorio);
            ViewBag.TipoPrecentacionID = new SelectList(db.CatalogDetails, "Id", "Name", producto.TipoPrecentacionID);
            ViewBag.TipoVentaID = new SelectList(db.CatalogDetails, "Id", "Name", producto.TipoVentaID);
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ID", "Nombre", producto.ProveedorID);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Almacen = new SelectList(db.CatalogDetails, "Id", "Name", producto.Almacen);
            ViewBag.Laboratorio = new SelectList(db.CatalogDetails, "Id", "Name", producto.Laboratorio);
            ViewBag.TipoPrecentacionID = new SelectList(db.CatalogDetails, "Id", "Name", producto.TipoPrecentacionID);
            ViewBag.TipoVentaID = new SelectList(db.CatalogDetails, "Id", "Name", producto.TipoVentaID);
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ID", "Nombre", producto.ProveedorID);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Marca,DescripcionEspecifica,DescripcionGeneral,FechaVencimiento,TipoPrecentacionID,PrecioCompra,PrecioVenta,ProveedorID,CodigoBarra,CantidadDisponible,CantidadPrecentacion,TipoVentaID,Almacen,Laboratorio,Activo,IVA,StockMinimo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,CodigoProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Almacen = new SelectList(db.CatalogDetails, "Id", "Name", producto.Almacen);
            ViewBag.Laboratorio = new SelectList(db.CatalogDetails, "Id", "Name", producto.Laboratorio);
            ViewBag.TipoPrecentacionID = new SelectList(db.CatalogDetails, "Id", "Name", producto.TipoPrecentacionID);
            ViewBag.TipoVentaID = new SelectList(db.CatalogDetails, "Id", "Name", producto.TipoVentaID);
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ID", "Nombre", producto.ProveedorID);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productoes.Find(id);
            db.Productoes.Remove(producto);
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
