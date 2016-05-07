using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    public class Companies
    {
        public CitiesList City { get; set; }
        public CompanyNamesList CompanyName {get;set;}
        public string Adress { get; set; }
        public string Comment {get;set;}
        public uint Id { get; private set; }

        public Companies(){
        }
        public Companies(uint id, CitiesList city, CompanyNamesList companyName,string adress, string comment)
        {
            Id = id;
            City = city;
            CompanyName = companyName;
            Adress = adress;
            Comment = comment;
        }
    }
}
