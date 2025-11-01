using mixposAPI.Controllers.TokenMngt;
using mixposAPI.Models.CustomClass;
using mixposAPI.Models.Model;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mixposAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        controleEntities db = new controleEntities();

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [Authorize]
        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" Usuario autenticado: {identity.Name}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            
            if (login == null)
               return  Content(HttpStatusCode.Unauthorized, "Los datos enviados estan vacios!!!");

            var usuario = db.Usuarios.FirstOrDefault(z=> z.Usuario1 == login.Usuario && z.Store == login.Compania);



            if (usuario == null)
                return Content(HttpStatusCode.Unauthorized, "El usuario no existe!!");

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (login.Password == usuario.Contrasenia);

            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Usuario,usuario.Id);
                return Ok(token);
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, "Password incorrecto!!");
            }
        }

    }
}