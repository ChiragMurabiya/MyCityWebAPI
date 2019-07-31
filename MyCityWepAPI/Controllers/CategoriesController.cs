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
    public class CategoriesController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/Categories
        public IQueryable<tblCategory> GettblCategories()
        {
            return db.tblCategories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult GettblCategory(int id)
        {
            tblCategory tblCategory = db.tblCategories.Find(id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return Ok(tblCategory);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCategory(int id, tblCategory tblCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(tblCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult PosttblCategory(tblCategory tblCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCategories.Add(tblCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblCategory.ID }, tblCategory);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult DeletetblCategory(int id)
        {
            tblCategory tblCategory = db.tblCategories.Find(id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            db.tblCategories.Remove(tblCategory);
            db.SaveChanges();

            return Ok(tblCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCategoryExists(int id)
        {
            return db.tblCategories.Count(e => e.ID == id) > 0;
        }
    }
}