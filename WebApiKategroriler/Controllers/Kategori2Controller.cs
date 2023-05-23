using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiKategroriler.Models;

namespace WebApiKategroriler.Controllers
{
    public class Kategori2Controller : ApiController
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: api/Kategori2
        public IQueryable<Kategori> GetKategoris()
        {
            return db.Kategoris;
        }

        // GET: api/Kategori2/5
        [ResponseType(typeof(Kategori))]
        public IHttpActionResult GetKategori(int id)
        {
            Kategori kategori = db.Kategoris.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }

            return Ok(kategori);
        }

        // PUT: api/Kategori2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKategori(int id, Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kategori.KategoriId)
            {
                return BadRequest();
            }

            db.Entry(kategori).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategoriExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kategori2
        [ResponseType(typeof(Kategori))]
        public IHttpActionResult PostKategori(Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kategoris.Add(kategori);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kategori.KategoriId }, kategori);
        }

        // DELETE: api/Kategori2/5
        [ResponseType(typeof(Kategori))]
        public IHttpActionResult DeleteKategori(int id)
        {
            Kategori kategori = db.Kategoris.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }

            db.Kategoris.Remove(kategori);
            db.SaveChanges();

            return Ok(kategori);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KategoriExists(int id)
        {
            return db.Kategoris.Count(e => e.KategoriId == id) > 0;
        }
    }
}