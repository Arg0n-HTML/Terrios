using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrios.MVVM.Model
{
    internal class ServiceModel
    {
        public int Idservice { get; set; }
        public string NomService { get; set; }

        public ServiceModel(int idservice, string nomService)
        {
            Idservice = idservice;
            NomService = nomService;
        }
    }
    
}
