using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class FacturaCustomConfirmacion
    {
        public int ID { get; set; }
        public String NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public String Cliente { get; set; }
        public String CodigoProducto { get; set; }
        public String CodigoBarra { get; set; }
        public String NombreProducto { get; set; }
        public double Cantidad { get; set; }
    }
}