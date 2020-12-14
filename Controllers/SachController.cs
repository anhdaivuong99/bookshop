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
using CNLTTH_BanSach;

namespace CNLTTH_BanSach.Controllers
{
    public class SachController : ApiController
    {
        private DBCNLTTH_BanSach db = new DBCNLTTH_BanSach();

        // GET: api/Sach
        public IEnumerable<Sach> GetSaches()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Saches.ToList();
        }

        // GET: api/Sach/5
        //[ResponseType(typeof(Sach))]
        //public IHttpActionResult GetSach(string id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    Sach sach = db.Saches.Find(id);
        //    if (sach == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(sach);
        //}
        //[ResponseType(typeof(Sach))]
        //public IHttpActionResult GetSach(string id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var list = db.Saches.FirstOrDefault(x => x.TenSach == id);
        //    if (list == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(list);
        //}
        [ResponseType(typeof(Sach))]
        public IHttpActionResult GetSach(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = (Sach)db.Saches.SqlQuery("SELECT * FROM dbo.Sach s WHERE  s.MaSach LIKE '%" + id + "%' OR s.TenSach LIKE N'%" + id + "%'").FirstOrDefault();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
        // PUT: api/Sach/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSach(string id, Sach sach)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != sach.MaSach)
            //{
            //    return BadRequest();
            //}

            db.Entry(sach).State = EntityState.Modified;
            db.SaveChanges();
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(id))
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

        // POST: api/Sach
        [ResponseType(typeof(Sach))]
        public IHttpActionResult PostSach(Sach sach)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Saches.Add(sach);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SachExists(sach.MaSach))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sach.MaSach }, sach);
        }

        // DELETE: api/Sach/5
        [ResponseType(typeof(Sach))]
        public IHttpActionResult DeleteSach(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }

            db.Saches.Remove(sach);
            db.SaveChanges();

            return Ok(sach);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SachExists(string id)
        {
            return db.Saches.Count(e => e.MaSach == id) > 0;
        }
    }
}