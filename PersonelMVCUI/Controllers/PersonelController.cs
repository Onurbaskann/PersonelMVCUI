using PersonelMVCUI.Models.EntitiyFramework;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;//"Include"  metodunun namespace'i
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    
    public class PersonelController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();        
       
        //[OutputCache(Duration =zaman)]=>Değişikliğin uzun süre olmadığı veriler için kullanıllır.Belirtilen süre boyunca verileri cache'deki eski verileri getirir.Sonra güncel veriyi getirir.
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.PersonelTb.Include(x => x.DepartmanTb).ToList();//Eager Loading yaptığımız için "Include(x => x.DepartmanTb)" kodunu ekledik.Bu kod "DepartmanTb" tablosunu "PersonelTb" tablosu ile birleştiriyor(join)."Lazy Loading" false yaptık.
            return View(model);
        }
        [Authorize] 
        [Route("Personel/Yeni")]
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.DepartmanTb.ToList(),  //"DepartmanTb" tablosunu list şeklinde "PersonelFormViewModel"deki "Departmanlar" properties'ne gönderiyor.
                Personel = new PersonelTb()
            };
            return View("PersonelForm",model);
        }
        [Authorize(Roles = "A,U")]
        
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(PersonelTb personel)
        {
            if (!ModelState.IsValid)    //Sever Side Validation
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.DepartmanTb.ToList(),
                    Personel = personel     //Fomda doldurduğumuz bilgilerin aynı şekilde geri döndürdük.
                };
                return View("PersonelForm",model);
            }

            MesajViewModels msjmodel = new MesajViewModels();
            if (personel.PersonelID==0) {   //Ekleme işlemi
                                
                db.PersonelTb.Add(personel);
                msjmodel.Mesaj = "Personel Başarıyla eklendi";
            }
            else{   //Güncelleme işlemei
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified; //Bu kod databaseden tek tek bulmak için yazacağımız kodlar yerine tek satırda hepsini bulmamızı sağlıyor. 
                msjmodel.Mesaj = "Personel Başarıyla güncellendi.";
            }
            db.SaveChanges();
            msjmodel.Status = true;
            return View("_Mesaj",msjmodel);
        }
        [Authorize(Roles = "A,U")]
        
        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.DepartmanTb.ToList(),
                Personel=db.PersonelTb.Find(id)
            };
            return View("PersonelForm",model);
        }
        [Authorize(Roles = "A,U")]
        
        public ActionResult Sil(int id)
        {
            var prs = db.PersonelTb.Find(id);
            
            if (prs == null)
                return HttpNotFound();

            db.PersonelTb.Remove(prs);
            db.SaveChanges();
            return RedirectToAction("index", "Personel");
        }
       
        public ActionResult Listele(int id)
        {
            var model = db.PersonelTb.Where(m => m.DepartmanID == id).ToList();

            return View(model);
        }

        public int ToplamPersonel (){   //RenderAction'da göstermek istediğimiz olayı yazıyoruz.
            
            return db.PersonelTb.Count();
        }
    }
}