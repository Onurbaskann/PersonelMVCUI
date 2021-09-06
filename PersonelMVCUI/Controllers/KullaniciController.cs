using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntitiyFramework;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    
    public class KullaniciController : Controller
    {
        private PersonelDbEntities1 db = new PersonelDbEntities1();
        
        [Authorize(Roles = "A")]

        // GET: Kullanici
        public ActionResult Index()
        {
            return View(db.KullaniciTb.ToList());
        }
        [Authorize]
        // GET: Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KullaniciTb kullaniciTb = db.KullaniciTb.Find(id);
            if (kullaniciTb == null)
            {
                return HttpNotFound();
            }
            return View(kullaniciTb);
        }
        MesajViewModels msjModel = new MesajViewModels();
        [Authorize]
        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Ad,Sifre,Rol")] KullaniciTb kullaniciTb)
        {
            if (ModelState.IsValid)
            {
                db.KullaniciTb.Add(kullaniciTb);
                msjModel.Mesaj = "Kullanıcı başarıyla eklendi";
                db.SaveChanges();
                msjModel.Status = true;
                return View("_Mesaj", msjModel);
            }
            
            return View(kullaniciTb);
        }
        [Authorize]
        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KullaniciTb kullaniciTb = db.KullaniciTb.Find(id);
            if (kullaniciTb == null)
            {
                return HttpNotFound();
            }
            return View(kullaniciTb);
        }

        // POST: Kullanici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ad,Sifre,Rol")] KullaniciTb kullaniciTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullaniciTb).State = EntityState.Modified;
                msjModel.Mesaj = "Kullanıcı başarıyla güncellendi";
                db.SaveChanges();
                msjModel.Status = true;
                return View("_Mesaj",msjModel);
            }
            return View(kullaniciTb);
        }
        [Authorize]
        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KullaniciTb kullaniciTb = db.KullaniciTb.Find(id);
            if (kullaniciTb == null)
            {
                return HttpNotFound();
            }
            return View(kullaniciTb);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KullaniciTb kullaniciTb = db.KullaniciTb.Find(id);
            db.KullaniciTb.Remove(kullaniciTb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
