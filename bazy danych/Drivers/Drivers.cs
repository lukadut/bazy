using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    public class Drivers
    {
        public string Name{get;set;}
        public string Surname { get; set; }
        public uint Wage { get; set; }
        public bool Adr { get; set; }
        public bool Employed { get; set; }
        public bool Busy { get; set; }
        public string Comment { get; set; }
        public uint Id { get; private set; }

        public Drivers(){
        }
        public Drivers(uint id, string name, string surname, uint wage, bool adr, bool employed, bool busy, string comment)
        {
            Id = id;
            if (name == null)
            {
                Name = "null";
            }
            else
                Name = name.ToString();
            Surname = surname;
            Wage = wage;
            Adr = adr;
            Employed = employed;
            Busy = busy;
            Comment = comment;
        }
    }
}
