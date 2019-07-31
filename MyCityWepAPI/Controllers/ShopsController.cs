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
    public class ShopsController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/Shops
        public IQueryable<tblShop> GettblShops()
        {
            return db.tblShops;
        }

        // GET: api/Shops/5
        [ResponseType(typeof(tblShop))]
        public IHttpActionResult GettblShop(int id)
        {
            tblShop tblShop = db.tblShops.Find(id);
            if (tblShop == null)
            {
                return NotFound();
            }

            return Ok(tblShop);
        }

        // PUT: api/Shops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblShop(int id, tblShop tblShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblShop.ID)
            {
                return BadRequest();
            }

            db.Entry(tblShop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblShopExists(id))
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

        // POST: api/Shops
        [ResponseType(typeof(tblShop))]
        public IHttpActionResult PosttblShop(tblShop tblShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblShops.Add(tblShop);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblShop.ID }, tblShop);
        }

        // DELETE: api/Shops/5
        [ResponseType(typeof(tblShop))]
        public IHttpActionResult DeletetblShop(int id)
        {
            tblShop tblShop = db.tblShops.Find(id);
            if (tblShop == null)
            {
                return NotFound();
            }

            db.tblShops.Remove(tblShop);
            db.SaveChanges();

            return Ok(tblShop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblShopExists(int id)
        {
            return db.tblShops.Count(e => e.ID == id) > 0;
        }
    }
}