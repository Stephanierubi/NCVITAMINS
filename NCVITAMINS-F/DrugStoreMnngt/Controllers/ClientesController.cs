using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrugStoreMnngt.Models;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class ClientesController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            var tiendas = (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda"
                           select
                           a
                ).ToDictionary(x=>x.Id,x=>x.Name);


            ViewBag.Tienda = tiendas;
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult ComprasCliente(int? id) { 

            var comprasCliente = db.vw_CompraSCliente.Where(z => z.Cliente == id).ToList();


            return View(comprasCliente); 
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            var tiendas = (from a in db.CatalogDetail
                       join b in db.Catalog on a.CatalogId equals b.Id
                       where
                       b.Name == "Tienda"
                       select
                       a
                 ).ToList();

            ViewBag.Tienda = new SelectList(tiendas, "ID", "Name");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var today = CustomValuesFormat.currentDateTimeUTC();
                cliente.CreatedOn = today;
                cliente.ModifiedOn = today;
                cliente.CreatedBy = InformationIdentity.GetID(); ;
                cliente.ModifiedBy = InformationIdentity.GetID(); ;
                cliente.Activo = true;
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        [HttpPost]

        public ActionResult agregarCliente(Collection_invoice info)
        {
            Cliente cliente = new Cliente();

            var today = CustomValuesFormat.currentDateTimeUTC();
            cliente.Nombres = info.nombreCliente;
            cliente.Apellidos = info.apellidoCliente;
            cliente.Telefono = info.telefono;
            cliente.FechaNacimiento = info.fechaNacimiento;
            cliente.Activo= true;
            cliente.CreatedOn = today;
            cliente.ModifiedOn = today;
            cliente.CreatedBy = InformationIdentity.GetID(); ;
            cliente.ModifiedBy = InformationIdentity.GetID(); ;
            cliente.Activo = true;
            db.Cliente.Add(cliente);
            db.SaveChanges();


            return RedirectToAction("Invoice", "Facturas");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
         
            var tiendas = (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda"
                           select
                           a
          ).ToList();

            ViewBag.Tienda = new SelectList(tiendas, "ID", "Name");

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Nombres,Apellidos,Telefono,Activo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,Tienda,FechaNacimiento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                var today = CustomValuesFormat.currentDateTimeUTC();
                cliente.ModifiedOn = today;
                cliente.ModifiedBy = InformationIdentity.GetID(); 

                //temp
                cliente.CreatedBy = InformationIdentity.GetID(); 
                cliente.CreatedOn = today;
                cliente.Activo = true;

                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            cliente.Activo = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
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
