using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class DeatellesRequizaCustom
    {
        public int ID { get; set; }
        public String nombreProveedor { get; set; }
        public String Usuario { get; set; }
        public DateTime? fecha  { get; set; }
        public double cantidadProducto { get; set; }
        public String nombreAlmacen { get; set; }



    }

    public class DeatellesRequizaItemsCustom
    {
        
        public String nombreProducto { get; set; }
        public String laboratorio { get; set; }
        public String Precentacion { get; set; }
        public double cantidad { get; set; }



    }
}