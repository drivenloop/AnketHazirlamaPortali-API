//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace anketportali.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kayit
    {
        public string kayitId { get; set; }
        public string kayitSoruId { get; set; }
        public string kayitKatilimciId { get; set; }
    
        public virtual Katilimci Katilimci { get; set; }
        public virtual Sorular Sorular { get; set; }
    }
}
