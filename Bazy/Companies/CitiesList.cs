using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    public class CitiesList
    {
        public string City { get; set; }

        public uint Id { get; private set; }

        public CitiesList(){
        }
        public CitiesList(uint id, string city)
        {
            Id = id;
            City = city;
        }
        public override string ToString()
        {
            return City;
        }
    }
}
