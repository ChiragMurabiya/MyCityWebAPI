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
    public class ShopProductImagesController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/ShopProductImages
        public IQueryable<tblShopProductImage> GettblShopProductImages()
        {
            return db.tblShopProductImages;
        }

        // GET: api/ShopProductImages/5
        [ResponseType(typeof(tblShopProductImage))]
        public IHttpActionResult GettblShopProductImage(int id)
        {
            tblShopProductImage tblShopProductImage = db.tblShopProductImages.Find(id);
            if (tblShopProductImage == null)
            {
                return NotFound();
            }

            return Ok(tblShopProductImage);
        }

        [ResponseType(typeof(tblShop))]
        [System.Web.Http.Route("api/GetShopProduct")]
        public IHttpActionResult GetShopProductsByShopID(int ShopID)
        {
            var ShopProductData = db.tblShopProducts.Where(w=>w.ShopID == ShopID)
                                   .Select(s => new ShopProductAndImage
                                   {
                                       ID = s.ID,
                                       Name = s.Name,
                                       Description = s.Description,
                                       Discount = s.Discount,
                                       Price = s.Price,
                                       Active = s.Active
                                   }
                                            ).ToList();

            return Ok(ShopProductData);
        }

        [ResponseType(typeof(tblShop))]
        [System.Web.Http.Route("api/GetShopProductImageByShopProductID")]
        public IHttpActionResult GetShopProductImagesByShopID(int ShopProductID)
        {
            var ShopProductImageData = db.tblShopProductImages.Where(w => w.ShopProductID == ShopProductID)
                                   .Select(s => new ShopProductAndImage
                                   {
                                       ID = s.ID,
                                       ImageName = s.ImageName,
                                       ImageActive = s.Active
                                   }
                                            ).ToList();

            return Ok(ShopProductImageData);
        }

        // PUT: api/ShopProductImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblShopProductImage(int id, tblShopProductImage tblShopProductImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblShopProductImage.ID)
            {
                return BadRequest();
            }

            db.Entry(tblShopProductImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblShopProductImageExists(id))
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

        // POST: api/ShopProductImages
        [ResponseType(typeof(tblShopProductImage))]
        public IHttpActionResult PosttblShopProductImage(tblShopProductImage tblShopProductImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblShopProductImages.Add(tblShopProductImage);
            db.SaveChanges();

            return Ok(new { code = 0, data = "Image added successfully." });
        }

        [HttpPost]
        [System.Web.Http.Route("api/AddShopProductImages")]
        public IEnumerable<ShopProductAndImage> PostProductImages([FromBody]IEnumerable<ShopProductAndImage> pList)
        {
            return null;
        }

        // DELETE: api/ShopProductImages/5
        [ResponseType(typeof(tblShopProductImage))]
        public IHttpActionResult DeletetblShopProductImage(int id)
        {
            tblShopProductImage tblShopProductImage = db.tblShopProductImages.Find(id);
            if (tblShopProductImage == null)
            {
                return NotFound();
            }

            db.tblShopProductImages.Remove(tblShopProductImage);
            db.SaveChanges();

            return Ok(tblShopProductImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblShopProductImageExists(int id)
        {
            return db.tblShopProductImages.Count(e => e.ID == id) > 0;
        }
    }
}