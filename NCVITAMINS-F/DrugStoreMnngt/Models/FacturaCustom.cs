using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class FacturaCustom
    {
        public int ID { get; set; }
        public string NumeroFactura { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Comentario { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> Tienda { get; set; }
        public Nullable<bool> Pagada { get; set; }
        public Nullable<bool> Revertida { get; set; }
        public Nullable<bool> Cancelada { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public int? FormaPago { get; set; }
        public string FormaPagoText { get; set; }
        public double Neto { get; set; }

        public string NombreProducto { get; set; }

        public double Cantidad { get; set; }
        public double total { get; set; }
        public string NombreResponsable { get; set; }
        public string NombreTienda { get; set; }
        public bool? Estado { get; set; }

        public Nullable<double> Valor { get; set; }
        public string CodigoBarra { get; set; }
        public string Codigo { get; set; }
    }
}