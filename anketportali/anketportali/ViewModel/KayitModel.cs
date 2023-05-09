using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anketportali.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitSoruId { get; set; }
        public string kayitKatilimciId { get; set; }


        public KatilimciModel katBilgi { get; set; }
        public SorularModel soruBilgi { get; set; }
    }
}