using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anketportali.ViewModel
{
    public class KatilimciModel
    {
        public string katId { get; set; }
        public string katNo { get; set; }
        public string katAdSoyad { get; set; }
        public string katMail { get; set; }
        public int katYas { get; set; }
    }
}