using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DrugStoreMnngt.Models;
using CCustomization.CustomClasses;
using System.Collections.Generic;
using SecurityShell.Models;
using DrugStoreMnngt.Models.BD;

namespace DrugStoreMnngt.Controllers
{
  
        [SecurityFilterManage]
        public class AccountController : Controller
        {


        DB_A54362_mngEntities dbAccess = new DB_A54362_mngEntities();

            //
            // GET: /Account/Login
            [AllowAnonymous]
            public ActionResult Login(string returnUrl)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            //
            // POST: /Account/Login
            [HttpPost]
            [AllowAnonymous]
            public async Task<ActionResult> Login(AutorizeClass model, string returnUrl)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AutorizeClass returnInformation = new AutorizeClass();

                var usuario = dbAccess.Usuarios.Where(z => z.Usuario == model.userName && z.Contrasenia == model.password ).FirstOrDefault();

                if (usuario == null)
                {

                    ModelState.AddModelError(string.Empty, "Usuario o contraseña errada!!");
                    return View(model);
                }
                else
                {
                if (usuario.ACTIVO == true)
                {
                    model.userID = usuario.Id;
                    model.typeUser = usuario.Tipo ?? 0;
                    IdentitySignin(model, model.userName, false);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Usuario inactivo!!");
                    return View(model);
                }
                   
                }

                // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
                // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
                //  var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                //
                //var result = await SignInManager.PasswordSignInAsync(model.userName, model.password, true, shouldLockout: false);


            }





            public void IdentitySignin(AutorizeClass appUserState, string providerKey = null, bool isPersistent = false)
            {
                var claims = new List<Claim>();

                // create required claims
                claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.userName));
                claims.Add(new Claim(ClaimTypes.Name, appUserState.userName));
                claims.Add(new Claim("userID", appUserState.userID.ToString()));
                claims.Add(new Claim("typeUser", appUserState.typeUser.ToString()));
                // custom – my serialized AppUserState object
                claims.Add(new Claim("userState", appUserState.ToString()));

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = isPersistent,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                }, identity);
            }





            //
            // POST: /Account/LogOff
            [HttpPost]
            public ActionResult LogOff()
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index", "Home");
            }



            protected override void Dispose(bool disposing)
            {

                base.Dispose(disposing);
            }

            #region Aplicaciones auxiliares
            // Se usa para la protección XSRF al agregar inicios de sesión externos
            private const string XsrfKey = "XsrfId";

            private IAuthenticationManager AuthenticationManager
            {
                get
                {
                    return HttpContext.GetOwinContext().Authentication;
                }
            }

            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            private ActionResult RedirectToLocal(string returnUrl)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            internal class ChallengeResult : HttpUnauthorizedResult
            {
                public ChallengeResult(string provider, string redirectUri)
                    : this(provider, redirectUri, null)
                {
                }

                public ChallengeResult(string provider, string redirectUri, string userId)
                {
                    LoginProvider = provider;
                    RedirectUri = redirectUri;
                    UserId = userId;
                }

                public string LoginProvider { get; set; }
                public string RedirectUri { get; set; }
                public string UserId { get; set; }

                public override void ExecuteResult(ControllerContext context)
                {
                    var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                    if (UserId != null)
                    {
                        properties.Dictionary[XsrfKey] = UserId;
                    }
                    context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
                }
            }
            #endregion
        }
    
}