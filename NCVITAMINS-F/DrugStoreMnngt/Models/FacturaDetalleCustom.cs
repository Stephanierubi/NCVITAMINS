using DrugStoreMnngt.Models.BD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class FacturaDetalleCustom
    {
        public int ID { get; set; }
        public Nullable<int> FacturaID { get; set; }
       public string ProductoNombre { get; set; }
        public string CodigoBarra { get; set; }
        public Nullable<int> ProductoID { get; set; }
        public Nullable<double> Cantidad { get; set; }
        public Nullable<double> Valor { get; set; }
        public Nullable<double> IVA { get; set; }
        public double? total { get; set; }
        public bool? Devolucion { get; set; }
    }
}