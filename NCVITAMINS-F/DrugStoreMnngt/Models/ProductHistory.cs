using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class ProductHistory
    {
        public DateTime? Fecha { get; set; }
        public string Desde { get; set; }
        public string Para { get; set; }
        public string Comentario { get; set; }
        public int? Cantidad { get; set; }
        public string Creado { get; set; }
    }
}