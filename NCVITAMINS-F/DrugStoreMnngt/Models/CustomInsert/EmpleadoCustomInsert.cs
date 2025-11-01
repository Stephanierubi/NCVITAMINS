using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models.CustomInsert
{
    public class EmpleadoCustomInsert
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Estado { get; set; }
        public int Barrio { get; set; }      
        public int Ciudad { get; set; }
        public int Departamento { get; set; }
    }
}