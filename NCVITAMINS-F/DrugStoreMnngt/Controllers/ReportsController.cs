using DrugStoreMnngt.Models;
using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class ReportsController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();
        // GET: Reports
        public ActionResult Ventas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ventas(FilterReport filters)
        {
            filters.toDate = filters.toDate.Value.AddDays(1);
            List<FacturaCustom> returnInf = (from a in db.Factura.Where(z => z.Fecha.Value >= filters.fromDate.Value && z.Fecha.Value < filters.toDate.Value)
                                             select new FacturaCustom
                                             {
                                                 ID = a.ID,
                                                 Fecha = a.Fecha,
                                                 Neto = db.FacturaDetalle.Where(z => z.FacturaID == a.ID).Select(x => x.Cantidad * x.Valor).Sum() ?? 0
                                             }).ToList();
            ViewBag.Lista = returnInf;
            return View(filters);
        }

        public ActionResult productStockMinimo()
        {
            var producto =
                (from a in db.Producto
                 join b in db.Existencia on a.ID equals b.IDProducto
                 join c in db.CatalogDetail on b.IDAlmacen equals c.Id
                 where a.Activo == true //&& c.Isdefault == true

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

                )
                .Where(a =>
                    a.StockMinimo != null &&
                    a.CantidadDisponible != null &&
                    a.CantidadDisponible <= a.StockMinimo
                )
                .ToList();

            return View(producto);
        }

        public ActionResult productosVendidos()
        {
            FilterReport filters = new FilterReport();
            filters.toDate = DateTime.Today;
            filters.fromDate = DateTime.Today;
            filters.IncluirResponsableDeshabilitado = true;

            //var responsableList = this.db.Cliente.Where((w => filters.IncluirResponsableDeshabilitado ? true : w.Activo == (bool?)true)).Select(a => new
            //{
            //    ID = a.ID,
            //    Name = (a.Nombres ?? "") + " " + (a.Apellidos ?? ""),
            //    Activo = a.Activo,
            //    Tienda = a.Tienda
            //}).ToList();
            //responsableList.Insert(0, null);
            //ViewBag.Responsable = responsableList;

            return View(filters);
        }
        public ActionResult VentasProductosMensuales()
        {
            FilterReport filters = new FilterReport();
            filters.filterString= CustomValuesFormat.currentDateTimeUTC().Year.ToString();
            filters.filterStringF = CustomValuesFormat.currentDateTimeUTC().Month.ToString();
            return View(filters);
        }

        [HttpPost]
        public ActionResult VentasProductosMensuales(FilterReport filters)
        {
            int anio = int.Parse(filters.filterString);
            int mes = int.Parse(filters.filterStringF);

            List<vw_VentasPorProductoMensuales> returnInf = db.vw_VentasPorProductoMensuales.Where(z=>z.Mes== mes && z.Anio== anio).ToList();

            ViewBag.Lista = returnInf;
            return View(filters);
        }

        public ActionResult VentaProductoMensualesUnidad(int? anio,int? mes,int? producto)
        {          
            return View(db.vw_VentaProductoUnidadPorMes.Where(z=>z.FECHAFACTURA.Value.Year == anio && z.FECHAFACTURA.Value.Month== mes && z.Producto == producto).ToList());
        }

        [HttpPost]
        public ActionResult productosVendidos(FilterReport filters)
        {

            var responsableList = this.db.Cliente.Where((w => filters.IncluirResponsableDeshabilitado ? true : w.Activo == (bool?)true)).Select(a => new
            {
                ID = a.ID,
                Name = (a.Nombres ?? "") + " " + (a.Apellidos ?? ""),
                Activo = a.Activo,
                Tienda = a.Tienda
            }).ToList();
            responsableList.Insert(0, null);
           

            var formaPago = (from a in db.CatalogDetail
                             join b in db.Catalog on a.CatalogId equals b.Id
                             where
                             b.Name == "FormaPago"
                             select
                             a
    ).ToList();

            int usuario = InformationIdentity.GetID();
            filters.toDate = filters.toDate.Value.AddDays(1);
            db.Database.CommandTimeout = 0;
            List<FacturaCustom> returnInf = (from a in db.Factura.Where(z => z.Fecha.Value >= filters.fromDate.Value && z.Fecha.Value < filters.toDate.Value && z.CreatedBy == usuario)
                                             join b in db.FacturaDetalle on a.ID equals b.FacturaID
                                             join c in db.Producto on b.ProductoID equals c.ID                                             
                                             join f in db.CatalogDetail on a.Tienda equals f.Id
                                             join e in db.Usuarios on a.CreatedBy equals e.Id
                                             select new FacturaCustom
                                             {
                                                 ID = a.ID,
                                                 Fecha = a.Fecha,
                                                 NombreProducto = c.DescripcionEspecifica,
                                                 Cantidad = b.Cantidad ?? 0,
                                                 Neto = db.FacturaDetalle.Where(z => z.FacturaID == a.ID).Select(x => x.Cantidad * x.Valor).Sum() ?? 0,
                                                 Valor = b.Valor,
                                                 Estado = b.Devolucion,
                                                 total = (b.Valor ?? 0) * (b.Cantidad ?? 0),
                                                 Codigo = c.CodigoProducto,
                                                 NombreResponsable = e.Usuario,
                                                 NombreTienda = f.Name,
                                                 FormaPago = a.FormaPago
                                             }).ToList();


            returnInf.ForEach(f =>
            {
                f.FormaPagoText = f.FormaPago == null ? "" : formaPago.FirstOrDefault(fp => fp.Id == f.FormaPago.Value).Name;
            });


            ViewBag.formaPago = formaPago;
            ViewBag.Lista = returnInf;
            ViewBag.Responsable = responsableList;
            return View(filters);
        }

        public ActionResult CierreCaja()
        {
            int id = InformationIdentity.GetID();
            var usuario = db.Usuarios.Where(z => z.Id ==id).FirstOrDefault();
            DateTime fecha = CustomValuesFormat.currentDateTimeUTC();
            List<CIERRECAJAGENERAR_Result> lista =  db.CIERRECAJAGENERAR(fecha, id).ToList();
            ViewBag.ResponsableNombre = usuario.Usuario;
            ViewBag.FechaConsulta = fecha.ToShortDateString();

            return View(lista);
        }

        public ActionResult CierreCajaAdmin()
        {
            FilterReport filters = new FilterReport();
            filters.toDate = CustomValuesFormat.currentDateTimeUTC();

            var responsableList = this.db.Usuarios.Where(w => w.Tipo == 2 ).Select(a => new
            {
                ID = a.Id,
                Name = a.Usuario              
            }).ToList();
            responsableList.Insert(0, null);
            ViewBag.Responsable = responsableList;

            return View(filters);
        }

        

        [HttpPost]
        public ActionResult CierreCajaAdmin(FilterReport filters)
        {
            var responsableList = this.db.Usuarios.Where(w => w.Tipo == 2).Select(a => new
            {
                ID = a.Id,
                Name = a.Usuario
            }).ToList();
            //responsableList.Insert(0, null);

            var usuario = db.Usuarios.Where(z=>z.Id==filters.ResponsableId).FirstOrDefault();
            List<CIERRECAJAGENERAR_Result> lista = db.CIERRECAJAGENERAR(filters.toDate, filters.ResponsableId).ToList();

            ViewBag.Responsable = responsableList;
            ViewBag.Lista = lista;
            ViewBag.ResponsableNombre = usuario != null? usuario.Usuario:"";
            ViewBag.FechaConsulta = filters.fromDate;
            return View(filters);
        }
        public ActionResult garficaBarraSimple()
        {
            int anio = CustomValuesFormat.currentDateTimeUTC().Year;
            int usuario = InformationIdentity.GetID();

            List<vw_VentasVendedorXMes> venta= db.vw_VentasVendedorXMes.Where(z=>z.Anio == anio && z.USUARIO == usuario).ToList();

            ViewBag.Venta = venta;
            return View();
        }

        public ActionResult garficaBSimpleAnual()
        {
            int anio = CustomValuesFormat.currentDateTimeUTC().Year;

            List< vw_VentasNetasXMes> venta = (from a in db.vw_VentasNetasXMes.Where(z => z.Anio == anio) select a).ToList();

            ViewBag.Venta = venta;
            return View();
        }
    }
}