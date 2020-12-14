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
    public class KhachHangController : ApiController
    {
        private DBCNLTTH_BanSach db = new DBCNLTTH_BanSach();

        // GET: api/KhachHang
        public IEnumerable<KhachHang> GetSaches()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.KhachHangs.ToList();
        }

        // GET: api/KhachHang/5
        [ResponseType(typeof(KhachHang))]
        //public IHttpActionResult GetKhachHang(string id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    KhachHang khachHang = db.KhachHangs.Find(id);
        //    if (khachHang == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(khachHang);
        //}

        [ResponseType(typeof(KhachHang))]
        public IHttpActionResult GetKhachHang(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = (KhachHang)db.KhachHangs.SqlQuery("SELECT * FROM dbo.KhachHang kh WHERE  kh.MaKhachHang LIKE '%" + id + "%' OR kh.TenTaiKhoan LIKE N'%" + id + "%' OR kh.TenKhachHhang LIKE N'%" + id + "%'").FirstOrDefault();
            if (list == null)
            {
                return NotFound();
            }
            
            return Ok(list);
        }

        // PUT: api/KhachHang/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKhachHang(string id, KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khachHang.MaKhachHang)
            {
                return BadRequest();
            }

            db.Entry(khachHang).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHang
        [ResponseType(typeof(KhachHang))]
        public IHttpActionResult PostKhachHang(KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KhachHangs.Add(khachHang);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KhachHangExists(khachHang.MaKhachHang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = khachHang.MaKhachHang }, khachHang);
        }

        // DELETE: api/KhachHang/5
        [ResponseType(typeof(KhachHang))]
        public IHttpActionResult DeleteKhachHang(string id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();

            return Ok(khachHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KhachHangExists(string id)
        {
            return db.KhachHangs.Count(e => e.MaKhachHang == id) > 0;
        }
    }
}