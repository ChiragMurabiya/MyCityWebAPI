using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyCityWepAPI.Models;

namespace MyCityWepAPI.Controllers
{
    public class ShopImagesController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/ShopImages
        public IQueryable<tblShopImage> GettblShopImages()
        {
            return db.tblShopImages;
        }

        // GET: api/ShopImages/5
        [ResponseType(typeof(tblShopImage))]
        public IHttpActionResult GettblShopImage(int id)
        {
            try
            {                
                var data = db.tblShopImages.Where(w => w.ID == id).FirstOrDefault();

                if (data == null)
                {
                    return Ok(new { code = 1, data = "Not found" });
                }
                else
                {
                    tblShopImage model = new tblShopImage();
                    model.ID = data.ID;
                    model.ImageName = data.ImageName;
                    model.ImagePath = data.ImagePath;
                    model.ShopID = data.ShopID;
                    model.Created = System.DateTime.Now;
                    model.CreatedBy = data.CreatedBy;
                    model.Updated = System.DateTime.Now;
                    model.UpdatedBy = data.UpdatedBy;
                    model.Active = Convert.ToBoolean(data.Active);

                    return Ok(new { code = 0, data = model });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/ShopImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblShopImage(tblShopImage tblShopImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.tblShopImages.Where(w => w.ID == tblShopImage.ID).Count();//.FirstOrDefault();
            if (data >= 0)
            {
                tblShopImage ShopImage = new tblShopImage();
                ShopImage.ID = tblShopImage.ID;
                ShopImage.ShopID = tblShopImage.ShopID;
                ShopImage.ImageName = tblShopImage.ImageName;
                ShopImage.ImagePath = tblShopImage.ImagePath;
                ShopImage.Created = System.DateTime.Now;
                ShopImage.CreatedBy = tblShopImage.CreatedBy;
                ShopImage.Updated = System.DateTime.Now;
                ShopImage.UpdatedBy = tblShopImage.UpdatedBy;
                ShopImage.Active = Convert.ToBoolean(tblShopImage.Active);

                db.Entry(ShopImage).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {                    
                    db.SaveChanges();
                }

                return Ok(new { code = 0, data = "Shop Image updated successfully." });
            }
            else
            {
                return Ok(new { code = 1, data = "Shop Image not found." });
            }
        }

        // POST: api/ShopImages
        [ResponseType(typeof(tblShopImage))]
        public IHttpActionResult PosttblShopImage(tblShopImage tblShopImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblShopImages.Add(tblShopImage);
            db.SaveChanges();

            return Ok(new { code = 0, data = "Shop Image added successfully." });
        }

        // DELETE: api/ShopImages/5
        [ResponseType(typeof(tblShopImage))]
        public IHttpActionResult DeletetblShopImage(int id)
        {
            tblShopImage tblShopImage = db.tblShopImages.Find(id);
            if (tblShopImage == null)
            {
                return NotFound();
            }

            db.tblShopImages.Remove(tblShopImage);
            db.SaveChanges();

            return Ok(new { code = 0, message = "Record deleted successfully" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblShopImageExists(int id)
        {
            return db.tblShopImages.Count(e => e.ID == id) > 0;
        }
    }
}