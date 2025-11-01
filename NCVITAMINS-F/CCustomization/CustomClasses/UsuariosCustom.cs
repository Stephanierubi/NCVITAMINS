using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCustomization.CustomClasses
{
    public class UsuariosCustom
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public Nullable<int> Tipo { get; set; }
        public Nullable<int> Store { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
        public Nullable<int> SUCURSAL { get; set; }
        public string nombreSucursal { get; set; }
        public string nombreTienda { get;set; }

    }
}
