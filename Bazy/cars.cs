//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bazy
{
    using System;
    using System.Collections.Generic;
    
    public partial class cars
    {
        public cars()
        {
            this.shipping = new HashSet<shipping>();
        }
    
        public int Id { get; set; }
        public string Number_plate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Carry { get; set; }
        public string IsUsed { get; set; }
        public string Sold { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<shipping> shipping { get; set; }
    }
}