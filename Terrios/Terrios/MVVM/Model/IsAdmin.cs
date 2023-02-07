using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrios.MVVM.Model
{
    internal class IsAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Admin { get; set; }
        
        public IsAdmin(int id, string name, bool admin)
        {
            Id = id;
            Name = name;
            Admin = admin;
        }
    }
}
