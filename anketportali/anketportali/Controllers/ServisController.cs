using anketportali.Models;
using anketportali.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace anketportali.Controllers
{
    public class ServisController : ApiController
    {
        DB2Entities db = new DB2Entities();
        SonucModel sonuc = new SonucModel();


        #region Sorular

        [HttpGet]
        [Route("api/soruliste")]
        public List<SorularModel> SorularListe()
        {
            List<SorularModel> liste = db.Sorular.Select(x => new
                SorularModel()
            {
                soruId = x.soruId,
                soruSayi = x.soruSayi,
                soruKonusu = x.soruKonusu,
                soruKodu = x.soruKodu,
                soruCevabi = x.soruCevabi


            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/sorubyid/{Sorularid}")]
        public SorularModel SorularById(string soruId)
        {
            SorularModel kayit = db.Sorular.Where(s => s.soruId == soruId).Select(x => new
                SorularModel()
            {

                soruId = x.soruId,
                soruSayi = x.soruSayi,
                soruKonusu = x.soruKonusu,
                soruKodu = x.soruKodu,
                soruCevabi = x.soruCevabi

            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/soruekle")]
        public SonucModel SorularEkle(SorularModel model)
        {
            if (db.Sorular.Count(s => s.soruKodu == model.soruKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Soru Kodu Kayitlidir!";
                return sonuc;
            }
            Sorular yeni = new Sorular();
            yeni.soruId = Guid.NewGuid().ToString();
            yeni.soruSayi = model.soruSayi;
            yeni.soruKonusu = model.soruKonusu;
            yeni.soruCevabi = model.soruCevabi;
            yeni.soruKodu = model.soruKodu;

            db.Sorular.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sorular Eklendi";


            return sonuc;
        }


        [HttpPut]
        [Route("api/soruduzenle")]

        public SonucModel SorularDuzenle(SorularModel model)
        {
            Sorular kayit = db.Sorular.Where(s => s.soruId == model.soruId).FirstOrDefault();
            if (kayit == null)
            {

                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadi!";
                return sonuc;

            }

            kayit.soruSayi = model.soruSayi;
            kayit.soruKonusu = model.soruKonusu;
            kayit.soruCevabi = model.soruCevabi;
            kayit.soruKodu = model.soruKodu;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sorular Güncellendi";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/sorusil/{soruId}")]

        public SonucModel SorularSil(string soruId)
        {
            Sorular kayit = db.Sorular.Where(s => s.soruId == soruId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitSoruId == soruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sorulara Kayıtlı Katılımcı Olduğu İçin Sorular Silinemez!";
                return sonuc;
            }

            db.Sorular.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sorular Silindi";

            return sonuc;
        }

        #endregion


        #region Katilimci

        [HttpGet]
        [Route("api/katilimciliste")]
        public List<KatilimciModel> KatilimciListe()
        {
            List<KatilimciModel> liste = db.Katilimci.Select(x => new
                KatilimciModel()
            {
                katId = x.katId,
                katNo = x.katNo,
                katAdSoyad = x.katAdSoyad,
                katMail = x.katMail,
                katYas = x.katYas

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/katilimcibyid/{Katid}")]

        public KatilimciModel KatilimciById(string katId)
        {
            KatilimciModel kayit = db.Katilimci.Where(s => s.katId ==
              katId).Select(x => new KatilimciModel()
              {

                  katId = x.katId,
                  katNo = x.katNo,
                  katAdSoyad = x.katAdSoyad,
                  katMail = x.katMail,
                  katYas = x.katYas

              }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/katilimciekle")]
        public SonucModel KatilimciEkle(KatilimciModel model)
        {
            if (db.Katilimci.Count(c => c.katNo == model.katNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girdiğiniz Katılımcı Numarası Kayıtlıdır!";
                return sonuc;
            }
            Katilimci yeni = new Katilimci();
            yeni.katId = Guid.NewGuid().ToString();
            yeni.katNo = model.katNo;
            yeni.katAdSoyad = model.katAdSoyad;
            yeni.katMail = model.katMail;
            yeni.katYas = model.katYas;

            db.Katilimci.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Katılımcı Eklendi";


            return sonuc;
        }

        [HttpPut]
        [Route("api/katilimciduzenle")]

        public SonucModel KatilimciDuzenle(KatilimciModel model)
        {
            Katilimci kayit = db.Katilimci.Where(s => s.katId == model.katId).FirstOrDefault();
            if (kayit == null)
            {

                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadi!";
                return sonuc;

            }

            kayit.katNo = model.katNo;
            kayit.katAdSoyad = model.katAdSoyad;
            kayit.katMail = model.katMail;
            kayit.katYas = model.katYas;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Katılımcı Güncellendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/katilimcisil/{katId}")]

        public SonucModel KatilimciSil(string katId)
        {
            Katilimci kayit = db.Katilimci.Where(s => s.katId == katId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitKatilimciId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Katılımcı Üzerinde Soru Kaydı Olduğu İçin Katılımcı Silinemez!";
                return sonuc;
            }

            db.Katilimci.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Katılımcı Silindi";

            return sonuc;
        }

        #endregion


        #region Kayit

        [HttpGet]
        [Route("api/katilimcisoruliste/{katId}")]

        public List<KayitModel> KatilimciSoruListe(string katId)
        {

            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKatilimciId == katId).Select(x => new
              KayitModel()
            {
                kayitId = x.kayitId,
                kayitSoruId = x.kayitSoruId,
                kayitKatilimciId = x.kayitKatilimciId,

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.katBilgi = KatilimciById(kayit.kayitKatilimciId);
                kayit.soruBilgi = SorularById(kayit.kayitSoruId);
            }

            return liste;
        }


        [HttpGet]
        [Route("api/sorukatilimciliste/{soruId}")]

        public List<KayitModel> SorularKatilimciListe(string soruId)
        {

            List<KayitModel> liste = db.Kayit.Where(s => s.kayitSoruId == soruId).Select(x => new
              KayitModel()
            {
                kayitId = x.kayitId,
                kayitSoruId = x.kayitSoruId,
                kayitKatilimciId = x.kayitKatilimciId,

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.katBilgi = KatilimciById(kayit.kayitKatilimciId);
                kayit.soruBilgi = SorularById(kayit.kayitSoruId);
            }

            return liste;
        }


        [HttpPost]
        [Route("api/kayitekle")]

        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitSoruId == model.kayitSoruId && s.kayitKatilimciId ==
            model.kayitKatilimciId) > 0)

            {
                sonuc.islem = false;
                sonuc.mesaj = "İlgili Katilimci Soruya Katılmıştır!";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitKatilimciId = model.kayitKatilimciId;
            yeni.kayitSoruId = model.kayitSoruId;

            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Kaydı Eklendi";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]


        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Kaydı Silindi";
            return sonuc;

        }

        #endregion


        #region Cevaplar


        [HttpPost]
        [Route("api/cevaplarekle/{soruId}")]
        public SonucModel CevaplarEkle([FromBody] CevaplarModel model, [FromUri] string soruId)
        {
            if (db.Cevaplar.Any(x => x.cevapAd == model.cevapAd))
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Cevap Kayıtlıdır!";
                return sonuc;
            }

            Cevaplar yeni = new Cevaplar();
            yeni.cevapId = Guid.NewGuid().ToString();
            yeni.cevapAd = model.cevapAd;
            yeni.cevapSoruId = soruId;


            db.Cevaplar.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevaplar Eklendi";
            return sonuc;
        }

        [HttpGet]
        [Route("api/cevaplarliste")]
        public List<CevaplarModel> CevaplarListe()
        {
            List<CevaplarModel> liste = db.Cevaplar.Select(x => new CevaplarModel()
            {
                cevapAd = x.cevapAd,
                cevapId = x.cevapId,
                cevapSoruId = x.cevapSoruId,

            }).ToList();
            return liste;
        }

        [HttpDelete]
        [Route("api/cevaplarsil/{cevapId}")]
        public SonucModel CevaplarSil(string cevapId)
        {
            Cevaplar kayit = db.Cevaplar.Where(s => s.cevapId == cevapId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            db.Cevaplar.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Silindi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/cevaplarduzenle")]
        public SonucModel CevaplarDuzenle(CevaplarModel model)
        {
            Cevaplar kayit = db.Cevaplar.Where(s => s.cevapId == model.cevapId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Bulunamadı!";
                return sonuc;
            }
            kayit.cevapAd = model.cevapAd;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Düzenlendi";
            return sonuc;
        }


        #endregion

    }
}