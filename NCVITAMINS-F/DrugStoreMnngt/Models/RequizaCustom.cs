using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class RequizaCustom
    {
        public RequizaHeaderCustom requiza { get; set; }

        public List<RequizaDetalleCustom>requizaDetalle{get;set; }
    }
}