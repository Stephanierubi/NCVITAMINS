using CCustomization.RespuestaApi;
using mixposAPI.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.ModelBinding;

namespace mixposAPI.Controllers.Generico
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CatalogoController : ApiController
    {
        controleEntities db = new controleEntities();
        RespuestasAccionesAPI rp = new RespuestasAccionesAPI();

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoBusquedaProducto")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoBusquedaProducto()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue=null;

            returnValue = (from catalgo in db.Catalogs.Where(z=>z.Name== "TipoBusqueda" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion =  detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle=detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoBusquedaProductoTodos")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoBusquedaProductoTodos()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoBusqueda" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerComapanias")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerComapanias()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Compania" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }
        [HttpGet]
        [Route("api/Catalogo/obtenerComapaniasTodas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerComapaniasTodas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Compania" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerCompania")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerCompania(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Compania" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarCompania")]
        public IHttpActionResult agregarCompania(CCustomization.CustomClasses.Catalogo cat)
        {
            try {
                var catalogo = db.Catalogs.FirstOrDefault(z=>z.Name== "Compania");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet= new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy= usuario.Id;
                cdet.CreatedOn=DateTime.Now;
                cdet.ModifiedOn=DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTiposMovimientoInventarios")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTiposMovimientoInventarios()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoMovimientoInventario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }


        [HttpGet]
        [Route("api/Catalogo/obtenerTiposMovimientoInventariosTodos")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTiposMovimientoInventariosTodos()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoMovimientoInventario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoMovimientoInventario")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoMovimientoInventario(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoMovimientoInventario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarTipoMovimientoInventario")]
        public IHttpActionResult agregarTipoMovimientoInventario(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "TipoMovimientoInventario");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTiendas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTiendas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Tienda" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTiendasTodas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTiendasTodas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Tienda" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTienda")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTienda(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Tienda" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarTienda")]
        public IHttpActionResult agregarTienda(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "Tienda");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoClientes")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoClientes()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoCliente" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoClientesTodos")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoClientesTodos()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoCliente" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoCliente")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoCliente(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoCliente" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarTipoCliente")]
        public IHttpActionResult agregarTipoCliente(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "TipoCliente");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerLaborarios")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerLaborarios()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Laborario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerLaborariosTodos")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerLaborariosTodos()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Laborario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerLaborario")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerLaborario(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Laborario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarLaborario")]
        public IHttpActionResult agregarLaborario(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "Laborario");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoVentas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoVentas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoVenta" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoVentasTodas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoVentasTodas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoVenta" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoVenta")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoVenta(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoVenta" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }


        [HttpPost]
        [Route("api/Catalogo/agregarTipoVenta")]
        public IHttpActionResult agregarTipoVenta(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "TipoVenta");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoPrecentaciones")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoPrecentaciones()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoPrecentacion" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoPrecentacionesTodas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoPrecentacionesTodas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoPrecentacion" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoPrecentacion")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoPrecentacion(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoPrecentacion" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarTipoPrecentacion")]
        public IHttpActionResult agregarTipoPrecentacion(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "TipoPrecentacion");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerAlmacenes")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerAlmacenes()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Almacen" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerAlmacenesTodas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerAlmacenesTodas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Almacen" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerAlmacen")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerAlmacen(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Almacen" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarAlmacen")]
        public IHttpActionResult agregarAlmacen(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "Almacen");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerMarcas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerMarcas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Marcas" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerMarcasTodas")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerMarcasTodas()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Marcas" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerMarca")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerMarca(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "Marcas" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarMarca")]
        public IHttpActionResult agregarMarca(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "Marcas");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoUsuarios")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoUsuarios()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoUsuario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.IsActive) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoUsuariosTodos")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoUsuariosTodos()
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoUsuario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpGet]
        [Route("api/Catalogo/obtenerTipoUsuario")]
        public List<CCustomization.CustomClasses.Catalogo> obtenerTipoUsuario(int id)
        {
            List<CCustomization.CustomClasses.Catalogo> returnValue = null;

            returnValue = (from catalgo in db.Catalogs.Where(z => z.Name == "TipoUsuario" && z.IsActive)
                           join detalleCatalogo in db.CatalogDetails.Where(z => z.Id == id) on catalgo.Id equals detalleCatalogo.CatalogId
                           join creadoPor in db.Usuarios on detalleCatalogo.CreatedBy equals creadoPor.Id
                           join modificadoPor in db.Usuarios on detalleCatalogo.ModifiedBy equals modificadoPor.Id
                           select new CCustomization.CustomClasses.Catalogo
                           {
                               CreadoPor = creadoPor.Usuario1,
                               ModificadoPor = modificadoPor.Usuario1,
                               FechaCreacion = detalleCatalogo.CreatedOn.ToString(),
                               FechaModificacion = detalleCatalogo.ModifiedOn.ToString(),
                               Nombre = detalleCatalogo.Name,
                               Valor = detalleCatalogo.Id.ToString(),
                               Estado = detalleCatalogo.IsActive ? "Activo" : "Inactivo",
                               Descripcion = detalleCatalogo.Description,
                               Comentario = detalleCatalogo.OptionalInformation,
                               IDCatalogo = catalgo.Id,
                               IDCatalogoDetalle = detalleCatalogo.Id
                           }).ToList();

            return returnValue;
        }

        [HttpPost]
        [Route("api/Catalogo/agregarTipoUsuario")]
        public IHttpActionResult agregarTipoUsuario(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogo = db.Catalogs.FirstOrDefault(z => z.Name == "TipoUsuario");
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                CatalogDetail cdet = new CatalogDetail();

                cdet.Name = cat.Nombre;
                cdet.CatalogId = catalogo.Id;
                cdet.Description = cat.Descripcion;
                cdet.OptionalInformation = cat.Comentario;
                cdet.IsActive = isActive;
                cdet.CreatedBy = usuario.Id;
                cdet.ModifiedBy = usuario.Id;
                cdet.CreatedOn = DateTime.Now;
                cdet.ModifiedOn = DateTime.Now;
                db.CatalogDetails.Add(cdet);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.agregarMensajeSastifactorio);
        }

        [HttpPut]
        [Route("api/Catalogo/actualizarInformacionCatalogo")]
        public IHttpActionResult actualizarInformacionCatalogo(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogoDetalle = db.CatalogDetails.FirstOrDefault(z=> z.Id==cat.IDCatalogoDetalle);
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z=>z.Usuario1.Equals(identity.Name));
                bool isActive = false;

                if (cat.Estado == "Activo")
                {
                    isActive = true;
                }
                else
                    isActive = false;

                if (catalogoDetalle != null) {
                    catalogoDetalle.Name = cat.Nombre;
                    catalogoDetalle.ModifiedOn = DateTime.Now;
                    catalogoDetalle.ModifiedBy = usuario.Id;
                    catalogoDetalle.IsActive = isActive;
                    catalogoDetalle.OptionalInformation = cat.Comentario;
                    catalogoDetalle.Description = cat.Descripcion;

                    db.SaveChanges();
                   
                }
                else
                {
                    throw new Exception("La informacion enviada no es correcta");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
          

            return Ok(rp.editarMensajeSastisfactorio);
        }
        [HttpDelete]
        [Route("api/Catalogo/eliminarInformacionCatalogo")]
        public IHttpActionResult eliminarInformacionCatalogo(CCustomization.CustomClasses.Catalogo cat)
        {
            try
            {
                var catalogoDetalle = db.CatalogDetails.FirstOrDefault(z => z.Id == cat.IDCatalogoDetalle);
                var identity = Thread.CurrentPrincipal.Identity;
                var usuario = db.Usuarios.FirstOrDefault(z => z.Usuario1.Equals(identity.Name));
              

                if (catalogoDetalle != null)
                {
               
                    catalogoDetalle.ModifiedOn = DateTime.Now;
                    catalogoDetalle.ModifiedBy = usuario.Id;
                    catalogoDetalle.IsActive = false;                  

                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("La informacion enviada no es correcta");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.eliminarMensajeSastisfactorio);
        }
        }
}
