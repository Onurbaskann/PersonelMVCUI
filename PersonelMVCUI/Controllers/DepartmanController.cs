using PersonelMVCUI.Models.EntitiyFramework;
using PersonelMVCUI.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    
    public class DepartmanController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        
        // GET: Departman
        public ActionResult Index()
        {            
            var model = db.DepartmanTb.ToList();            
            return View(model);
        }

        [Authorize(Roles = "A,U")]
        [Route("Yeni")]
        [HttpGet]
        public ActionResult Yeni()
        {
                                     //"new DepartmanTb()" Sever Side Validation için kullandık 
            return View("DepartmanForm",new DepartmanTb());//Geri döndürülecek view'ın adı yazılır.Boş bırakılırsa default olarak "Yeni" view'ni arar. ("Yeni"=>"DepartmanForm" olarak değiştiği için boş bırakıldığında "Yeni" view'nı bulamaz.) 
        }
        
        [Authorize(Roles = ",A,U")]
        [Route("Yeni")]
        [ValidateAntiForgeryToken] //cross site request forgery (csrf) saldırıları için )      
        public ActionResult Kaydet(DepartmanTb departman)
        {
            if(!ModelState.IsValid) //Sever Side Validation
            {
                return View("DepartmanForm");
            }

            MesajViewModels msjModel = new MesajViewModels();   //MesajViewModel'den obje yarattık
            if (departman.DepartmanID == 0)
            {
                msjModel.Mesaj = departman.DepartmanAd + " başarıyla eklendi."; //Göstermek istediğimiz mesajı girdik.
                db.DepartmanTb.Add(departman);
            }
            else
            {
                var guncellenecekDepartman = db.DepartmanTb.Find(departman.DepartmanID);
                if (guncellenecekDepartman == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    guncellenecekDepartman.DepartmanAd = departman.DepartmanAd;
                    msjModel.Mesaj = departman.DepartmanAd + " başarıyla güncellendi.";
                }
            }
                        
            db.SaveChanges();
            msjModel.Status = true;

            return View("_Mesaj",msjModel); //"_Mesaj" view'ne "msjModel" modelini yolluyoruz.
        }
        [Authorize(Roles = ",A,U")]
        
        public ActionResult Guncelle(int id)//Başka bir view kullanacağımız için "Guncelle" için view yaratmamıza gerek yok
        {
            var model = db.DepartmanTb.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View("DepartmanForm",model);//Gidilecek view yazılıyor. 
        }
        [Authorize(Roles = ",A,U")]
        
        public ActionResult Sil(int id)
        {
            var silinecekDepartman=db.DepartmanTb.Find(id);
            if (silinecekDepartman == null)
                return HttpNotFound();

            db.DepartmanTb.Remove(silinecekDepartman);
            db.SaveChanges();

            return RedirectToAction("Index","Departman");
        }
    }
}