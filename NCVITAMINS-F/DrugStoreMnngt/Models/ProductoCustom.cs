using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class ProductoCustom
    {

        public int ID { get; set; }
        public string Marca { get; set; }
        public string DescripcionEspecifica { get; set; }
        public string DescripcionGeneral { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public Nullable<int> TipoPrecentacionID { get; set; }
        public string TipoPresentacion { get; set; }
        public Nullable<double> PrecioCompra { get; set; }
        public Nullable<double> PrecioVenta { get; set; }
        public Nullable<int> ProveedorID { get; set; }
        public string CodigoBarra { get; set; }
        public Nullable<double> CantidadDisponible { get; set; }
        public Nullable<double> CantidadPrecentacion { get; set; }
        public Nullable<int> TipoVentaID { get; set; }
        public Nullable<int> Almacen { get; set; }
        public Nullable<int> Laboratorio { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<bool> IVA { get; set; }
        public Nullable<double> StockMinimo { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string CodigoProducto { get; set; }
        public IEnumerable<AlmacenDetalle> almacenes { get; set; }
        public int Existencia { get; set; }

    }

    public class AlmacenDetalle
    {
        public string Almacen { get; set; }
        public double? Cantidad { get; set; }
    }
}