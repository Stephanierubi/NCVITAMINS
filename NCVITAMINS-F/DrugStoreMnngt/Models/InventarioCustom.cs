using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class InventarioCustom
    {
        public int Id { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool? Cerrado { get; set; }
        public string createdByName { get; set; }
        public string modifiedByName { get; set; }
        public List<InventarioDetalleCustom> productos { get; set; }
    }

    public class InventarioDetalleCustom
    {
        public int Id { get; set; }
        public int InventarioId { get; set; }
        public int ProductoId { get; set; }
        public int AlmacenId { get; set; }
        public string NombreAlmacen { get; set; }
        public int CantidadFisica { get; set; }
        public int ExistenciaEnSistema { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
    }
}