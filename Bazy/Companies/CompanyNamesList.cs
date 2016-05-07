using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    public class CompanyNamesList
    {
        public string CompanyName { get; set; }

        public uint Id { get; private set; }

        public CompanyNamesList(){
        }
        public CompanyNamesList(uint id, string name)
        {
            Id = id;
            CompanyName = name;

        }
        public override string ToString()
        {
            return CompanyName;
        }
    }
}
