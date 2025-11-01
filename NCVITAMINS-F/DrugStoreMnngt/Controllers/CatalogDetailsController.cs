using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrugStoreMnngt.Models;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class CatalogDetailsController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: CatalogDetails
        public ActionResult Index()
        {
            String xx = DateTime.Now.ToString();
            String xt = CustomValuesFormat.currentDateTimeUTC().ToString();
            return View(db.CatalogDetail.Where(z => z.CatalogId == 5).ToList());
        }

        public ActionResult TipoPrecentacion()
        {
            return View(db.CatalogDetail.Where(z => z.CatalogId == 4).ToList());
        }

        public ActionResult Laboratorio()
        {
            return View(db.CatalogDetail.Where(z => z.CatalogId == 6).ToList());
        }

        public ActionResult Almacen()
        {
            return View(db.CatalogDetail.Where(z => z.CatalogId == 3).ToList());
        }

        public ActionResult Tienda()
        {
            return View(db.CatalogDetail.Where(z => z.CatalogId == 8).ToList());
        }

        // GET: CatalogDetails/Details/5
        public ActionResult Details(int? id, string Tipo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogDetail catalogDetail = db.CatalogDetail.Find(id);
            if (catalogDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title2 = Tipo;
            return View(catalogDetail);
        }

        //public JsonResult SetDefaultStorage(int id)
        //{
        //    var catalogDetail = db.CatalogDetail.FirstOrDefault(f => f.Id == id);
        //    catalogDetail.Isdefault = true;
        //    catalogDetail.ModifiedBy = InformationIdentity.GetID();
        //    catalogDetail.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
        //    catalogDetail.IsActive = true;
        //    var storage = db.CatalogDetail.Where(w => w.CatalogId == 3 && w.Id != id).ToList();

        //    storage.ForEach(f =>
        //    {
        //        f.Isdefault = false;
        //    });

        //    db.SaveChanges();
        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        //public void SetNoDefaultStorage(int id, int tiendaId)
        //{

        //    var storage = db.CatalogDetail.Where(w => w.CatalogId == 3 && w.Id != id && w.ParentId == tiendaId).ToList();

        //    storage.ForEach(f =>
        //    {
        //        f.Isdefault = false;
        //        f.ParentId = null;
        //    });

        //    db.SaveChanges();
        //}

        // GET: CatalogDetails/Create
        public ActionResult Create(string Tipo)
        {
            ViewBag.Title2 = Tipo;

            CatalogDetail inf = new CatalogDetail();
            if (Tipo == "TipoVenta")
            {
                inf.CatalogId = 5;
            }
            else if (Tipo == "TipoPrecentacion")
            {
                inf.CatalogId = 4;
            }
            else if (Tipo == "Laboratorio")
            {
                inf.CatalogId = 6;
            }
            else if (Tipo == "Tienda")
            {
                inf.CatalogId = 8;
            }
            else if (Tipo == "Almacen")
            {
                inf.CatalogId = 3;

                List<CatalogDetail> Tiendas = new List<CatalogDetail>();
                Tiendas = (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda"
                           select
                          a
                     ).ToList();
                Tiendas.Insert(0, new CatalogDetail { Id = 0, Name = null });
                ViewBag.Tiendas = new SelectList(Tiendas, "ID", "Name");

                inf.Isdefault = false;
            }



            return View(inf);
        }

        // POST: CatalogDetails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,CatalogId,Name,Description,IsActive,Isdefault,ParentId,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,OptionalInformation")] CatalogDetail catalogDetail)
        {
            if (ModelState.IsValid)
            {
                var tipo = HttpContext.Request.Params.Get("Tipo");
                catalogDetail.CreatedBy = InformationIdentity.GetID();
                catalogDetail.CreatedOn = CustomValuesFormat.currentDateTimeUTC();
                catalogDetail.ModifiedBy = InformationIdentity.GetID();
                catalogDetail.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();

                if (tipo == "Almacen" && catalogDetail.Isdefault == false)
                {
                    catalogDetail.ParentId = null;
                }


                db.CatalogDetail.Add(catalogDetail);
                db.SaveChanges();
                if (tipo == "Almacen" && catalogDetail.Isdefault && catalogDetail.ParentId != null)
                {
                    SetNoDefaultStorage(catalogDetail.Id, catalogDetail.ParentId.Value);
                }


                if (tipo == "TipoVenta")
                {
                    tipo = "Index";
                }
                return RedirectToAction($"/{tipo}");
            }

            return View(catalogDetail);
        }

        // GET: CatalogDetails/Edit/5
        public ActionResult Edit(int? id, string Tipo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogDetail catalogDetail = db.CatalogDetail.Find(id);
            if (catalogDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title2 = Tipo;
            if (Tipo == "Almacen")
            {
                List<CatalogDetail> Tiendas = new List<CatalogDetail>();
                Tiendas = (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda"
                           select
                          a
                     ).ToList();
                Tiendas.Insert(0, new CatalogDetail { Id = 0, Name = null });
                ViewBag.Tiendas = new SelectList(Tiendas, "ID", "Name");
            }
            return View(catalogDetail);
        }

        // POST: CatalogDetails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,CatalogId,Name,Description,IsActive,Isdefault,ParentId,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,OptionalInformation")] CatalogDetail catalogDetail)
        {
            if (ModelState.IsValid)
            {
                catalogDetail.ModifiedBy = InformationIdentity.GetID();
                catalogDetail.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();

                var tipo = HttpContext.Request.Params.Get("Tipo");
                if (tipo == "Almacen" && catalogDetail.Isdefault == false)
                {
                    catalogDetail.ParentId = null;
                }


                db.Entry(catalogDetail).State = EntityState.Modified;
                db.SaveChanges();

                if (tipo == "Almacen" && catalogDetail.Isdefault && catalogDetail.ParentId != null)
                {
                    SetNoDefaultStorage(catalogDetail.Id, catalogDetail.ParentId.Value);
                }

                if (tipo == "TipoVenta")
                {
                    tipo = "Index";
                }
                return RedirectToAction($"/{tipo}");
            }
            return View(catalogDetail);
        }

        // GET: CatalogDetails/Delete/5
        public ActionResult Delete(int? id, string Tipo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogDetail catalogDetail = db.CatalogDetail.Find(id);
            if (catalogDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title2 = Tipo;
            return View(catalogDetail);
        }

        // POST: CatalogDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CatalogDetail catalogDetail = db.CatalogDetail.Find(id);

            db.CatalogDetail.Remove(catalogDetail);
            db.SaveChanges();

            var tipo = HttpContext.Request.Params.Get("Tipo");
            if (tipo == "TipoVenta")
            {
                tipo = "Index";
            }
            return RedirectToAction($"/{tipo}");
        }
        public JsonResult SetDefaultStorage(int id)
        {
            CatalogDetail catalogDetail = this.db.CatalogDetail.FirstOrDefault<CatalogDetail>((Expression<Func<CatalogDetail, bool>>)(f => f.Id == id));
            catalogDetail.Isdefault = true;
            catalogDetail.ModifiedBy = InformationIdentity.GetID();
            catalogDetail.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
            catalogDetail.IsActive = true;
            this.db.CatalogDetail.Where<CatalogDetail>((Expression<Func<CatalogDetail, bool>>)(w => w.CatalogId == 3 && w.Id != id)).ToList<CatalogDetail>().ForEach((Action<CatalogDetail>)(f => f.Isdefault = false));
            this.db.SaveChanges();
            return this.Json((object)new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public void SetNoDefaultStorage(int id, int tiendaId)
        {
            this.db.CatalogDetail.Where<CatalogDetail>((Expression<Func<CatalogDetail, bool>>)(w => w.CatalogId == 3 && w.Id != id && w.ParentId == (int?)tiendaId)).ToList<CatalogDetail>().ForEach((Action<CatalogDetail>)(f =>
            {
                f.Isdefault = false;
                f.ParentId = new int?();
            }));
            this.db.SaveChanges();
        }

        [AllowAnonymous]
        public JsonResult GetDataCatalogJson(string catalogName, bool OnlyActive = false) => this.Json((object)this.db.CatalogDetail.Where<CatalogDetail>((Expression<Func<CatalogDetail, bool>>)(w => !OnlyActive ? true : w.IsActive == true)).Join((IEnumerable<Catalog>)this.db.Catalog, (Expression<Func<CatalogDetail, int>>)(a => a.CatalogId), (Expression<Func<Catalog, int>>)(b => b.Id), (a, b) => new
        {
            a = a,
            b = b
        }).Where(data => data.b.Name == catalogName).Select(data => data.a).ToList<CatalogDetail>(), JsonRequestBehavior.AllowGet);
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
