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
    public class InventarioController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var inventarios = (from inv in db.Inventario
                               join usercr in db.Usuarios on inv.CreatedBy equals usercr.Id
                               join usermod in db.Usuarios on inv.ModifiedBy equals usermod.Id
                               select new InventarioCustom
                               {
                                   Id = inv.Id,
                                   modifiedByName = usermod.Usuario,
                                   createdByName = usercr.Usuario,
                                   CreatedOn = inv.CreatedOn,
                                   ModifiedOn = inv.ModifiedOn,
                                   Cerrado = inv.Cerrado
                               }).ToList();
            return View(inventarios);
        }

        public JsonResult GetAlmacenJson()
        {
            var storages = db.CatalogDetail.Where(w => w.CatalogId == 3).ToList();

            var ObjJson = new { almacenes = storages };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductosJson(int almacenId, int? concilationId)
        {
            var productos = (from prod in db.Producto
                             join exist in db.Existencia.Where(w => w.IDAlmacen == almacenId) on prod.ID equals exist.IDProducto
                             into prodExist
                             from pExist in prodExist.DefaultIfEmpty()
                             join almacen in db.CatalogDetail on pExist.IDAlmacen equals almacen.Id
                             join invprod in db.InventarioProducto.Where(w => w.InventarioId == concilationId) on prod.ID equals invprod.ProductoId
                             into InvProd //from invProd in InvProd.DefaultIfEmpty()
                             where prod.Activo == true && almacen.Id == almacenId && !InvProd.Any()
                             select new InventarioDetalleCustom
                             {
                                 ProductoId = prod.ID,
                                 CodigoProducto = prod.CodigoProducto,
                                 NombreProducto = prod.DescripcionEspecifica,
                                 ExistenciaEnSistema = prodExist.Any() ? (int)(prodExist.FirstOrDefault().Cantidad ?? 0) : 0,
                                 AlmacenId = almacen.Id,
                                 NombreAlmacen = almacen.Name
                             }
                         ).ToList();

            var ObjJson = new { productos = productos };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Cerrar(int conciliacionId, bool cerrar)
        {
            var inventario = db.Inventario.FirstOrDefault(f => f.Id == conciliacionId);
            var user = InformationIdentity.GetID();
            var now = DateTime.Now;
            inventario.Cerrado = cerrar;
            inventario.ModifiedBy = user;
            inventario.ModifiedOn = now;

            if (!cerrar)
            {
                db.SaveChanges();
                return Redirect("index");
            }

            var productos = db.InventarioProducto.Where(w => w.InventarioId == conciliacionId).ToList();

            var productosIds = productos.Select(s => s.ProductoId.ToString() + '_' + s.AlmacenId.ToString()).ToList();

            var existencias = db.Existencia.Where(w => productosIds.Contains(w.IDProducto.Value.ToString() + "_" + w.IDAlmacen.ToString())).ToList();

            var movimientos = new List<Models.BD.MovimientoExitencia>();

            var tipoMov = db.CatalogDetail.FirstOrDefault(f => f.CatalogId == 9 && f.Name.ToLower() == "conciliacion");

            productos.ForEach(f =>
            {
                var exist = existencias.FirstOrDefault(frst => frst.IDProducto.Value == f.ProductoId && frst.IDAlmacen.Value == f.AlmacenId);

                if (exist != null)
                {
                    var cantidadMov = f.CantidadFisica - f.ExistenciaEnSistema;
                    exist.Cantidad = f.CantidadFisica;

                    var mov = new Models.BD.MovimientoExitencia();
                    mov.Cantidad = cantidadMov;
                    mov.Comentario = "Moviento de ajuste";
                    mov.CreatedBy = user;
                    mov.CreatetedOn = now;
                    mov.IDExistencia = exist.ID;
                    mov.IDExistenciaTarget = exist.ID;
                    mov.IDTipoMovimiento = tipoMov.Id;
                    mov.InventarioProductoId = f.Id;

                    movimientos.Add(mov);

                }

            });

            db.MovimientoExitencia.AddRange(movimientos);

            db.SaveChanges();

            return Redirect("index");
        }

        public ActionResult Conciliacion(int? conciliacionId)
        {

            var inventario = new InventarioCustom();
            if (conciliacionId != null)
            {
                var inventarioDB = db.Inventario.Find(conciliacionId);

                inventario.Cerrado = inventarioDB.Cerrado;
                inventario.CreatedBy = inventarioDB.CreatedBy;
                inventario.CreatedOn = inventarioDB.CreatedOn;
                inventario.Id = inventarioDB.Id;
                inventario.ModifiedBy = inventarioDB.ModifiedBy;
                inventario.ModifiedOn = inventarioDB.ModifiedOn;

                inventario.productos = db.InventarioProducto.Where(w => w.InventarioId == conciliacionId)
                    .Join(db.Producto,
                        invprod => invprod.ProductoId,
                        prod => prod.ID,
                        (invp, prod) => new { invp, prod }
                        )
                    .Join(db.CatalogDetail, invp => invp.invp.AlmacenId, almacen => almacen.Id, (invp, almacen) => new { invp.invp, invp.prod, almacen })
                    .Select(s =>
                    new InventarioDetalleCustom
                    {
                        Id = s.invp.Id,
                        ProductoId = s.invp.ProductoId,
                        ModifiedOn = s.invp.ModifiedOn,
                        CantidadFisica = s.invp.CantidadFisica,
                        CreatedBy = s.invp.CreatedBy,
                        CreatedOn = s.invp.CreatedOn,
                        ExistenciaEnSistema = s.invp.ExistenciaEnSistema,
                        InventarioId = s.invp.InventarioId,
                        ModifiedBy = s.invp.ModifiedBy,
                        CodigoProducto = s.prod.CodigoProducto,
                        NombreProducto = s.prod.DescripcionEspecifica,
                        DescripcionProducto = s.prod.DescripcionGeneral,
                        AlmacenId = s.almacen.Id,
                        NombreAlmacen = s.almacen.Name
                    }).ToList();
            }
            //else
            //{
            //    inventario.productos = db.Producto
            //                           .GroupJoin(
            //                                db.Existencia,
            //                                prod => prod.ID,
            //                                Exis => Exis.IDProducto,
            //                                (prod, exist) => new
            //                                {
            //                                    ProductoId = prod.ID,
            //                                    Codigo = prod.CodigoProducto,
            //                                    NombreProducto = prod.DescripcionEspecifica,
            //                                    Descripcion = prod.DescripcionGeneral,
            //                                    ExistenciaActual = (exist.FirstOrDefault().Cantidad) ?? 0
            //                                }).Select(s => new InventarioDetalleCustom
            //                                {
            //                                    ProductoId = s.ProductoId,
            //                                    CantidadFisica = 0,
            //                                    ExistenciaEnSistema = (int)(s.ExistenciaActual),
            //                                    CodigoProducto = s.Codigo,
            //                                    NombreProducto = s.NombreProducto,
            //                                    DescripcionProducto = s.Descripcion
            //                                }).ToList();
            //}

            return View(inventario);

        }
        [HttpPost]
        public ActionResult Conciliacion(InventarioCustom inventario)
        {

            var user = InformationIdentity.GetID();
            var now = DateTime.Now;
            if (inventario.Id != 0)
            {
                var inventarioDB = db.Inventario.Find(inventario.Id);

                inventarioDB.ModifiedBy = user;
                inventarioDB.ModifiedOn = now;

                List<InventarioProducto> productos = inventario.productos.Where(w => w.Id == 0).Select(s =>
                        new InventarioProducto
                        {
                            CantidadFisica = s.CantidadFisica,
                            CreatedBy = user,
                            CreatedOn = now,
                            ExistenciaEnSistema = s.ExistenciaEnSistema,
                            ModifiedBy = user,
                            ModifiedOn = now,
                            InventarioId = inventario.Id,
                            ProductoId = s.ProductoId,
                            AlmacenId = s.AlmacenId

                        }).ToList();

                var detArrayIds = inventario.productos.Where(w => w.Id > 0).Select(s => s.Id).ToArray();

                var InventarioProductosDb = db.InventarioProducto.Where(w => detArrayIds.Contains(w.Id)).ToList();

                inventario.productos.Where(w => w.Id > 0).ToList().ForEach(f =>
                {
                    var invProdDb = InventarioProductosDb.FirstOrDefault(first => first.Id == f.Id);
                    invProdDb.CantidadFisica = f.CantidadFisica;
                    invProdDb.ExistenciaEnSistema = f.ExistenciaEnSistema;
                    invProdDb.ModifiedBy = user;
                    invProdDb.ModifiedOn = now;
                });

                db.InventarioProducto.AddRange(productos);

                db.SaveChanges();
            }
            else
            {
                var InventarioDb = new Inventario()
                {
                    Cerrado = null,
                    CreatedBy = user,
                    CreatedOn = now,
                    ModifiedBy = user,
                    ModifiedOn = now
                };

                db.Inventario.Add(InventarioDb);
                db.SaveChanges();


                List<InventarioProducto> productos = inventario.productos.Select(s =>
                new InventarioProducto
                {
                    CantidadFisica = s.CantidadFisica,
                    CreatedBy = user,
                    CreatedOn = now,
                    ExistenciaEnSistema = s.ExistenciaEnSistema,
                    ModifiedBy = user,
                    ModifiedOn = now,
                    InventarioId = InventarioDb.Id,
                    ProductoId = s.ProductoId,
                    AlmacenId = s.AlmacenId

                }).ToList();

                db.InventarioProducto.AddRange(productos);

                db.SaveChanges();



                inventario.productos = db.Producto
                                       .GroupJoin(
                                            db.Existencia,
                                            prod => prod.ID,
                                            Exis => Exis.IDProducto,
                                            (prod, exist) => new
                                            {
                                                ProductoId = prod.ID,
                                                Codigo = prod.CodigoProducto,
                                                NombreProducto = prod.DescripcionEspecifica,
                                                Descripcion = prod.DescripcionGeneral,
                                                ExistenciaActual = (exist.FirstOrDefault().Cantidad) ?? 0
                                            }).Select(s => new InventarioDetalleCustom
                                            {
                                                ProductoId = s.ProductoId,
                                                CantidadFisica = 0,
                                                ExistenciaEnSistema = (int)(s.ExistenciaActual),
                                                CodigoProducto = s.Codigo,
                                                NombreProducto = s.NombreProducto,
                                                DescripcionProducto = s.Descripcion
                                            }).ToList();
            }

            return Redirect("index");

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
