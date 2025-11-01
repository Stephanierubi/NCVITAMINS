using CCustomization.RespuestaApi;
using mixposAPI.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mixposAPI.Controllers.Producto
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductoController : ApiController
    {
        controleEntities db = new controleEntities();
        RespuestasAccionesAPI rp = new RespuestasAccionesAPI();

        // GET: api/Producto/5
        [HttpGet]
        [Route("api/Producto/obtenerProducto")]
        public mixposAPI.Models.Model.Producto obtenerProducto(int id)
        {
            mixposAPI.Models.Model.Producto producto = db.Productoes.FirstOrDefault(z=> z.ID ==id);
            return producto;
        }

        [HttpGet]
        [Route("api/Producto/obtenerProductoDetalle")]
        public mixposAPI.Models.Model.vw_PordutosDetalle obtenerProductoDetalle(int id)
        {
            mixposAPI.Models.Model.vw_PordutosDetalle producto = db.vw_PordutosDetalle.FirstOrDefault(z => z.ID == id);
            return producto;
        }

        [HttpGet]
        [Route("api/Producto/obtenerProductoDetalles")]
        public List<mixposAPI.Models.Model.vw_PordutosDetalle> obtenerProductoDetalles(int tipoBusqueda,string valorbusqueda)
        {
            List<mixposAPI.Models.Model.vw_PordutosDetalle> producto= new List<vw_PordutosDetalle>();
            string busqueda = db.CatalogDetails.Where(z=> z.Id == tipoBusqueda).FirstOrDefault().Name;
           
            switch (busqueda)
            {
                case "Nombre Producto":
                    producto.AddRange(db.vw_PordutosDetalle.Where(z=>z.DescripcionEspecifica.Contains(valorbusqueda)).ToList());
                 break;
            }
             
            return producto;
        }

        // POST: api/Producto
        [HttpPost]
        [Route("api/Producto/agregarProducto")]
        public IHttpActionResult agregarProducto(mixposAPI.Models.Model.Producto producto)
        {
            try {
                db.Productoes.Add(producto);
                db.SaveChanges();
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message); 
            }
            
            return Ok(rp.agregarMensajeSastifactorio);
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        [HttpDelete]
        [Route("api/Producto/borrarProducto")]
        public IHttpActionResult borrarPrducto(mixposAPI.Models.Model.Producto producto)
        {
            try
            {
                db.Productoes.Remove(producto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.eliminarMensajeSastisfactorio);
        }
        [HttpPut]
        [Route("api/Producto/editarProducto")]
        public IHttpActionResult editarProducto(mixposAPI.Models.Model.Producto producto)
        {
            try
            {
                db.Productoes.AddOrUpdate(producto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(rp.editarMensajeSastisfactorio);
        }

    }
}
