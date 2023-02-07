using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrios.MVVM.Model
{
    internal class EmployeModel
    {
        public int IdEmploye { get; set; }
        public String NomEmploye { get; set; }
        public String PrenomEmploye { get; set; }
        public String TelProEmploye { get; set; }
        public String TelPersoEmploye { get; set; }
        public String EmailEmploye { get; set; }
        public String ServiceEmploye { get; set; }
        public String SiteEmploye { get; set; }

        public EmployeModel(int Idemploye, string nomEmploye, string prenomEmploye, string telProEmploye, string telPersoEmploye, string emailEmploye, string serviceEmploye, string siteEmploye)
        {
            IdEmploye = Idemploye;
            NomEmploye = nomEmploye;
            PrenomEmploye = prenomEmploye;
            TelProEmploye = telProEmploye;
            TelPersoEmploye = telPersoEmploye;
            EmailEmploye = emailEmploye;
            ServiceEmploye = serviceEmploye;
            SiteEmploye = siteEmploye;
        }
        
    }
}
