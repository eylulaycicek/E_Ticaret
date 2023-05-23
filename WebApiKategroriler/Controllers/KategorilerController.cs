using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiKategroriler.Models;

namespace WebApiKategroriler.Controllers
{
    public class KategorilerController : ApiController
    {
        E_TicaretEntities db=new E_TicaretEntities();
        // GET: Kategoriler
        public List<Kategori> Get()
        {
            List<Kategoriler> liste = db.Kategoriler.ToList();
            List<Kategori> kategoriler = new List<Kategori>();
            //foreach(var item in liste)
            //{
            //    kategoriler.Add(new Kategori() { KategoriAdi=item.KategoriAdi,KategoriId=item.KategoriId });
            //}

            kategoriler = (from x in db.Kategoriler
                          select new Kategori { KategoriId = x.KategoriId, KategoriAdi = x.KategoriAdi }).ToList();
            return kategoriler;

        }

        public Kategori Get(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            Kategori kategori = new Kategori() { KategoriId = kategoriler.KategoriId, KategoriAdi = kategoriler.KategoriAdi };
            return kategori;
        }
    }
}