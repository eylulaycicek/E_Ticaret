using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret_2023.Controllers
{
    [Authorize(Roles = "admin")]
    public class UrunlerController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kategoriler);
            return View(urunler.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urunler urunler, HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();

                if (urunResim != null && urunResim.ContentLength > 0)
                {
                    string dosya = Server.MapPath("~/Resim/") + urunler.UrunId + ".jpg";
                    urunResim.SaveAs(dosya);
                }
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", urunler.KategoriId);
            return View(urunler);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", urunler.KategoriId);
            return View(urunler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Urunler urunler, HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();

                if (urunResim != null && urunResim.ContentLength > 0)
                {
                    string dosya = Server.MapPath("~/Resim/") + urunler.UrunId + ".jpg";
                    urunResim.SaveAs(dosya);
                }


                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", urunler.KategoriId);
            return View(urunler);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();

            string dosya = Server.MapPath("~/Resim/") + urunler.UrunId + ".jpg";
            FileInfo file = new FileInfo(dosya);
            if (file.Exists)
            {
                file.Delete();
            }

            return RedirectToAction("Index");
        }

    }
}