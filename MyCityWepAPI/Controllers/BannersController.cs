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
using MyCityWepAPI.Models;

namespace MyCityWepAPI.Controllers
{
    public class BannersController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/Banners
        public IQueryable<tblBanner> GettblBanners()
        {
            return db.tblBanners;
        }

        // GET: api/Banners/5
        [ResponseType(typeof(tblBanner))]
        public IHttpActionResult GettblBanner(int id)
        {
            tblBanner tblBanner = db.tblBanners.Find(id);
            if (tblBanner == null)
            {
                return NotFound();
            }

            return Ok(tblBanner);
        }

        // PUT: api/Banners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblBanner(int id, tblBanner tblBanner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (tblBanner == null)
                {
                    throw new ArgumentNullException("tblBanner");
                }

                var data = db.tblCategories.Where(w => w.ID == tblBanner.ID).Count();//.FirstOrDefault();
                if (data >= 0)
                {
                    tblBanner banner = new tblBanner();
                    banner.ID = tblBanner.ID;
                    banner.ImageName = tblBanner.ImageName;
                    banner.ImagePath = tblBanner.ImagePath;
                    banner.Created = System.DateTime.Now;
                    banner.CreatedBy = tblBanner.CreatedBy;
                    banner.Updated = System.DateTime.Now;
                    banner.UpdatedBy = tblBanner.UpdatedBy;
                    banner.Active = Convert.ToBoolean(tblBanner.Active);

                    db.Entry(banner).State = EntityState.Modified;
                    db.SaveChanges();

                    return Ok(new { code = 0, data = "Banner updated successfully." });
                }
                else
                {
                    return Ok(new { code = 1, data = "Banner not found." });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Banners
        [ResponseType(typeof(tblBanner))]
        public IHttpActionResult PosttblBanner(tblBanner tblBanner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.tblBanners.Add(tblBanner);
                db.SaveChanges();

                return Ok(new { code = 0, data = "Banner added successfully." });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Banners/5
        [ResponseType(typeof(tblBanner))]
        public IHttpActionResult DeletetblBanner(int id)
        {
            tblBanner tblBanner = new tblBanner();

            tblBanner = db.tblBanners.Find(id);
            db.tblBanners.Remove(tblBanner);
            db.SaveChanges();

            return Ok(new { code = 0, message = "Banner deleted successfully!" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblBannerExists(int id)
        {
            return db.tblBanners.Count(e => e.ID == id) > 0;
        }
    }
}