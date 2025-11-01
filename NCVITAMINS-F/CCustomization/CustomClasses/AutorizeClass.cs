using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCustomization.CustomClasses
{
    public class AutorizeClass
    {
        [Required(ErrorMessage = "Este Campo es requerido!")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido!")]
        public string password { get; set; }

        public string errorMessage { get; set; }
        public string SSI { get; set; }

        public int userID { get; set; }


        public int typeUser { get; set; }
    }
}
