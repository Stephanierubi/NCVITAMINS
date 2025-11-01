using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCustomization.CustomClasses;
using DrugStoreMnngt.Models;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    public class COMPANIAsController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: COMPANIAs
        public ActionResult Index()
        {
            int usuarioLog = InformationIdentity.GetID();
            
            var logUser = db.Usuarios.Where(z=> z.Id == usuarioLog).FirstOrDefault();

            ViewBag.UsuarioMaestro = logUser.SUCURSAL ?? 0;
            if(logUser.SUCURSAL == null)
            {
                return View(db.COMPANIAs.ToList());
            }
            else
            {
                int idcompania = (
                    from a in db.SUCURSALs where a.ID == logUser.SUCURSAL select a
                    ).FirstOrDefault().COMPANIA??0;

                return View(db.COMPANIAs.Where(z=>z.ID== idcompania).ToList());
            }
            
           
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

        public ActionResult sucursalesCompania(int? compania)
        {
            if (compania == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SUCURSAL> sucursales = db.SUCURSALs.Where(z=> z.COMPANIA == compania).ToList();
            if (sucursales == null)
            {
                return HttpNotFound();
            }

            ViewBag.Compania = compania;
            return View(sucursales);
        }

        public ActionResult configuracionSucursal(int? sucursal)
        {

            if (sucursal == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<configuracionSucursalCompania> sucursales =  (from a in db.CONFIGURACIONTIENDASUCURSALs.Where(z => z.SUCURSAL == sucursal)
                                          join b in db.CatalogDetail on a.TIENDA equals b.Id
                                          join c in db.SUCURSALs on a.SUCURSAL equals c.ID
                                          select new configuracionSucursalCompania
                                          {
                                              ID = a.ID,
                                              IDSucursal = a.SUCURSAL??0,
                                              sucursal = c.NOMBRE,
                                              tienda = b.Name,
                                              comentario = a.Comentario
                                          }
                                          ).ToList();
            if (sucursales == null)
            {
                return HttpNotFound();
            }

            ViewBag.Sucursal = sucursal;
            return View(sucursales);
        }

        // GET: COMPANIAs/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]      
        public ActionResult Create(COMPANIA cOMPANIA)
        {
            if (ModelState.IsValid)
            {
                cOMPANIA.ModifiedBy = InformationIdentity.GetID();
                cOMPANIA.CreatedBy =InformationIdentity.GetID();
                cOMPANIA.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
                cOMPANIA.CreatedOn = CustomValuesFormat.currentDateTimeUTC();
                db.COMPANIAs.Add(cOMPANIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(cOMPANIA);
        }

        [HttpPost]       
        public ActionResult Edit(COMPANIA cOMPANIA)
        {
            if (ModelState.IsValid)
            {
                cOMPANIA.ModifiedBy = InformationIdentity.GetID();                
                cOMPANIA.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();                
                db.Entry(cOMPANIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
