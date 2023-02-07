using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrios.MVVM.Model
{
    internal class SiteModel
    {
        public int Idsite { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Nomsite { get; set;}

        public SiteModel(int idsite, string adresse, string ville, string nomsite)
        {
            Idsite = idsite;
            Adresse = adresse;
            Ville = ville;
            Nomsite = nomsite;
        }
    }
}
