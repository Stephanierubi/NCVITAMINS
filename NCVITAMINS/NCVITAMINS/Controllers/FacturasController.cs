using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using DrugStoreMnngt.Models;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    [DataContract(IsReference = true)]
    public class FacturasController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        public FacturasController()
        {

            db.Configuration.LazyLoadingEnabled = false;
        }
        // GET: Facturas
        public ActionResult Index()
        {

            var conciliacionAbierta = db.Inventario.Any(a => a.Cerrado == null);

            ViewBag.ConciliacionAbierta = conciliacionAbierta;

            DateTime dates = CustomValuesFormat.currentDateTimeUTC().Date;

            List<FacturaCustom> returnInf = (from a in db.Factura.Where(z => z.Fecha.Value >= dates && (z.Cancelada == false && z.Revertida == false))
                                             select new FacturaCustom
                                             {
                                                 ID = a.ID,
                                                 Fecha = a.Fecha,
                                                 Tienda = a.Tienda,
                                                 ClientID = a.ClientID,
                                                 FormaPago = a.FormaPago,
                                                 Neto = db.FacturaDetalle.Where(z => z.FacturaID == a.ID).Select(x => x.Cantidad * x.Valor).Sum() ?? 0
                                             }).ToList();

          int usuarioLogeado=  InformationIdentity.GetID();

            var usr = db.Usuarios.Where(z => z.Id == usuarioLogeado).FirstOrDefault();

            var formaPago = (from a in db.CatalogDetail
                             join b in db.Catalog on a.CatalogId equals b.Id
                             where
                             b.Name == "FormaPago"
                             select
                             a
            ).ToList();

            var tiendas = (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda"
                           select
                           a
            ).ToList();

            var clientes = (from a in db.Cliente select a).ToList();

            returnInf.ForEach(f =>
            {
                var tienda = tiendas.FirstOrDefault(w => w.Id == f.Tienda);
                if (tienda != null)
                {
                    f.NombreTienda = tienda.Name;
                }

                if (f.FormaPago != null)
                {
                    var fPago = formaPago.FirstOrDefault(w => w.Id == f.FormaPago);
                    f.FormaPagoText = fPago.Name;
                }

                var responsable = clientes.FirstOrDefault(w => w.ID == f.ClientID);
                if (responsable != null)
                {
                    f.NombreResponsable = $"{responsable.Nombres ?? ""} {responsable.Apellidos ?? ""}";
                }
            });

            return View(returnInf);
        }

        // GET: Facturas/Details/5
        public ActionResult Imprimir(int? id)
        {
            int i = 0;
            int idUsuario = InformationIdentity.GetID();
            var usuario = db.Usuarios.Where(z => z.Id == idUsuario).FirstOrDefault();
            DateTime fecha = CustomValuesFormat.currentDateTimeUTC();         
            ViewBag.ResponsableNombre = usuario.Usuario;            
            ViewBag.NoFactura = id;

            String nombreCompania = db.COMPANIAs.First(z=>z.ID == usuario.SUCURSAL).NOMBRE;

            ViewBag.NombreCompania= nombreCompania;

            Factura fac = db.Factura.First(z => z.ID == id);

            ViewBag.FechaConsulta = fac.Fecha?.ToString("dd/MM/yyyy");

            List<FacturaDetalleCustom> factura = (from a in db.FacturaDetalle.Where(z => z.FacturaID == id)
                                                  join b in db.Producto on a.ProductoID equals b.ID
                                                  
                                                  select new FacturaDetalleCustom
                                                  {
                                                      ID= i + 1,
                                                      ProductoNombre = b.DescripcionEspecifica,
                                                      Cantidad = a.Cantidad,
                                                      Valor = a.Valor,
                                                      Devolucion = a.Devolucion,
                                                      total= a.Cantidad*a.Valor
                                                  }).ToList();

            FacturaDetalleCustom fcustom = new FacturaDetalleCustom();

            fcustom.total = factura.Sum(z => z.total);
            fcustom.ProductoNombre = "Total";

            factura.Add(fcustom);
            if (factura == null)
            {
                return HttpNotFound();
            }
            

            return View(factura);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<FacturaDetalleCustom> factura = (from a in db.FacturaDetalle.Where(z => z.FacturaID == id)
                                                  join b in db.Producto on a.ProductoID equals b.ID
                                                  select new FacturaDetalleCustom
                                                  {
                                                      ProductoNombre = b.DescripcionEspecifica,
                                                      Cantidad = a.Cantidad,
                                                      Valor = a.Valor,
                                                      Devolucion = a.Devolucion
                                                  }).ToList();
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
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

            var clientes = (from a in db.Cliente.Where(w => (w.Activo ?? false))
                            select new
                            {
                                a.ID,
                                Name = (a.Nombres ?? "") + " " + (a.Apellidos ?? ""),
                                Tienda = a.Tienda
                            }
                        ).ToList();

            ViewBag.Responsable = new SelectList(clientes, "ID", "Name");

            return View();
        }

        public ActionResult Invoice()
        {


            int usuarioLogeado = InformationIdentity.GetID();

            var usr = db.Usuarios.Where(z => z.Id == usuarioLogeado).FirstOrDefault();

            var formaPago = (from a in db.CatalogDetail
                             join b in db.Catalog on a.CatalogId equals b.Id
                             where
                             b.Name == "FormaPago"
                             select
                             a
            ).ToList();

            List<CatalogDetail> tiendas = new List<CatalogDetail>();
            CatalogDetail tiendaEmpty = new CatalogDetail();

            tiendaEmpty.Name = "Seleccione Tienda";
            tiendaEmpty.Id = 0;
            tiendas.Add(tiendaEmpty);
            if (usr.Store == null)
            {
                tiendas.AddRange( (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           join c in db.CONFIGURACIONTIENDASUCURSALs on a.Id equals c.TIENDA
                           where
                           b.Name == "Tienda" &&
                           c.SUCURSAL == usr.SUCURSAL
                           select
                           a
            ).ToList());
            }
            else
            {
                tiendas.AddRange( (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda" && a.Id == usr.Store
                           select
                           a
                           ).ToList());
            }

            List<FacturaCustom> cliente = new List<FacturaCustom>();
            FacturaCustom clienteEmpty = new FacturaCustom();

            clienteEmpty.Comentario = "Seleccione Cliente";
            clienteEmpty.ID = 0;
            cliente.Add(clienteEmpty);
            cliente.AddRange((from a in db.Cliente.Where(z => z.Activo == true) select new FacturaCustom
            {
             ID = a.ID,
             Comentario = (a.Telefono??"")+" - " + a.Nombres + " " + a.Apellidos
            }).ToList());

           
            ViewBag.Tiendas = new SelectList(tiendas, "ID", "Name");
            ViewBag.FormaPago = new SelectList(formaPago, "ID", "Name");
            ViewBag.Clientes = new SelectList(cliente, "ID", "Comentario");

            return View();
        }
        [HttpPost]
        public ActionResult Invoice(Collection_invoice factura)
        {

            var fact = factura.Invoice;

            DateTime today = CustomValuesFormat.currentDateTimeUTC();

            fact.CreatedBy = InformationIdentity.GetID();
            fact.ModifiedBy = InformationIdentity.GetID();
            fact.CreatedOn = today;
            fact.ModifiedOn = today;
            fact.Fecha = today;
            fact.Pagada = true;

            Factura facturaentity = new Factura();
            facturaentity.Fecha = fact.Fecha;
            facturaentity.Comentario = "";
            facturaentity.ClientID = fact.ClientID;
            facturaentity.Tienda = fact.Tienda;
            facturaentity.Pagada = fact.Pagada;
            facturaentity.Revertida = false;
            facturaentity.Cancelada = false;
            facturaentity.CreatedOn = fact.CreatedOn;
            facturaentity.ModifiedOn = fact.ModifiedOn;
            facturaentity.CreatedBy = fact.CreatedBy;
            facturaentity.ModifiedBy = fact.ModifiedBy;
            facturaentity.FormaPago = fact.FormaPago;
            db.Factura.Add(facturaentity);
            db.SaveChanges();

            if (facturaentity.ID != 0)
            {

                List<FacturaDetalle> facturaDetalleList = new List<FacturaDetalle>();

                var productoIds = factura.InvoiceDetail.Select(s => s.ProductoID).ToList();

                var almacenDefaultId = db.CatalogDetail.FirstOrDefault(f => f.CatalogId == 3 && f.Isdefault && f.ParentId == facturaentity.Tienda.Value)?.Id;
                var existencia = db.Existencia.Where(w => productoIds.Contains(w.IDProducto) && w.IDAlmacen == almacenDefaultId).ToList();

                var tipoMov = db.CatalogDetail.FirstOrDefault(f => f.CatalogId == 9 && f.Name.ToLower() == "factura");
                List<MovimientoExitencia> movimientos = new List<MovimientoExitencia>();

                factura.InvoiceDetail.ForEach(f =>
                {
                    FacturaDetalle detalle = new FacturaDetalle();
                    detalle.ProductoID = f.ProductoID;
                    detalle.Valor = f.Valor;
                    detalle.FacturaID = facturaentity.ID;
                    detalle.IVA = 0;
                    detalle.Cantidad = f.Cantidad;

                    var exist = existencia.FirstOrDefault(p => p.IDProducto == f.ProductoID);
                    exist.Cantidad -= f.Cantidad;
                    facturaDetalleList.Add(detalle);

                    var mov = new MovimientoExitencia();
                    mov.IDExistencia = exist.ID;
                    mov.IDTipoMovimiento = tipoMov.Id;
                    mov.Comentario = "Movimiento de facturación";
                    mov.Cantidad = ((int)f.Cantidad) * (-1);
                    mov.CreatedBy = fact.CreatedBy;
                    mov.CreatetedOn = today;

                    movimientos.Add(mov);

                });
                db.MovimientoExitencia.AddRange(movimientos);
                db.FacturaDetalle.AddRange(facturaDetalleList);
                db.SaveChanges();

            }

            return Redirect("Index");
        }

        public JsonResult GetProductosJson(int TiendaId)
        {
            var productos = (from a in db.Producto
                             join b in db.Existencia on a.ID equals b.IDProducto
                             join c in db.CatalogDetail on b.IDAlmacen equals c.Id
                             where a.Activo == true && c.Isdefault == true && c.ParentId == TiendaId && b.Cantidad>0

                             group new { a, b } by a.ID into g
                             select new ProductoCustom
                             {
                                 ID = g.FirstOrDefault().a.ID,
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


                             }

                           ).ToList();

            var ObjJson = new { productos = productos };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDataJson()
        {


            var clientes = (from a in db.Cliente.Where(w => (w.Activo ?? false))
                            select
                            a
                      ).OrderBy(o => o.Nombres).ToList();




            var tiendas = (from a in db.CatalogDetail
                           join b in db.Catalog on a.CatalogId equals b.Id
                           where
                           b.Name == "Tienda"
                           select
                           a
          ).ToList();


            var formaPago = (from a in db.CatalogDetail
                             join b in db.Catalog on a.CatalogId equals b.Id
                             where
                             b.Name == "FormaPago"
                             select
                             a
        ).ToList();

            var ObjJson = new { responsables = clientes, tiendas = tiendas, formaPago = formaPago };
            return Json(ObjJson, JsonRequestBehavior.AllowGet);

        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(FacturaDetalleCustom facturaCustom)
        {
            var producto = (from a in db.Producto where a.CodigoBarra == facturaCustom.CodigoBarra select a).FirstOrDefault();


            if (producto != null && producto.CantidadDisponible >= facturaCustom.Cantidad)
            {
                Factura facturaID = (from a in db.Factura.Where(z => z.Pagada.Value == false) select a).FirstOrDefault();
                if (facturaID == null)
                {
                    facturaID = new Factura();
                    facturaID.ClientID = 1;
                    facturaID.Fecha = CustomValuesFormat.currentDateTimeUTC();
                    facturaID.Pagada = false;
                    facturaID.Revertida = false;
                    facturaID.Cancelada = false;
                    facturaID.CreatedBy = InformationIdentity.GetID();
                    facturaID.CreatedOn = CustomValuesFormat.currentDateTimeUTC();
                    facturaID.ModifiedBy = InformationIdentity.GetID();
                    facturaID.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
                    db.Factura.Add(facturaID);
                    db.SaveChanges();
                }
                if (facturaID.ID > 0)
                {
                    FacturaDetalle factura = new FacturaDetalle();
                    factura.Cantidad = facturaCustom.Cantidad;
                    factura.FacturaID = facturaID.ID;
                    factura.ProductoID = producto.ID;
                    factura.Valor = producto.PrecioVenta;

                    producto.CantidadDisponible = producto.CantidadDisponible - facturaCustom.Cantidad;
                    db.FacturaDetalle.Add(factura);
                    db.SaveChanges();
                }


                var FacturaDetallesss = (
                  from a in db.FacturaDetalle.Where(z => z.FacturaID == facturaID.ID)
                  join b in db.Producto on a.ProductoID equals b.ID
                  select new FacturaDetalleCustom
                  {
                      ProductoNombre = b.Marca,
                      Cantidad = a.Cantidad,
                      Valor = a.Valor
                  }
                    ).ToList();


                ViewBag.FacturaDetalleInf = FacturaDetallesss;
                ViewBag.Total = FacturaDetallesss.Sum(z => (z.Cantidad * z.Valor));

            }
            else
            {
                if (producto == null)
                {
                    ModelState.AddModelError("Error", "El producto no Existe!!");
                }
                else
                {
                    ModelState.AddModelError("Error", "La cantidad Existen es de:" + producto.CantidadDisponible);
                }
            }

            return View(facturaCustom);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,NumeroFactura,Fecha,Comentario,ClientID,Pagada,Revertida,Cancelada,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Pagar()
        {
            Factura facturaID = (from a in db.Factura.Where(z => z.Pagada.Value == false) select a).FirstOrDefault();
            facturaID.Pagada = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       

        public ActionResult Delete(int id)
        {

            return View(db.Factura.Find(id));
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Factura info)
        {
            var facturaDetalle = (from a in db.FacturaDetalle where a.FacturaID == info.ID select a).ToList();

            foreach (FacturaDetalle item in facturaDetalle)
            {
                var producto = (from a in db.Producto where a.ID == item.ProductoID select a).FirstOrDefault();
                producto.CantidadDisponible = producto.CantidadDisponible + item.Cantidad;

                db.SaveChanges();
            }
            Factura factura = db.Factura.Find(info.ID);
            factura.Cancelada = true;
            factura.ModifiedOn = CustomValuesFormat.currentDateTimeUTC();
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult confirmarProductos()
        {

            var conciliacionAbierta = db.Inventario.Any(a => a.Cerrado == null);
            ViewBag.ConciliacionAbierta = conciliacionAbierta;

            List<DrugStoreMnngt.Models.FacturaCustomConfirmacion> lista = (from a in db.Factura
                                                                           join b in db.FacturaDetalle.Where(zzz => zzz.Devolucion == null) on a.ID equals b.FacturaID
                                                                           join c in db.Producto on b.ProductoID equals c.ID
                                                                           join d in db.Cliente on a.ClientID equals d.ID
                                                                           select new DrugStoreMnngt.Models.FacturaCustomConfirmacion
                                                                           {
                                                                               ID = b.ID,
                                                                               Cliente = d.Nombres,
                                                                               NumeroFactura = "" + a.ID,
                                                                               Cantidad = b.Cantidad ?? 0,
                                                                               CodigoProducto = c.CodigoProducto,
                                                                               FechaFactura = a.CreatedOn,
                                                                               NombreProducto = c.DescripcionEspecifica,
                                                                               CodigoBarra = c.CodigoBarra
                                                                           }).ToList();
            return View(lista);
        }


        public JsonResult aprobarProducto(string ID)
        {
            var ids = ID.Split(',').Select(a => int.Parse(a)).ToList();

            var facturaDetalle = db.FacturaDetalle.Where(w => ids.Contains(w.ID)).ToList(); //var facturaDetalle = (from a in db.FacturaDetalle where a.ID == ID select a).FirstOrDefault();
            facturaDetalle.ForEach(f =>
            {
                f.Devolucion = false;
            });
            db.SaveChanges();
            //return RedirectToAction("confirmarProductos", true);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult denegarProducto(string ID)
        {

            var ids = ID.Split(',').Select(a => int.Parse(a)).ToArray();

            var facturaDetalle = db.FacturaDetalle.Where(w => ids.Contains(w.ID)).ToList(); // (from a in db.FacturaDetalle where a.ID == ID select a).FirstOrDefault();

            var tipoMov = db.CatalogDetail.FirstOrDefault(f => f.CatalogId == 9 && f.Name.ToLower() == "factura");

            facturaDetalle.ForEach(f =>
            {
                f.Devolucion = true;
                var factura = db.Factura.FirstOrDefault(fact => fact.ID == f.FacturaID);
                var storageDefault = db.CatalogDetail.FirstOrDefault(st => st.CatalogId == 3 && st.Isdefault && st.ParentId == factura.Tienda);
                var existencia = db.Existencia.FirstOrDefault(d => d.IDProducto == f.ProductoID && d.IDAlmacen == storageDefault.Id);

                existencia.Cantidad += f.Cantidad;

                MovimientoExitencia mov = new MovimientoExitencia();
                mov.Cantidad = (int)(f.Cantidad ?? 0);
                mov.Comentario = "Movimiento de devolución de factura";
                mov.CreatedBy = InformationIdentity.GetID();
                mov.CreatetedOn = CustomValuesFormat.currentDateTimeUTC();
                mov.IDExistenciaTarget = existencia.ID;
                mov.IDTipoMovimiento = tipoMov.Id;

                db.MovimientoExitencia.Add(mov);

                db.SaveChanges();
            });




            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("confirmarProductos",true);
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
