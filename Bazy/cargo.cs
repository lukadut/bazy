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
    
    public partial class cargo
    {
        public cargo()
        {
            this.freights = new HashSet<freights>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ADR { get; set; }
        public string ADR_Class { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<freights> freights { get; set; }
    }
}
