using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCustomization.RespuestaApi
{
    public class RespuestasAccionesAPI
    {
        public string agregarMensajeSastifactorio { get; set; }
        public string editarMensajeSastisfactorio { get; set; }
        public string eliminarMensajeSastisfactorio { get; set; }

        public RespuestasAccionesAPI()
        {
            agregarMensajeSastifactorio = "Registro agregado sastisfactoriamente";
            editarMensajeSastisfactorio = "Registro editado sastisfactoriamente";
            eliminarMensajeSastisfactorio = "Registro elimando sastisfactoriamente";
        }
    }
}
