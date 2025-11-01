using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCustomization.CustomClasses
{
    public class FilterReport
    {
        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }

        public String filterString { get; set; }

        public String filterStringF { get; set; }

        public String filterStringS { get; set; }

        public String filterStringT { get; set; }
            public int producto { get; set; }

        public Boolean? IncluirResponsableDeshabilitado { get; set; }
    }

    public class Credito
    {
        public int ID { get; set; }
        public DateTime Fecha{ get; set; }
        public string Nombre { get; set; }
        public double CreditoMonto { get; set; }
        public double Pago { get; set; }
    }
}
