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
    public class CitiesController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/Cities
        public IQueryable<tblCity> GettblCities()
        {
            return db.tblCities;
        }

        // GET: api/Cities/5
        [ResponseType(typeof(tblCity))]
        public IHttpActionResult GettblCity(int id)
        {
            tblCity tblCity = db.tblCities.Find(id);
            if (tblCity == null)
            {
                return NotFound();
            }

            return Ok(tblCity);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCity(int id, tblCity tblCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCity.ID)
            {
                return BadRequest();
            }

            db.Entry(tblCity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCityExists(id))
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

        // POST: api/Cities
        [ResponseType(typeof(tblCity))]
        public IHttpActionResult PosttblCity(tblCity tblCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCities.Add(tblCity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblCity.ID }, tblCity);
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(tblCity))]
        public IHttpActionResult DeletetblCity(int id)
        {
            tblCity tblCity = db.tblCities.Find(id);
            if (tblCity == null)
            {
                return NotFound();
            }

            db.tblCities.Remove(tblCity);
            db.SaveChanges();

            return Ok(tblCity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCityExists(int id)
        {
            return db.tblCities.Count(e => e.ID == id) > 0;
        }
    }
}