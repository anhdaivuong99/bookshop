using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using CNLTTH_BanSach;
namespace CNLTTH_BanSach.Controllers
{
    public class LoginController : ApiController
    {
        private DBCNLTTH_BanSach db = new DBCNLTTH_BanSach();
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult GetSach(string id)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var list = (NhanVien)db.NhanViens.SqlQuery("SELECT * FROM dbo.NhanVien nv WHERE  nv.TenTaiKhoan LIKE '%" + id + "%'").FirstOrDefault();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
        [HttpPost]
        public int Login([System.Web.Http.FromBody] NhanVien nv)
        {
            int ret = 0;
            DBCNLTTH_BanSach db = new DBCNLTTH_BanSach();
            var data = db.NhanViens.Where(x => x.TenTaiKhoan == nv.TenTaiKhoan.Trim() && x.MatKhau == nv.MatKhau).FirstOrDefault();
            if (data == null)
            {
                ret = 0;
            }
            else
            {
                ret = 1;
            }
            return ret;
        }
    }
}
