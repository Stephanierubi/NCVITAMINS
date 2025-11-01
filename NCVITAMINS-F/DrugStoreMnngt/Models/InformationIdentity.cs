using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using DrugStoreMnngt.Models.BD;

namespace SecurityShell.Models
{
    public static class InformationIdentity
    {
        public static int GetID()
        {
            var principal = ClaimsPrincipal.Current;

            int userId = 0;
            if (principal != null && principal.FindFirst("userID") != null)
                int.TryParse(principal.FindFirst("userID").Value, out userId);
            return userId;

        }
        public static int TypeID()
        {
            var principal = ClaimsPrincipal.Current;

            int userId = 0;
            if (principal != null && principal.FindFirst("typeUser") != null)
                int.TryParse(principal.FindFirst("typeUser").Value, out userId);
            return userId;

        }
        public static string userName()
        {
            ConsultInformation inf = new ConsultInformation();

            return inf.getusername(InformationIdentity.GetID());
        }
    }

    public class ConsultInformation{
        private DB_A54362_mngEntities db = new DB_A54362_mngEntities();

        public string getusername(int userID)
        {
            return db.Usuarios.Where(z => z.Id == userID).FirstOrDefault().Usuario;
        }
    }
}