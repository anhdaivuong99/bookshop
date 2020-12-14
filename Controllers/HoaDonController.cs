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
    public class HoaDonController : ApiController
    {
        private DBCNLTTH_BanSach db = new DBCNLTTH_BanSach();

        // GET: api/HoaDon
        public IQueryable<HoaDon> GetHoaDons()
        {
            return db.HoaDons;
        }

        // GET: api/HoaDon/5
        //[ResponseType(typeof(HoaDon))]
        //public IHttpActionResult GetHoaDon(string id)
        //{
        //    HoaDon hoaDon = db.HoaDons.Find(id);
        //    if (hoaDon == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(hoaDon);
        //}

        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult GetHoaDon(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.HoaDons.SqlQuery("SELECT * FROM dbo.HoaDon hd, dbo.KhachHang kh WHERE kh.MaKhachHang = hd.MaKhachHang AND kh.MaKhachHang LIKE N'%"+ id +"%'").ToList();
            if (list == null)
            {
                return NotFound();
            }
            IEnumerable<HoaDon> l = new List<HoaDon>();
            l = list;
            return Ok(l);
        }

        // PUT: api/HoaDon/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHoaDon(string id, HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hoaDon.MaHoaDon)
            {
                return BadRequest();
            }

            db.Entry(hoaDon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonExists(id))
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

        // POST: api/HoaDon
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult PostHoaDon(HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HoaDons.Add(hoaDon);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HoaDonExists(hoaDon.MaHoaDon))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hoaDon.MaHoaDon }, hoaDon);
        }

        // DELETE: api/HoaDon/5
        [ResponseType(typeof(HoaDon))]
        public IHttpActionResult DeleteHoaDon(string id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();

            return Ok(hoaDon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HoaDonExists(string id)
        {
            return db.HoaDons.Count(e => e.MaHoaDon == id) > 0;
        }
    }
}