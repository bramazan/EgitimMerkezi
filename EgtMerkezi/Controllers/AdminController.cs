using EgtMerkezi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgtMerkez.Controllers
{
    public class AdminController : Controller
    {
        EgitimMerkeziEntities ent = new EgitimMerkeziEntities();

        #region // Slider

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult Slider()
        {
            var slider = ent.Slider.ToList();
            return View(slider);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult SlideEkle()
        {
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult SlideDuzenle(int SlideID)
        {
            var _slideDuzenle = ent.Slider.Where(x => x.ID == SlideID).FirstOrDefault();
            return View(_slideDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult SlideSil(int SlideID)
        {
            try
            {
                ent.Slider.Remove(ent.Slider.First(d => d.ID == SlideID));
                ent.SaveChanges();
                return RedirectToAction("Slider", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult SlideEkle(Slider s, HttpPostedFileBase file)
        {
            try
            {
                Slider _slide = new Slider();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _slide.SliderFoto = memoryStream.ToArray();
                }
                _slide.SliderText = s.SliderText;
                _slide.BaslangicTarih = s.BaslangicTarih;
                _slide.BitisTarih = s.BitisTarih;
                ent.Slider.Add(_slide);
                ent.SaveChanges();
                return RedirectToAction("Slider", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult SlideDuzenle(Slider slide, HttpPostedFileBase file)
        {
            try
            {
                var _slideDuzenle = ent.Slider.Where(x => x.ID == slide.ID).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _slideDuzenle.SliderFoto = memoryStream.ToArray();
                }
                _slideDuzenle.SliderText = slide.SliderText;
                _slideDuzenle.BaslangicTarih = slide.BaslangicTarih;
                _slideDuzenle.BitisTarih = slide.BitisTarih;
                ent.SaveChanges();
                return RedirectToAction("Slider", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Bilgi Formu

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult BilgiFormu()
        {
            var bilgiformu = ent.Bilgilendirme.ToList();
            return View(bilgiformu);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult BilgiFormuDuzenle(int BilgiID)
        {
            var _bilgiDuzenle = ent.Bilgilendirme.Where(x => x.ID == BilgiID).FirstOrDefault();
            return View(_bilgiDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult BilgiFormuDuzenle(Bilgilendirme blg)
        {
            try
            {
                var _bilgiDuzenle = ent.Bilgilendirme.Where(x => x.ID == blg.ID).FirstOrDefault();
                _bilgiDuzenle.Cevap = blg.Cevap;
                _bilgiDuzenle.CevapTarihi = DateTime.Now;
                ent.SaveChanges();
                return RedirectToAction("BilgiFormu", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Duyuru

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult Duyurular()
        {
            var duyurular = ent.Duyurular.ToList();
            return View(duyurular);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult DuyuruEkle()
        {
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult DuyuruDuzenle(int DuyuruID)
        {
            var _duyuruDuzenle = ent.Duyurular.Where(x => x.ID == DuyuruID).FirstOrDefault();
            return View(_duyuruDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult DuyuruSil(int DuyuruID)
        {
            try
            {
                ent.Duyurular.Remove(ent.Duyurular.First(d => d.ID == DuyuruID));
                ent.SaveChanges();
                return RedirectToAction("Duyurular", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult DuyuruEkle(Duyurular d)
        {
            try
            {
                Duyurular _duyuru = new Duyurular();
                _duyuru.DuyuruBaslik = d.DuyuruBaslik;
                _duyuru.Duyuru = d.Duyuru;
                _duyuru.Tarih = DateTime.Now;
                ent.Duyurular.Add(_duyuru);
                ent.SaveChanges();
                return RedirectToAction("Duyurular", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult DuyuruDuzenle(Duyurular dyr)
        {
            try
            {
                var _duyuruDuzenle = ent.Duyurular.Where(x => x.ID == dyr.ID).FirstOrDefault();
                _duyuruDuzenle.DuyuruBaslik = dyr.DuyuruBaslik;
                _duyuruDuzenle.Duyuru = dyr.Duyuru;
                ent.SaveChanges();
                return RedirectToAction("Duyurular", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region Öğrenci Görüşleri

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciGorusleri()
        {
            var ogrencigorus = ent.OgrenciGorus.ToList();
            return View(ogrencigorus);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciGorusEkle()
        {
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciGorusDuzenle(int GorusID)
        {
            var _ogrencigorusDuzenle = ent.OgrenciGorus.Where(x => x.ID == GorusID).FirstOrDefault();
            return View(_ogrencigorusDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciGorusSil(int GorusID)
        {
            try
            {
                ent.OgrenciGorus.Remove(ent.OgrenciGorus.First(d => d.ID == GorusID));
                ent.SaveChanges();
                return RedirectToAction("OgrenciGorusleri", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult OgrenciGorusEkle(OgrenciGorus g, HttpPostedFileBase file)
        {
            try
            {
                OgrenciGorus _ogrgorus = new OgrenciGorus();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _ogrgorus.Foto = memoryStream.ToArray();
                }
                _ogrgorus.AdSoyad = g.AdSoyad;
                _ogrgorus.Gorus = g.Gorus;
                _ogrgorus.Tarih = DateTime.Now;
                ent.OgrenciGorus.Add(_ogrgorus);
                ent.SaveChanges();
                return RedirectToAction("OgrenciGorusleri", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult OgrenciGorusDuzenle(OgrenciGorus ogr, HttpPostedFileBase file)
        {
            try
            {
                var _ogrgorusDuzenle = ent.OgrenciGorus.Where(x => x.ID == ogr.ID).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _ogrgorusDuzenle.Foto = memoryStream.ToArray();
                }
                _ogrgorusDuzenle.AdSoyad = ogr.AdSoyad;
                _ogrgorusDuzenle.Gorus = ogr.Gorus;
                ent.SaveChanges();
                return RedirectToAction("OgrenciGorusleri", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Öğretim Elemanı

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgretimElemani()
        {
            var ogretimelemani = ent.OgretimElemani.ToList();
            return View(ogretimelemani);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgretimElemaniEkle()
        {
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgretimElemaniDuzenle(int OgretimElemaniID)
        {
            var _ogretimelemaniDuzenle = ent.OgretimElemani.Where(x => x.OgretimElemanId == OgretimElemaniID).FirstOrDefault();
            return View(_ogretimelemaniDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgretimElemaniSil(int OgretimElemaniID)
        {
            try
            {
                ent.OgretimElemani.Remove(ent.OgretimElemani.First(d => d.OgretimElemanId == OgretimElemaniID));
                ent.SaveChanges();
                return RedirectToAction("OgretimElemani", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult OgretimElemaniEkle(OgretimElemani ogreleman, HttpPostedFileBase file)
        {
            try
            {
                OgretimElemani _ogretimelemani = new OgretimElemani();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _ogretimelemani.Foto = memoryStream.ToArray();
                }
                _ogretimelemani.Ad = ogreleman.Ad;
                _ogretimelemani.Soyad = ogreleman.Soyad;
                _ogretimelemani.Telefon = ogreleman.Telefon;
                _ogretimelemani.Eposta = ogreleman.Eposta;
                _ogretimelemani.Ozgecmis = ogreleman.Ozgecmis;
                ent.OgretimElemani.Add(_ogretimelemani);
                ent.SaveChanges();
                return RedirectToAction("OgretimElemani", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult OgretimElemaniDuzenle(OgretimElemani ogrelemani, HttpPostedFileBase file)
        {
            try
            {
                var _ogretimelemaniDuzenle = ent.OgretimElemani.Where(x => x.OgretimElemanId == ogrelemani.OgretimElemanId).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _ogretimelemaniDuzenle.Foto = memoryStream.ToArray();
                }
                _ogretimelemaniDuzenle.Ad = ogrelemani.Ad;
                _ogretimelemaniDuzenle.Soyad = ogrelemani.Soyad;
                _ogretimelemaniDuzenle.Telefon = ogrelemani.Telefon;
                _ogretimelemaniDuzenle.Eposta = ogrelemani.Eposta;
                _ogretimelemaniDuzenle.Ozgecmis = ogrelemani.Ozgecmis;
                ent.SaveChanges();
                return RedirectToAction("OgretimElemani", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Kurslar

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult Kurslar()
        {
            var kurslar = ent.Kurs.ToList();
            return View(kurslar);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursEkle()
        {
            ViewBag.KursTipi = new SelectList(ent.KursTip.OrderBy(x => x.KursKategoriAd).ToList(), "KursTipId", "KursKategoriAd");
            ViewBag.OgretimElemani = new SelectList(ent.OgretimElemani.OrderBy(x => x.Soyad).ToList(), "OgretimElemanId", "Ad");
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursDuzenle(int KursID)
        {
            ViewBag.KursTipi = new SelectList(ent.KursTip.OrderBy(x => x.KursKategoriAd).ToList(), "KursTipId", "KursKategoriAd");
            ViewBag.OgretimElemani = new SelectList(ent.OgretimElemani.OrderBy(x => x.Soyad).ToList(), "OgretimElemanId", "Ad");
            var _kursDuzenle = ent.Kurs.Where(x => x.KursId == KursID).FirstOrDefault();
            return View(_kursDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursSil(int KursID)
        {
            try
            {
                ent.Kurs.Remove(ent.Kurs.First(d => d.KursId == KursID));
                ent.SaveChanges();
                return RedirectToAction("Kurslar", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult KursEkle(Kurs krs, HttpPostedFileBase file)
        {
            try
            {
                Kurs _kurs = new Kurs();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _kurs.Foto = memoryStream.ToArray();
                }
                _kurs.KursTipId = krs.KursTipId;
                _kurs.KursAd = krs.KursAd;
                _kurs.Aciklama = krs.Aciklama;
                _kurs.OgretimElemanId = krs.OgretimElemanId;
                _kurs.Sure = krs.Sure;
                _kurs.Ucret = krs.Ucret;
                _kurs.BaslangicTarihi = krs.BaslangicTarihi;
                _kurs.BitisTarihi = krs.BitisTarihi;
                _kurs.ToplamSaat = krs.ToplamSaat;
                ent.Kurs.Add(_kurs);
                ent.SaveChanges();
                return RedirectToAction("Kurslar", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult KursDuzenle(Kurs k, HttpPostedFileBase file)
        {
            try
            {
                var _kursDuzenle = ent.Kurs.Where(x => x.KursId == k.KursId).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _kursDuzenle.Foto = memoryStream.ToArray();
                }
                _kursDuzenle.KursTipId = k.KursTipId;
                _kursDuzenle.KursAd = k.KursAd;
                _kursDuzenle.Aciklama = k.Aciklama;
                _kursDuzenle.OgretimElemanId = k.OgretimElemanId;
                _kursDuzenle.Sure = k.Sure;
                _kursDuzenle.Ucret = k.Ucret;
                _kursDuzenle.BaslangicTarihi = k.BaslangicTarihi;
                _kursDuzenle.BitisTarihi = k.BitisTarihi;
                _kursDuzenle.ToplamSaat = k.ToplamSaat;
                ent.SaveChanges();
                return RedirectToAction("Kurslar", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        public ActionResult OgretimElemaniArama(string ad)
        {
            var personel_list = ent.OgretimElemani.Select(x => new { ID = x.OgretimElemanId, AdSoyad = x.Ad + " " + x.Soyad });
            if (ad != "")
            {
                personel_list = personel_list.Where(x => x.AdSoyad.Contains(ad));
            }

            return Json(personel_list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region // Kurs Kayıtları

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursKayitlari()
        {
            var kurskayitlari = ent.KursOgrenci.OrderBy(x => x.Kurs.KursTip.KursKategoriAd).ThenBy(x => x.Kurs.KursAd).ToList();
            return View(kurskayitlari);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursKayitlariEkle()
        {
            ViewBag.Kurs = new SelectList(ent.Kurs.OrderBy(x => x.KursAd).ToList(), "KursId", "KursAd");
            ViewBag.Ogrenci = new SelectList(ent.Ogrenci.OrderBy(x => x.Soyad).ToList(), "OgrenciId", "Ad");
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursKayitlariDuzenle(int KursOgrenciID)
        {
            ViewBag.Kurs = new SelectList(ent.Kurs.OrderBy(x => x.KursAd).ToList(), "KursId", "KursAd");
            ViewBag.Ogrenci = new SelectList(ent.Ogrenci.OrderBy(x => x.Soyad).ToList(), "OgrenciId", "Ad");
            var _kursDuzenle = ent.KursOgrenci.Where(x => x.KursOgenciId == KursOgrenciID).FirstOrDefault();
            return View(_kursDuzenle);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult KursKayitlariSil(int KursOgrenciID)
        {
            try
            {
                ent.KursOgrenci.Remove(ent.KursOgrenci.First(d => d.KursOgenciId == KursOgrenciID));
                ent.SaveChanges();
                return RedirectToAction("KursKayitlari", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult KursKayitlariEkle(KursOgrenci krs)
        {
            try
            {
                KursOgrenci _kurskayit = new KursOgrenci();
                _kurskayit.KursId = krs.KursId;
                _kurskayit.OgrenciId = krs.OgrenciId;
                _kurskayit.Ucret = krs.Ucret;
                _kurskayit.KayitTarihi = DateTime.Now;
                ent.KursOgrenci.Add(_kurskayit);
                ent.SaveChanges();
                return RedirectToAction("KursKayitlari", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult KursKayitlariDuzenle(KursOgrenci k)
        {
            try
            {
                var _kurskayitDuzenle = ent.KursOgrenci.Where(x => x.KursOgenciId == k.KursOgenciId).FirstOrDefault();
                _kurskayitDuzenle.KursId = k.KursId;
                _kurskayitDuzenle.OgrenciId = k.OgrenciId;
                _kurskayitDuzenle.Ucret = k.Ucret;
                ent.SaveChanges();
                return RedirectToAction("KursKayitlari", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion

        #region // Öğrenciler

        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult Ogrenciler()
        {
            var ogrenciler = ent.Ogrenci.ToList();
            return View(ogrenciler);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciEkle()
        {
            return View();
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciDuzenle(int OgrenciID)
        {
            var _ogrenci = ent.Ogrenci.Where(x => x.OgrenciId == OgrenciID).FirstOrDefault();
            return View(_ogrenci);
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        public ActionResult OgrenciSil(int OgrenciID)
        {
            try
            {
                ent.Ogrenci.Remove(ent.Ogrenci.First(d => d.OgrenciId == OgrenciID));
                ent.SaveChanges();
                return RedirectToAction("Ogrenciler", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult OgrenciEkle(Ogrenci ogr)
        {
            try
            {
                Ogrenci _ogrenci = new Ogrenci();
                _ogrenci.Ad = ogr.Ad;
                _ogrenci.Soyad = ogr.Soyad;
                _ogrenci.Telefon = ogr.Telefon;
                _ogrenci.Eposta = ogr.Eposta;
                _ogrenci.DogumTarihi = ogr.DogumTarihi;
                ent.Ogrenci.Add(_ogrenci);
                ent.SaveChanges();
                return RedirectToAction("Ogrenciler", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [EgtMerkezi.Attributes.AdminRoleControl]
        [HttpPost]
        public ActionResult OgrenciDuzenle(Ogrenci ogrenci)
        {
            try
            {
                var _ogrenciDuzenle = ent.Ogrenci.Where(x => x.OgrenciId == ogrenci.OgrenciId).FirstOrDefault();
                _ogrenciDuzenle.Ad = ogrenci.Ad;
                _ogrenciDuzenle.Soyad = ogrenci.Soyad;
                _ogrenciDuzenle.Telefon = ogrenci.Telefon;
                _ogrenciDuzenle.Eposta = ogrenci.Eposta;
                _ogrenciDuzenle.DogumTarihi = ogrenci.DogumTarihi;
                ent.SaveChanges();
                return RedirectToAction("Ogrenciler", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion
    }
}