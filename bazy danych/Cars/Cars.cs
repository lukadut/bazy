using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    public class Cars
    {
        public string Plate { get; set; }
        public string Make{get;set;}
        public string Model{ get; set; }
        public uint Carry { get; set; }
        public bool IsUsed { get; set; }
        public bool Sold { get; set; }
        //public bool Busy { get; set; }
        public string Comment { get; set; }
        public uint Id { get; private set; }

        public Cars(){
        }
        public Cars(uint id, string plate, string make, string model, uint carry, bool isUsed, bool sold, string comment)
        {
            Id = id;
            Plate = plate;
            Make = make;
            Model = model;
            Carry = carry;
            IsUsed = isUsed;
            Sold = sold;
            Comment = comment;
        }

    }
}
