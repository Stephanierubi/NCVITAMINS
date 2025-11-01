using DrugStoreMnngt.Models.BD;
using SecurityShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrugStoreMnngt.Controllers
{
    [SecurityFilterManage]
    public class HomeController : Controller
    {
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();
        public ActionResult Index()
        {
            String nombreUsuarioLogeado = "";
            int idusuario = InformationIdentity.GetID();

            var empleadoId = db.UsuarioEmpleado.FirstOrDefault(z=>z.Usuario == idusuario );

            if ( empleadoId == null)
            {
                nombreUsuarioLogeado = db.Usuarios.First(z => z.Id == idusuario).Usuario;
            }
            else
            {
                var empleado = db.Empleado.First(z => z.ID == empleadoId.Empleado);
                if (empleado!=null)
                {
                    nombreUsuarioLogeado = empleado.Nombres + " " + empleado.Apellidos;
                }
                
            }

            ViewBag.NombreLogeado = nombreUsuarioLogeado;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}