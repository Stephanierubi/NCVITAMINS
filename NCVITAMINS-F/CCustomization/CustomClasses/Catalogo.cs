using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCustomization.CustomClasses
{
    public class Catalogo
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public string Valor  { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get;set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
        public string Estado { get; set; }
        public int IDCatalogo { get; set; }
        public int IDCatalogoDetalle { get; set; }
    }
}
