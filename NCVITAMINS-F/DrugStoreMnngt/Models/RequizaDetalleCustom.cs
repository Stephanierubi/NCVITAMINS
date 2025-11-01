using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class RequizaDetalleCustom
    {
        public int IDProducto { get; set; }
        public float Cantidad { get; set; }
        public int IDMarca { get; set; }
        public int IDLaboratorio { get; set; }
    }
}