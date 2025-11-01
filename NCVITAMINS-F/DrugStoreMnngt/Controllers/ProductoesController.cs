using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using CCustomization.CustomClasses;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class ProductoesController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: Productoes
        public ActionResult Index(string activos = "activos")
        {

            bool? Status = null;
            if (activos == "activos") { Status = true; }
            if (activos == "inactivos") { Status = false; }

            db.Database.CommandTimeout = 0;

            var listProduct = (from a in db.Producto.Where(w => Status == null ? true : w.Activo == Status)
                               join b in db.Existencia on a.ID equals b.IDProducto
                               join c in db.CatalogDetail on b.IDAlmacen equals c.Id
                               join d in db.CatalogDetail on a.TipoPrecentacionID equals d.Id
                               into tipoPresent
                               from tPresent in tipoPresent.DefaultIfEmpty()
                               group new { a, b, c, tPresent } by a.ID into g
                               select new Models.ProductoCustom
                               {
                                   ID = g.Key,
                                   Marca = g.FirstOrDefault().a.Marca,
                                   DescripcionEspecifica = g.FirstOrDefault().a.DescripcionEspecifica,
                                   DescripcionGeneral = g.FirstOrDefault().a.DescripcionGeneral,
                                   FechaVencimiento = g.FirstOrDefault().a.FechaVencimiento,
                                   TipoPrecentacionID = g.FirstOrDefault().a.TipoPrecentacionID,
                                   PrecioCompra = g.FirstOrDefault().a.PrecioCompra,
                                   PrecioVenta = g.FirstOrDefault().a.PrecioVenta,
                                   ProveedorID = g.FirstOrDefault().a.ProveedorID,
                                   CodigoBarra = g.FirstOrDefault().a.CodigoBarra,
                                   CantidadDisponible = g.Sum(s => s.b.Cantidad.Value),
                                   CantidadPrecentacion = g.FirstOrDefault().a.CantidadPrecentacion,
                                   TipoPresentacion = g.FirstOrDefault().tPresent.Name,
                                   TipoVentaID = g.FirstOrDefault().a.TipoVentaID,
                                   Almacen = g.FirstOrDefault().a.Almacen,
                                   Laboratorio = g.FirstOrDefault().a.Laboratorio,
                                   Activo = g.FirstOrDefault().a.Activo,
                                   IVA = g.FirstOrDefault().a.IVA,
                                   StockMinimo = g.FirstOrDefault().a.StockMinimo,
                                   CreatedOn = g.FirstOrDefault().a.CreatedOn,
                                   CreatedBy = g.FirstOrDefault().a.CreatedBy,
                                   ModifiedOn = g.FirstOrDefault().a.ModifiedOn,
                                   ModifiedBy = g.FirstOrDefault().a.ModifiedBy,
                                   CodigoProducto = g.FirstOrDefault().a.CodigoProducto,
                                   almacenes = g.Select(s => new Models.AlmacenDetalle { Almacen = s.c.Name, Cantidad = s.b.Cantidad })
                               }).ToList();



            var statusList = new List<StatusProd>()
            {
                new StatusProd{ key = "todos", name = "Todos" },
             new StatusProd{ key = "activos", name = "Activos" },
             new StatusProd{ key = "inactivos", name = "Inactivos" }
            };

            ViewBag.StatusList = new SelectList(statusList, "key", "name", activos);

            return View(listProduct);
        }

        public class StatusProd
        {
            public string key { get; set; }

            public string name { get; set; }
        }
        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            List<CatalogDetail> Marcas = new List<CatalogDetail>();
            List<CatalogDetail> Almacen = new List<CatalogDetail>();
            List<CatalogDetail> TipoPrecentacion = new List<CatalogDetail>();
            List<CatalogDetail> TipoVenta = new List<CatalogDetail>();
            List<CatalogDetail> Laborario = new List<CatalogDetail>();
            List<Proveedor> Prove = new List<Proveedor>();
            Marcas = (from a in db.CatalogDetail
                      join b in db.Catalog on a.CatalogId equals b.Id
                      where
                      b.Name == "Marcas" && a.IsActive == true && b.IsActive == true
                      select
                      a
                     ).ToList();

            Almacen = (from a in db.CatalogDetail
                       join b in db.Catalog on a.CatalogId equals b.Id
                       where
                       b.Name == "Almacen" && a.IsActive == true && b.IsActive == true
                       select
                       a
                   ).ToList();
            TipoPrecentacion = (from a in db.CatalogDetail
                                join b in db.Catalog on a.CatalogId equals b.Id
                                where
                                b.Name == "TipoPrecentacion" && a.IsActive == true && b.IsActive == true
                                select
                                a
                 ).ToList();
            TipoVenta = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "TipoVenta" && a.IsActive == true && b.IsActive == true
                         select
                         a
                 ).ToList();
            Laborario = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "Laborario" && a.IsActive == true && b.IsActive == true
                         select
                       a
                 ).ToList();
            Prove = (from a in db.Proveedor where a.Activo.Value select a).ToList();

            ViewBag.Marcas = new SelectList(Marcas, "ID", "Name");
            ViewBag.Almacen = new SelectList(Almacen, "ID", "Name");
            ViewBag.TipoPrecentacion = new SelectList(TipoPrecentacion, "ID", "Name");
            ViewBag.TipoVenta = new SelectList(TipoVenta, "ID", "Name");
            ViewBag.Laborario = new SelectList(Laborario, "ID", "Name");
            ViewBag.Prove = new SelectList(Prove, "ID", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Marca,DescripcionEspecifica,DescripcionGeneral,FechaVencimiento,TipoPrecentacionID,PrecioCompra,PrecioVenta,ProveedorID,CodigoBarra,CantidadDisponible,CantidadPrecentacion,TipoVentaID,Almacen,Laboratorio,Activo,IVA,StockMinimo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,CodigoProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.CreatedBy = InformationIdentity.GetID();
                producto.CreatedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                producto.ModifiedBy = InformationIdentity.GetID();
                producto.ModifiedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                producto.Activo = true;
                db.Producto.Add(producto);
                db.SaveChanges();

                var listExistencias = db.CatalogDetail
                                        .Join(db.Catalog.Where(w => w.Name == "Almacen"), cd => cd.CatalogId, c => c.Id, (cd, c) => cd)
                                        .Select(s =>
                                          new
                                          {
                                              IDProducto = producto.ID,
                                              Cantidad = 0,
                                              IDAlmacen = s.Id,
                                              CreatedBy = producto.CreatedBy,
                                              CreatetedOn = producto.CreatedOn
                                          }
                                        ).ToList();

                var listExistencia2 = listExistencias.Select(s =>
                                         new Existencia
                                         {
                                             IDProducto = s.IDProducto,
                                             Cantidad = s.Cantidad,
                                             IDAlmacen = s.IDAlmacen,
                                             CreatedBy = s.CreatedBy,
                                             CreatetedOn = s.CreatetedOn
                                         }).ToList();

                db.Existencia.AddRange(listExistencia2);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(producto);
        }

        public ActionResult CreateV2()
        {
            List<CatalogDetail> Marcas = new List<CatalogDetail>();
            List<CatalogDetail> Almacen = new List<CatalogDetail>();
            List<CatalogDetail> TipoPrecentacion = new List<CatalogDetail>();
            List<CatalogDetail> TipoVenta = new List<CatalogDetail>();
            List<CatalogDetail> Laborario = new List<CatalogDetail>();
            List<Proveedor> Prove = new List<Proveedor>();
            Marcas = (from a in db.CatalogDetail
                      join b in db.Catalog on a.CatalogId equals b.Id
                      where
                      b.Name == "Marcas"
                      select
                      a
                     ).ToList();

            Almacen = (from a in db.CatalogDetail
                       join b in db.Catalog on a.CatalogId equals b.Id
                       where
                       b.Name == "Almacen"
                       select
                       a
                   ).ToList();
            TipoPrecentacion = (from a in db.CatalogDetail
                                join b in db.Catalog on a.CatalogId equals b.Id
                                where
                                b.Name == "TipoPrecentacion"
                                select
                                a
                 ).ToList();
            TipoVenta = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "TipoVenta"
                         select
                         a
                 ).ToList();
            Laborario = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "Laborario"
                         select
                       a
                 ).ToList();
            Prove = (from a in db.Proveedor where a.Activo.Value select a).ToList();

            ViewBag.Marcas = new SelectList(Marcas, "ID", "Name");
            ViewBag.Almacen = new SelectList(Almacen, "ID", "Name");
            ViewBag.TipoPrecentacion = new SelectList(TipoPrecentacion, "ID", "Name");
            ViewBag.TipoVenta = new SelectList(TipoVenta, "ID", "Name");
            ViewBag.Laborario = new SelectList(Laborario, "ID", "Name");
            ViewBag.Prove = new SelectList(Prove, "ID", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateV2([Bind(Include = "ID,Marca,DescripcionEspecifica,DescripcionGeneral,FechaVencimiento,TipoPrecentacionID,PrecioCompra,PrecioVenta,ProveedorID,CodigoBarra,CantidadDisponible,CantidadPrecentacion,TipoVentaID,Almacen,Laboratorio,Activo,IVA,StockMinimo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,CodigoProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.CreatedBy = InformationIdentity.GetID();
                producto.CreatedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                producto.ModifiedBy = InformationIdentity.GetID();
                producto.ModifiedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }


        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            List<CatalogDetail> Marcas = new List<CatalogDetail>();
            List<CatalogDetail> Almacen = new List<CatalogDetail>();
            List<CatalogDetail> TipoPrecentacion = new List<CatalogDetail>();
            List<CatalogDetail> TipoVenta = new List<CatalogDetail>();
            List<CatalogDetail> Laborario = new List<CatalogDetail>();
            List<Proveedor> Prove = new List<Proveedor>();
            Marcas = (from a in db.CatalogDetail
                      join b in db.Catalog on a.CatalogId equals b.Id
                      where
                      b.Name == "Marcas"
                      select
                      a
                     ).ToList();

            Almacen = (from a in db.CatalogDetail
                       join b in db.Catalog on a.CatalogId equals b.Id
                       where
                       b.Name == "Almacen"
                       select
                       a
                   ).ToList();
            TipoPrecentacion = (from a in db.CatalogDetail
                                join b in db.Catalog on a.CatalogId equals b.Id
                                where
                                b.Name == "TipoPrecentacion"
                                select
                                a
                 ).ToList();
            TipoVenta = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "TipoVenta"
                         select
                         a
                 ).ToList();
            Laborario = (from a in db.CatalogDetail
                         join b in db.Catalog on a.CatalogId equals b.Id
                         where
                         b.Name == "Laborario"
                         select
                       a
                 ).ToList();
            Prove = (from a in db.Proveedor where a.Activo.Value select a).ToList();

            var cantidadDisponible = db.Existencia.Where(w => w.IDProducto == id).Sum(s => s.Cantidad);
            producto.CantidadDisponible = cantidadDisponible;

            ViewBag.Marcas = new SelectList(Marcas, "ID", "Name");
            ViewBag.Almacen = new SelectList(Almacen, "ID", "Name");
            ViewBag.TipoPrecentacion = new SelectList(TipoPrecentacion, "ID", "Name");
            ViewBag.TipoVenta = new SelectList(TipoVenta, "ID", "Name");
            ViewBag.Laborario = new SelectList(Laborario, "ID", "Name");
            ViewBag.Prove = new SelectList(Prove, "ID", "Nombre");
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Marca,DescripcionEspecifica,DescripcionGeneral,FechaVencimiento,TipoPrecentacionID,PrecioCompra,PrecioVenta,ProveedorID,CodigoBarra,CantidadDisponible,CantidadPrecentacion,TipoVentaID,Almacen,Laboratorio,Activo,IVA,StockMinimo,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,CodigoProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.ModifiedBy = InformationIdentity.GetID();
                producto.ModifiedOn = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Active(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            producto.Activo = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Active")]

        public ActionResult ActivedConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            producto.Activo = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ConsultarProducto()
        {
            //ViewBag.Lista = db.Producto.Where(w => w.CantidadDisponible > 0).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ConsultarProducto(FilterReport filter)
        {
            List<Producto> list = new List<Producto>();
            filter.filterString = filter.filterString?.Trim();
            filter.filterStringS = filter.filterStringS?.Trim();
            filter.filterStringT = filter.filterStringT?.Trim();
            filter.filterStringF = filter.filterStringF?.Trim();

            var ProductList = db.Producto
                .Where(w =>
                (String.IsNullOrEmpty(filter.filterString) ? true : filter.filterString == w.CodigoProducto) &&
                (String.IsNullOrEmpty(filter.filterStringS) ? true : w.DescripcionEspecifica.Contains(filter.filterStringS)) &&
                (String.IsNullOrEmpty(filter.filterStringT) ? true : w.DescripcionGeneral.Contains(filter.filterStringT)) &&
                (String.IsNullOrEmpty(filter.filterStringF) ? true : w.Marca.Contains(filter.filterStringF))
                ).AsQueryable();


            var listProduct = (from a in ProductList
                               join b in db.Existencia on a.ID equals b.IDProducto
                               group new { a, b } by a.ID into g
                               select new Models.ProductoCustom
                               {
                                   ID = g.Key,
                                   Marca = g.FirstOrDefault().a.Marca,
                                   DescripcionEspecifica = g.FirstOrDefault().a.DescripcionEspecifica,
                                   DescripcionGeneral = g.FirstOrDefault().a.DescripcionGeneral,
                                   FechaVencimiento = g.FirstOrDefault().a.FechaVencimiento,
                                   TipoPrecentacionID = g.FirstOrDefault().a.TipoPrecentacionID,
                                   PrecioCompra = g.FirstOrDefault().a.PrecioCompra,
                                   PrecioVenta = g.FirstOrDefault().a.PrecioVenta,
                                   ProveedorID = g.FirstOrDefault().a.ProveedorID,
                                   CodigoBarra = g.FirstOrDefault().a.CodigoBarra,
                                   CantidadDisponible = g.Sum(s => s.b.Cantidad.Value),
                                   CantidadPrecentacion = g.FirstOrDefault().a.CantidadPrecentacion,
                                   TipoVentaID = g.FirstOrDefault().a.TipoVentaID,
                                   Almacen = g.FirstOrDefault().a.Almacen,
                                   Laboratorio = g.FirstOrDefault().a.Laboratorio,
                                   Activo = g.FirstOrDefault().a.Activo,
                                   IVA = g.FirstOrDefault().a.IVA,
                                   StockMinimo = g.FirstOrDefault().a.StockMinimo,
                                   CreatedOn = g.FirstOrDefault().a.CreatedOn,
                                   CreatedBy = g.FirstOrDefault().a.CreatedBy,
                                   ModifiedOn = g.FirstOrDefault().a.ModifiedOn,
                                   ModifiedBy = g.FirstOrDefault().a.ModifiedBy,
                                   CodigoProducto = g.FirstOrDefault().a.CodigoProducto


                               }).Where(w => w.CantidadDisponible > 0)
                 .ToList();

            ViewBag.Lista = listProduct;
            return View();
        }

        public ActionResult TrasladoProducto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TrasladoProducto(DrugStoreMnngt.Models.TrasladoCustomCustom info)
        {
            if (info.TrasladoDetalle.Count() > 0)
            {
                var now = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();


                var ExistenciaList = new List<Existencia>();
                foreach (var trasladoDet in info.TrasladoDetalle)
                {

                    var existenciaOrigen = db.Existencia
                                            .FirstOrDefault(w =>
                                                w.IDProducto == trasladoDet.ProductoId &&
                                                w.IDAlmacen == trasladoDet.AlmacenOrigen
                                            );

                    var existenciaDestino = db.Existencia
                                            .FirstOrDefault(w =>
                                                w.IDProducto == trasladoDet.ProductoId &&
                                                w.IDAlmacen == trasladoDet.AlmancenDestino
                                            );


                    if (existenciaDestino == null)
                    {
                        existenciaDestino = new Existencia
                        {
                            IDAlmacen = trasladoDet.AlmancenDestino,
                            Cantidad = 0,
                            IDProducto = trasladoDet.ProductoId,
                            CreatetedOn = now,
                            CreatedBy = InformationIdentity.GetID()
                        };

                        db.Existencia.Add(existenciaDestino);
                        db.SaveChanges();
                    }

                    trasladoDet.ExistenciaOrigen = existenciaOrigen.ID;
                    trasladoDet.ExistenciaDestino = existenciaDestino.ID;

                    existenciaOrigen.Cantidad -= trasladoDet.Cantidad;
                    existenciaDestino.Cantidad += trasladoDet.Cantidad;

                    ExistenciaList.Add(existenciaOrigen);
                    ExistenciaList.Add(existenciaDestino);


                }

                List<MovimientoExitencia> lista = new List<MovimientoExitencia>();


                var tipoTraslado = db.CatalogDetail.FirstOrDefault(f => f.CatalogId == 9 && f.Name == "Traslado");
                foreach (var item in info.TrasladoDetalle)
                {

                    MovimientoExitencia MovimientoExitenciaItem = new MovimientoExitencia();
                    MovimientoExitenciaItem.Comentario = item.Comentario;
                    MovimientoExitenciaItem.IDExistencia = item.ExistenciaOrigen;
                    MovimientoExitenciaItem.IDExistenciaTarget = item.ExistenciaDestino;
                    MovimientoExitenciaItem.IDTipoMovimiento = tipoTraslado.Id;
                    MovimientoExitenciaItem.Cantidad = item.Cantidad;
                    MovimientoExitenciaItem.CreatetedOn = now;
                    MovimientoExitenciaItem.CreatedBy = InformationIdentity.GetID();

                    lista.Add(MovimientoExitenciaItem);


                }
                db.MovimientoExitencia.AddRange(lista);

                db.SaveChanges();

            }

            return RedirectToAction("DetalleMovimientosTraslado");
        }

        public ActionResult DetalleMovimientosTraslado(DateTime? FilterDate)
        {


            var conciliacionAbierta = db.Inventario.Any(a => a.Cerrado == null);

            ViewBag.ConciliacionAbierta = conciliacionAbierta;

            FilterDate = FilterDate ?? DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
            List<Models.TransladoDetalle> listmovs = (
                                                     from prod in db.Producto
                                                     join exis in db.Existencia on prod.ID equals exis.IDProducto
                                                     join almacen in db.CatalogDetail on exis.IDAlmacen equals almacen.Id
                                                     from movs in db.MovimientoExitencia
                                                     join tipo in db.CatalogDetail on movs.IDTipoMovimiento equals tipo.Id
                                                     where
                                                         DbFunctions.TruncateTime(movs.CreatetedOn.Value) == FilterDate &&
                                                         (exis.ID == movs.IDExistencia || exis.ID == movs.IDExistenciaTarget)
                                                     group new { prod, exis, almacen, movs, tipo } by movs.ID into g
                                                     select new Models.TransladoDetalle
                                                     {
                                                         Fecha = g.FirstOrDefault().movs.CreatetedOn ?? DateTime.Now,
                                                         CodigoProducto = g.FirstOrDefault().prod.CodigoProducto,
                                                         ProductoNombre = g.FirstOrDefault().prod.DescripcionEspecifica,
                                                         AlmacenOrigenNombre = g.FirstOrDefault(f => f.movs.IDExistencia == f.exis.ID).almacen.Name,
                                                         AlmacenDestinoNombre = g.FirstOrDefault(f => f.movs.IDExistenciaTarget == f.exis.ID).almacen.Name,
                                                         Comentario = g.FirstOrDefault().movs.Comentario,
                                                         Cantidad = g.FirstOrDefault().movs.Cantidad ?? 0,
                                                         TipoMov = g.FirstOrDefault().tipo.Name


                                                     }).ToList();


            ViewBag.FilterDate = FilterDate.Value;
            return View(listmovs);


        }
        public ActionResult agregarRequiza()
        {
            return View();
        }


        [HttpPost]
        public ActionResult agregarRequiza(DrugStoreMnngt.Models.RequizaCustom info)
        {
            if (info.requizaDetalle.Count() > 0)
            {
                RequizaCompra requizaCompra = new RequizaCompra();

                requizaCompra.Proveedor = info.requiza.IDProveedor;
                requizaCompra.FechaCreacion = DrugStoreMnngt.Models.CustomValuesFormat.currentDateTimeUTC();
                requizaCompra.Usuario = InformationIdentity.GetID();
                requizaCompra.AlmacenId = info.requiza.AlmacenId;
                db.RequizaCompra.Add(requizaCompra);

                db.SaveChanges();


                List<RequizaCompraDetalle> lista = new List<RequizaCompraDetalle>();

                var tipoMov = db.CatalogDetail.FirstOrDefault(f => f.CatalogId == 9 && f.Name.ToLower() == "compra");

                RequizaCompraDetalle item;
                foreach (DrugStoreMnngt.Models.RequizaDetalleCustom req in info.requizaDetalle)
                {
                    item = new RequizaCompraDetalle();
                    var existencia = db.Existencia.FirstOrDefault(w => w.IDProducto == req.IDProducto && w.IDAlmacen == requizaCompra.AlmacenId);

                    if (existencia == null)
                    {
                        existencia = new Existencia
                        {
                            IDAlmacen = requizaCompra.AlmacenId,
                            Cantidad = req.Cantidad,
                            IDProducto = req.IDProducto,
                            CreatetedOn = requizaCompra.FechaCreacion,
                            CreatedBy = InformationIdentity.GetID()
                        };

                        db.Existencia.Add(existencia);
                        db.SaveChanges();
                    }
                    else
                    {
                        existencia.Cantidad += req.Cantidad;
                    }
                    //(from a in db.Producto where a.ID == req.IDProducto select a).FirstOrDefault();

                    item.IDProducto = req.IDProducto;
                    item.IDRequizaCompra = requizaCompra.ID;
                    item.Cantidad = req.Cantidad;
                    item.FechaCreacion = DateTime.Now;

                    db.RequizaCompraDetalle.Add(item);
                    //producto.CantidadDisponible += req.Cantidad;

                    MovimientoExitencia mov = new MovimientoExitencia();
                    mov.Cantidad = (int)req.Cantidad;
                    mov.Comentario = "Movimiento de compra";
                    mov.IDExistenciaTarget = existencia.ID;
                    mov.IDTipoMovimiento = tipoMov.Id;
                    mov.CreatetedOn = requizaCompra.FechaCreacion;
                    mov.CreatedBy = InformationIdentity.GetID();
                    db.MovimientoExitencia.Add(mov);

                    db.SaveChanges();
                }

            }

            return RedirectToAction("detallesRequiza");
        }


        public ActionResult detallesRequiza()
        {

            var conciliacionAbierta = db.Inventario.Any(a => a.Cerrado == null);

            ViewBag.ConciliacionAbierta = conciliacionAbierta;

            List<DrugStoreMnngt.Models.DeatellesRequizaCustom> lista = (from a in db.RequizaCompra
                                                                        join b in db.Proveedor on a.Proveedor equals b.ID
                                                                        join c in db.Usuarios on a.Usuario equals c.Id
                                                                        join d in db.CatalogDetail on a.AlmacenId equals d.Id into alamcen
                                                                        from e in alamcen.DefaultIfEmpty()
                                                                        select new DrugStoreMnngt.Models.DeatellesRequizaCustom
                                                                        {
                                                                            ID = a.ID,
                                                                            nombreProveedor = b.Nombre,
                                                                            fecha = a.FechaCreacion,
                                                                            cantidadProducto = (from zz in db.RequizaCompraDetalle where zz.IDRequizaCompra == a.ID select zz).Sum(ss => ss.Cantidad) ?? 0,
                                                                            Usuario = c.Usuario,
                                                                            nombreAlmacen = e.Name
                                                                        }).ToList();

            return View(lista);
        }

        public ActionResult itemRequiza(int ID)
        {
            List<DrugStoreMnngt.Models.DeatellesRequizaItemsCustom> lista = (from a in db.RequizaCompraDetalle.Where(zz => zz.IDRequizaCompra == ID)
                                                                             join b in db.Producto on a.IDProducto equals b.ID
                                                                             join c in db.CatalogDetail on b.Laboratorio equals c.Id into lab
                                                                             from ljoin in lab.DefaultIfEmpty()
                                                                             select new DrugStoreMnngt.Models.DeatellesRequizaItemsCustom
                                                                             {
                                                                                 nombreProducto = b.DescripcionEspecifica,
                                                                                 cantidad = a.Cantidad ?? 0,
                                                                                 laboratorio = ljoin.Name

                                                                             }).ToList();

            return View(lista);
        }

        public JsonResult GetProductosJson()
        {
            var productos = (from a in db.Producto
                             where a.Activo == true
                             select
                             a

                         ).ToList();
            var proveedorx = (from a in db.Proveedor
                              select
                              a
                      ).ToList();

            var storages = db.CatalogDetail.Where(w => w.CatalogId == 3).ToList();





            var ObjJson = new { proveedor = proveedorx, productos = productos, almacenes = storages };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAlmacenOrigenJson()
        {

            var storages = db.CatalogDetail.Where(w => w.CatalogId == 3).ToList();

            var ObjJson = new { almacenes = storages };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAlmacenProductoDisponible(int AlmacenId)
        {

            var almacenesdestino = db.CatalogDetail.Where(w => w.CatalogId == 3 && w.Id != AlmacenId);

            var listProduct = (from a in db.Producto.Where(w => w.Activo == true)
                               join b in db.Existencia on a.ID equals b.IDProducto
                               where b.IDAlmacen == AlmacenId
                               group new { a, b } by a.ID into g
                               select new Models.ProductoCustom
                               {
                                   ID = g.Key,
                                   Marca = g.FirstOrDefault().a.Marca,
                                   DescripcionEspecifica = g.FirstOrDefault().a.DescripcionEspecifica,
                                   DescripcionGeneral = g.FirstOrDefault().a.DescripcionGeneral,
                                   FechaVencimiento = g.FirstOrDefault().a.FechaVencimiento,
                                   TipoPrecentacionID = g.FirstOrDefault().a.TipoPrecentacionID,
                                   PrecioCompra = g.FirstOrDefault().a.PrecioCompra,
                                   PrecioVenta = g.FirstOrDefault().a.PrecioVenta,
                                   ProveedorID = g.FirstOrDefault().a.ProveedorID,
                                   CodigoBarra = g.FirstOrDefault().a.CodigoBarra,
                                   CantidadDisponible = g.Sum(s => s.b.Cantidad.Value),
                                   CantidadPrecentacion = g.FirstOrDefault().a.CantidadPrecentacion,
                                   TipoVentaID = g.FirstOrDefault().a.TipoVentaID,
                                   Almacen = g.FirstOrDefault().a.Almacen,
                                   Laboratorio = g.FirstOrDefault().a.Laboratorio,
                                   Activo = g.FirstOrDefault().a.Activo,
                                   IVA = g.FirstOrDefault().a.IVA,
                                   StockMinimo = g.FirstOrDefault().a.StockMinimo,
                                   CreatedOn = g.FirstOrDefault().a.CreatedOn,
                                   CreatedBy = g.FirstOrDefault().a.CreatedBy,
                                   ModifiedOn = g.FirstOrDefault().a.ModifiedOn,
                                   ModifiedBy = g.FirstOrDefault().a.ModifiedBy,
                                   CodigoProducto = g.FirstOrDefault().a.CodigoProducto
                               })
                .ToList();


            var ObjJson = new { almacenesdestino = almacenesdestino, productos = listProduct };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);

        }

        public ActionResult HistoricoTransaccionesProducto(int Producto)
        {
            var producto = (from a in db.Producto select a).FirstOrDefault(z => z.ID == Producto);
            ViewBag.ProductoInfo = producto;
            FilterReport filter = new FilterReport();
            filter.producto = producto.ID;
            filter.fromDate = DateTime.Now;
            return View(filter);
        }

        [HttpPost]
        public ActionResult HistoricoTransaccionesProducto(FilterReport filter)
        {
            var producto = (from a in db.Producto select a).FirstOrDefault(z => z.ID == filter.producto);
            ViewBag.ProductoInfo = producto;
            var lista = (from a in db.getProductHistory(filter.fromDate, filter.producto)
                         select new DrugStoreMnngt.Models.ProductHistory
                         {
                             Cantidad = a.Cantidad,
                             Comentario = a.Comentario,
                             Creado = a.Creado,
                             Desde = a.Desde,
                             Fecha = a.Fecha,
                             Para = a.Para
                         }).ToList();
            ViewBag.ListaDetalle = lista;
            return View();
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
