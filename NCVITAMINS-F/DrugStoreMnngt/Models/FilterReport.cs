using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public class FilterReport
    {
        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public string filterString { get; set; }
        public string filterStringS { get; set; }
        public string filterStringT { get; set; }
        public string filterStringF { get; set; }
        public bool IncluirResponsableDeshabilitado { get;set; }
        public int? ResponsableId { get; set; }

        public int producto { get; set; }
    }
}