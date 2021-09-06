using PersonelMVCUI.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMVCUI.Controllers
{
    
    public class SecurityController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        // GET: Security
        public ActionResult Login()            
        {     
            return View();
        }
        [HttpPost]
        public ActionResult Login(KullaniciTb kullanici)
        {
            var kullanıcıInDb = db.KullaniciTb.FirstOrDefault(x=>x.Ad==kullanici.Ad && x.Sifre==kullanici.Sifre); 
            if (kullanıcıInDb!=null)
            {
                FormsAuthentication.SetAuthCookie(kullanıcıInDb.Ad, false); // Kullanıcının kimliğini doğrulamış (Authentication) oldu.  
                return RedirectToAction("Index", "Departman");
            }
            else
            {
                ViewBag.Mesaj="Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}