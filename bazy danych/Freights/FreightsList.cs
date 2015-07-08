using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    public class FreightsList
    {
        public enum Types { Container, Dump, Flatbed, Lowboy, Refrigerated, Tank };
        public string Type { get; set; }
        public string Name{get;set;}
        public bool[] AdrClass{get;set;} 
        public bool Adr { get; set; }
        //public bool Busy { get; set; }
        public string Comment { get; set; }
        public uint Id { get; private set; }

        public FreightsList(){
        }
        public FreightsList(uint id, string name, string type, string adrClass, bool adr, string comment)
        {
            Id = id;
            Type = type;
            Name = name;
            AdrClass = Functions.ExpADR(adrClass);
            Adr = adr;

            Comment = comment;
        }
    }
}
