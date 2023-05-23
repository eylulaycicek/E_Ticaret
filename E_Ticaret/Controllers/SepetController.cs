using E_Ticaret.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        E_TicaretEntities db = new E_TicaretEntities ();
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var sepet = db.Sepet.Where(x => x.KullaniciId == userId);
            return View(sepet.ToList());
        }
        public ActionResult SepeteEkle(int UrunId, int adet)
        {
            string userId = User.Identity.GetUserId();
            Urunler urun = db.Urunler.Find(UrunId);

            Sepet sepettekiurun = db.Sepet.FirstOrDefault(x => x.UrunId == UrunId && x.KullaniciId == userId);

            if (sepettekiurun == null)
            {
                Sepet sepet = new Sepet()
                {
                    KullaniciId = userId,
                    UrunId = UrunId,
                    Adet = adet,
                    ToplamTutar = adet * urun.UrunFiyati
                };
                db.Sepet.Add(sepet);
            }
            else
            {
                sepettekiurun.Adet += adet;
                sepettekiurun.ToplamTutar = sepettekiurun.Adet * urun.UrunFiyati;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SepetGuncelle(int id, int adet)
        {
            Sepet sepet = db.Sepet.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }

            Urunler urun = db.Urunler.Find(sepet.UrunId);

            sepet.Adet = adet;
            sepet.ToplamTutar = sepet.Adet * urun.UrunFiyati;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SepetSil(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            if (sepet != null)
            {
                db.Sepet.Remove(sepet);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}