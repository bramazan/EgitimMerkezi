using EgtMerkezi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EgtMerkezi.Controllers
{
    public class HomeController : Controller
    {
        EgitimMerkeziEntities ent = new EgitimMerkeziEntities();
        public ActionResult Index()
        {
            ViewBag.TercihEdilenEgitimler = new SelectList(ent.Kurs.OrderBy(x => x.KursAd).ToList(), "KursAd", "KursAd");
            AnaSayfaDTO obj = new AnaSayfaDTO();
            obj.slider = ent.Slider.Where(x => (x.BaslangicTarih <= DateTime.Now && x.BitisTarih > DateTime.Now)).ToList();
            obj.bilgiformu = new Bilgilendirme();
            obj.duyuru = ent.Duyurular.OrderByDescending(x => x.ID).Take(3).ToList();
            obj.gorus = ent.OgrenciGorus.OrderByDescending(x => x.ID).Take(3).ToList();
            return View("Index", obj);
        }
        public ActionResult GoogleSearch()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult RegisterMessage()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ExamNotes()
        {
            return View();
        }
        public ActionResult Documents()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BilgiIstekFormuGonder(AnaSayfaDTO blgform)
        {
            try
            {
                Bilgilendirme _bilgiform = new Bilgilendirme();
                _bilgiform.AdSoyad = blgform.bilgiformu.AdSoyad;
                _bilgiform.Telefon = blgform.bilgiformu.Telefon;
                _bilgiform.Eposta = blgform.bilgiformu.Eposta;
                _bilgiform.Kurslar = blgform.bilgiformu.Kurslar;
                _bilgiform.IstekTarihi = DateTime.Now;
                ent.Bilgilendirme.Add(_bilgiform);
                ent.SaveChanges();
                ViewBag.Mesaj = "Form Başarıyla gönderilmiştir.";
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        public ActionResult Courses(int CategoryID)
        {
            AnaSayfaDTO dto = new AnaSayfaDTO();
            dto.kurs = ent.Kurs.Where(x => x.KursTipId == CategoryID).OrderBy(x => x.KursAd).ToList();
            dto.kursdetay = ent.KursTip.FirstOrDefault(x => x.KursTipId == CategoryID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult Contact(Oneri iletisimform)
        {
            try
            {
                Oneri _iletisimform = new Oneri();
                _iletisimform.AdSoyad = iletisimform.AdSoyad;
                _iletisimform.Telefon = iletisimform.Telefon;
                _iletisimform.Eposta = iletisimform.Eposta;
                _iletisimform.Mesaj = iletisimform.Mesaj;
                _iletisimform.Tarih = DateTime.Now;
                ent.Oneri.Add(_iletisimform);
                ent.SaveChanges();
                TempData["Mesaj"] = "Form Başarıyla gönderilmiştir.";
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
        public ActionResult UyeEkle(RegisterViewModel RegisterModel)
        {
            var ent = new EgitimMerkeziEntities();
            Ogrenci _kursogrenci = new Ogrenci();
            string aktivasyonkodu = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper();
            _kursogrenci.Ad = RegisterModel.Ad;
            _kursogrenci.Soyad = RegisterModel.Soyad;
            _kursogrenci.Telefon = RegisterModel.Telefon;
            _kursogrenci.Eposta = RegisterModel.Email;
            _kursogrenci.Sifre = FormsAuthentication.HashPasswordForStoringInConfigFile(RegisterModel.Password, "md5");
            _kursogrenci.Aktivasyon = aktivasyonkodu;
            _kursogrenci.Onay = false;
            _kursogrenci.Role = "Kullanici";
            _kursogrenci.KayitTarihi = DateTime.Now;
            ent.Ogrenci.Add(_kursogrenci);
            ent.SaveChanges();
            string mailIcerik = "EĞİTİM MERKEZİ AİLESİNE HOSGELDİNIZ<br>www.egitimmerkezi.com a kayıt olduğunuz için teşekkür ederiz. <br> Üyeliğinizi onaylamak için doğrulamak için aşağıdaki linke tıklayınız.<br> ";
            //mailIcerik += "<a href=\"http://egitimmerkezi.sakarya.edu.tr/Home/Aktivasyon?Eposta=" + RegisterModel.Email + "&AktivasyonKodu=" + aktivasyonkodu + "\" style=\"text-decoration:underline; color:#3ba5d8;\">Üyeliğimi Aktifleştir</a>";
            mailIcerik += "<a href=\"http://egitimmerkezi.sakarya.edu.tr/Home/Aktivasyon?Eposta=" + RegisterModel.Email + "&AktivasyonKodu=" + aktivasyonkodu + "\" style=\"text-decoration:underline; color:#3ba5d8;\">Üyeliğimi Aktifleştir</a>";
            MailGonder(RegisterModel.Email, "Eğitim Merkezi ~ Üyelik Aktivasyon", mailIcerik);
            return View("RegisterMessage");
        }
        public ActionResult UyeGiris(RegisterViewModel LoginModel)
        {
            if (ModelState.IsValid)
            {
                var ent = new EgitimMerkeziEntities();
                string sifre = FormsAuthentication.HashPasswordForStoringInConfigFile(LoginModel.Password, "md5");
                var uye = ent.Ogrenci.Where(x => x.Eposta == LoginModel.Email && x.Sifre == sifre && x.Onay == true).FirstOrDefault();
                if (uye != null)
                {
                    Session["eposta"] = LoginModel.Email;
                    Session["role"] = uye.Role;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Mesaj"] = "Kullanıcı Adı veya şifre yanlış, eposta aktivasyon maili onaylanmamıştır";
                }
            }
            return View("Login");
        }
        public static void MailGonder(string kime, string baslik, string mesajIcerik)
        {
            string server = "smtp.gmail.com";
            string kadi = "umit@sakarya.edu.tr";
            string sifre = "xxxxxxxxxx";
            int port = 587;
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Port = port; //Smtp Portu (Sunucuya Göre Değişir)
            smtpclient.Host = server; //Smtp Hostu (Gmail smtp adresi bu şekilde)
            smtpclient.EnableSsl = true; //Sunucunun SSL kullanıp kullanmadıgı
            smtpclient.Credentials = new NetworkCredential(kadi, sifre); //Gmail Adresiniz ve Şifreniz
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(kadi, "Eğitim Merkezi"); //Gidecek Mail Adresi ve Görünüm Adınız
            mail.To.Add(kime); //Kime Göndereceğiniz
            mail.Subject = baslik; //Emailin Konusu
            mail.Body = mesajIcerik; //Mesaj İçeriği
            mail.IsBodyHtml = true; //Mesajınızın Gövdesinde HTML destegininin olup olmadıgı
            smtpclient.Send(mail);
        }
        public ActionResult Aktivasyon(string eposta, string aktivasyonkodu)
        {
            var ent = new EgitimMerkeziEntities();
            var uye = ent.Ogrenci.Where(x => x.Eposta == eposta && x.Aktivasyon == aktivasyonkodu).FirstOrDefault();
            if (uye != null)
            {
                uye.Onay = true;
                ent.SaveChanges();
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public static string RoleGetir(string eposta)
        {
            var ent = new EgitimMerkeziEntities();
            var uye_rol = ent.Ogrenci.FirstOrDefault(x => x.Eposta == eposta).Role;
            return uye_rol;
        }
        public static string OgrenciBilgiGetir(string eposta)
        {
            var ent = new EgitimMerkeziEntities();
            var ogrenci_bilgi =ent.Ogrenci.FirstOrDefault(x => x.Eposta == eposta);
            string adsoyad = ogrenci_bilgi.Ad + " " + ogrenci_bilgi.Soyad;
            return adsoyad;
        }
    }
    public class AnaSayfaDTO
    {
        public List<Slider> slider { get; set; }
        public List<Duyurular> duyuru { get; set; }
        public List<OgrenciGorus> gorus { get; set; }
        public Bilgilendirme bilgiformu { get; set; }
        public List<Kurs> kurs { get; set; }
        public KursTip kursdetay { get; set; }
    }
}