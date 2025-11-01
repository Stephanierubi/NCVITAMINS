using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class Collection_invoice
    {
        public FacturaCustom Invoice { get; set; }
        public Nullable<int> ClientID { get; set; }
        public List<FacturaDetalleCustom> InvoiceDetail { get; set; }

        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }        
        public string telefono { get; set; }    
        public DateTime fechaNacimiento { get; set; }

    }
}