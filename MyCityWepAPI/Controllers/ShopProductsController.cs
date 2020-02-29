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
    public class ShopProductsController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/ShopProducts
        public IQueryable<tblShopProduct> GettblShopProducts()
        {
            return db.tblShopProducts;
        }

        // GET: api/ShopProducts/5
        [ResponseType(typeof(tblShopProduct))]
        public IHttpActionResult GettblShopProduct(int id)
        {
            try
            {
                //tblShopProduct tblShopProduct = db.tblShopProducts.Find(id);
                //if (tblShopProduct == null)
                //{
                //    return NotFound();
                //}
                //return Ok(tblShopProduct);
                var data = db.tblShopProducts.Where(w => w.ID == id).FirstOrDefault();

                if (data == null)
                {
                    return Ok(new { code = 1, data = "Not found" });
                }
                else
                {
                    tblShopProduct model = new tblShopProduct();
                    model.ID = data.ID;
                    model.ShopID = data.ShopID;
                    model.Name = data.Name;
                    model.Description = data.Description;
                    model.Discount = data.Discount;
                    model.Price = data.Price;
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

        // GET: api/ShopProducts/5
        [ResponseType(typeof(tblShopProduct))]
        public IHttpActionResult GettblShopProductByshopID(int ShopID)
        {
            //tblShopProduct tblShopProduct = db.tblShopProducts.Find(ShopId);
            var data = db.tblShopProducts.Where(w => w.ShopID == ShopID).ToList();



            return Ok(new { code = 0, data = data });
        }

        // PUT: api/ShopProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblShopProduct(tblShopProduct tblShopProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.tblShops.Where(w => w.ID == tblShopProduct.ID).Count();
            if (data >= 0)
            {
                tblShopProduct ShopProduct = new tblShopProduct();
                ShopProduct.ID = tblShopProduct.ID;
                ShopProduct.ShopID = tblShopProduct.ShopID;
                ShopProduct.Name = tblShopProduct.Name;
                ShopProduct.Description = tblShopProduct.Description;
                ShopProduct.Discount = tblShopProduct.Discount;
                ShopProduct.Price = tblShopProduct.Price;
                ShopProduct.IsDisplayOnHomePage = tblShopProduct.IsDisplayOnHomePage;
                ShopProduct.DisplayOrder = tblShopProduct.DisplayOrder;
                ShopProduct.Created = System.DateTime.Now;
                ShopProduct.CreatedBy = tblShopProduct.CreatedBy;
                ShopProduct.Updated = System.DateTime.Now;
                ShopProduct.UpdatedBy = tblShopProduct.UpdatedBy;
                ShopProduct.Active = Convert.ToBoolean(tblShopProduct.Active);

                db.Entry(ShopProduct).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new { code = 0, data = "Shop Product updated successfully.", id = tblShopProduct.ID });
            }
            else
            {
                return Ok(new { code = 1, data = "Shop Product not found." });
            }
        }

        // POST: api/ShopProducts
        [ResponseType(typeof(tblShopProduct))]
        public IHttpActionResult PosttblShopProduct(tblShopProduct tblShopProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblShopProducts.Add(tblShopProduct);
            db.SaveChanges();

            return Ok(new { code = 0, data = "Shop Product added successfully.", id = tblShopProduct.ID });
        }

        // DELETE: api/ShopProducts/5
        [ResponseType(typeof(tblShopProduct))]
        public IHttpActionResult DeletetblShopProduct(int id)
        {
            tblShopProduct tblShopProduct = db.tblShopProducts.Find(id);
            if (tblShopProduct == null)
            {
                return Ok(new { code = 2, data = "Shop Product not found" });
            }

            db.tblShopProducts.Remove(tblShopProduct);
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

        private bool tblShopProductExists(int id)
        {
            return db.tblShopProducts.Count(e => e.ID == id) > 0;
        }
    }
}