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
    
    public partial class Katilimci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Katilimci()
        {
            this.Kayit = new HashSet<Kayit>();
        }
    
        public string katId { get; set; }
        public string katNo { get; set; }
        public string katAdSoyad { get; set; }
        public string katMail { get; set; }
        public int katYas { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kayit> Kayit { get; set; }
    }
}
