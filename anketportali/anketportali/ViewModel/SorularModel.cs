using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anketportali.ViewModel
{
    public class SorularModel
    {
        public string soruId { get; set; }
        public int soruSayi { get; set; }
        public string soruKonusu { get; set; }
        public string soruCevabi { get; set; }
        public string soruKodu { get; set; }
    }
}