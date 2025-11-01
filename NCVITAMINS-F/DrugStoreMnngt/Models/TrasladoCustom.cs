using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class TrasladoCustomCustom
    {
        public List<TransladoDetalle> TrasladoDetalle { get; set; }
    }

    public class TransladoDetalle
    {
        public int ProductoId { get; set; }
        public int AlmacenOrigen { get; set; }
        public int AlmancenDestino { get; set; }
        public int Cantidad { get; set; }
        public int ExistenciaOrigen { get; set; }
        public int ExistenciaDestino { get; set; }

        public string Comentario { get; set; }
        public string CodigoProducto { get; set; }

        public string ProductoNombre { get; set; }
        public string AlmacenOrigenNombre { get; set; }
        public string AlmacenDestinoNombre { get; set; }
        public string TipoMov { get; set; }
        public DateTime Fecha { get; set; }
    }
}