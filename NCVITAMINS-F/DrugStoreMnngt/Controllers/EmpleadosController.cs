using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrugStoreMnngt.Models;
using DrugStoreMnngt.Models.BD;
using DrugStoreMnngt.Models.CustomInsert;
using Microsoft.AspNet.Identity;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class EmpleadosController : Controller
    {
        public DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: Empleados
        public ActionResult Index()
        {

            var valuereturn = db.vw_DatosEmpleados.ToList();
            return View(valuereturn);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            List<CatalogDetail> estadoEmpleado = new List<CatalogDetail>();

            estadoEmpleado = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "EstadoEmpleado" && a.IsActive == true && b.IsActive == true
                              select
            a
                 ).ToList();
            

            ViewBag.estadoEmpleado = new SelectList(estadoEmpleado, "ID", "Name");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]      
        public ActionResult Create(EmpleadoCustomInsert empleadoCustom)
        {
            if (ModelState.IsValid)
            {
                Direccion direccion = new Direccion();
                Telefono telefono = new Telefono();

                Empleado empleado = new Empleado();

                empleado.Nombres = empleadoCustom.Nombres;
                empleado.Apellidos = empleadoCustom.Apellidos;

                var estadoDireccion = (from a in db.CatalogDetail
                                      join b in db.Catalog on a.CatalogId equals b.Id
                                      where
                                      b.Name == "EstadoDireccion" && a.IsActive == true && b.IsActive == true
                                      select
                    a
                 ).FirstOrDefault();

                direccion.Direccion1 = empleadoCustom.Direccion;
                direccion.Empleado = true;
                direccion.Activo = true;
                direccion.Estado = estadoDireccion.Id;
                direccion.Comentario = "";
                direccion.ModifiedBy = InformationIdentity.GetID();
                direccion.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
                direccion.CreatedBy = InformationIdentity.GetID();
                direccion.CreatedOn = CustomValuesFormat.currentDateTimeUTC();

                telefono.Telefono1 = empleadoCustom.Telefono;      
                telefono.Empleado= true; telefono.Activo= true;
                telefono.Comentario = "";
                telefono.ModifiedBy = InformationIdentity.GetID();
                telefono.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
                telefono.CreatedBy = InformationIdentity.GetID();
                telefono.CreatedOn = CustomValuesFormat.currentDateTimeUTC();


                empleado.Estado = empleadoCustom.Estado;
                empleado.ModifiedBy = InformationIdentity.GetID();
                empleado.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
                empleado.CreatedBy = InformationIdentity.GetID();
                empleado.CreatedOn = CustomValuesFormat.currentDateTimeUTC();
                
                db.Empleado.Add(empleado);
                db.SaveChanges();

                var emp = db.Empleado.FirstOrDefault(z => z.ID == empleado.ID);

                telefono.IdentificacionPerteneciente= emp.ID; 
                direccion.IdentificacionPerteneciente = emp.ID;

                db.Direccion.Add(direccion);
                db.Telefono.Add(telefono);
                db.SaveChanges();

                
                emp.Telefono = telefono.ID;
                emp.Direccion = direccion.ID;

                db.Empleado.AddOrUpdate(emp);

                return RedirectToAction("Index");
            }

            return View(empleadoCustom);
        }


        public ActionResult AgregarEmpleadoUsuario(int? id)
        {
            String nEmpleado = "";
            EmpleadoUsarioCustomInsert eu = new EmpleadoUsarioCustomInsert();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            UsuarioEmpleado uempleado = db.UsuarioEmpleado.Where(z=>z.Empleado==empleado.ID).FirstOrDefault();

            if (uempleado == null)
            {
                eu.ID = 0;
                eu.Empleado = empleado.ID;
                eu.NombreEmpleado = empleado.ID + " - " + empleado.Nombres + " " + empleado.Apellidos;
            }
            else
            {
                eu.ID = uempleado.ID;                
                eu.NombreEmpleado = empleado.ID + " - " + empleado.Nombres + " " + empleado.Apellidos;
                eu.Empleado = uempleado.Empleado;
                eu.Usuario = uempleado.Usuario;
            }

            nEmpleado = empleado.ID + " - " + empleado.Nombres + " " + empleado.Apellidos;

            var usuarios = (from a in db.Usuarios.Where(w => (w.ACTIVO == true))
                            select new
                            {
                                a.Id,
                                Name =a.Usuario,                                
                            }
                     ).ToList();

            ViewBag.NombreEmpleado = nEmpleado;
            ViewBag.Usuarios = new SelectList(usuarios, "ID", "Name");
            return View(eu);
        }

        [HttpPost]
        public ActionResult AgregarEmpleadoUsuario( EmpleadoUsarioCustomInsert empleadoUsarioCustomInsert)
        {            

            var u = db.UsuarioEmpleado.Where(z=>z.Empleado == empleadoUsarioCustomInsert.Empleado).FirstOrDefault();

            if (ModelState.IsValid)
            {
                    UsuarioEmpleado  ue= new UsuarioEmpleado();
                    ue.ID = u == null?0: u.ID;
                    ue.Activo = true;
                    ue.Usuario = empleadoUsarioCustomInsert.Usuario;
                    ue.Empleado= empleadoUsarioCustomInsert.Empleado;
                    ue.ModifiedBy = InformationIdentity.GetID();
                    ue.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
                    ue.CreatedBy = InformationIdentity.GetID();
                    ue.CreatedOn = CustomValuesFormat.currentDateTimeUTC();
                    db.UsuarioEmpleado.AddOrUpdate(ue);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleadoUsarioCustomInsert);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
           
            return View(empleado);
        }



       

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Edit([Bind(Include = "ID,Nombres,Apellidos,Activo,CreatedOn,ModifiedOn")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]        
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
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
