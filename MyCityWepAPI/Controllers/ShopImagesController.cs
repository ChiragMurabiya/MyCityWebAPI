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
            tblShopImage tblShopImage = db.tblShopImages.Find(id);
            if (tblShopImage == null)
            {
                return NotFound();
            }

            return Ok(tblShopImage);
        }

        // PUT: api/ShopImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblShopImage(int id, tblShopImage tblShopImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblShopImage.ID)
            {
                return BadRequest();
            }

            db.Entry(tblShopImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblShopImageExists(id))
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

            return CreatedAtRoute("DefaultApi", new { id = tblShopImage.ID }, tblShopImage);
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

            return Ok(tblShopImage);
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