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
            //tblCity tblCity = db.tblCities.Find(id);
            //if (tblCity == null)
            //{
            //    return NotFound();
            //}
            //return Ok(tblCity);

            var data = db.tblCities.Where(w => w.ID == id).FirstOrDefault();

            if (data == null)
            {
                return Ok(new { code = 1, data = "Not found" });
            }
            else
            {
                tblCity model = new tblCity();
                model.ID = data.ID;
                model.Name = data.Name;
                model.StateID = data.StateID;
                model.Created = System.DateTime.Now;
                model.CreatedBy = data.CreatedBy;
                model.Updated = System.DateTime.Now;
                model.UpdatedBy = data.UpdatedBy;
                model.Active = Convert.ToBoolean(data.Active);

                return Ok(new { code = 0, data = model });
            }
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCity(tblCity tblCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (id != tblCity.ID)
            //{
            //    return BadRequest();
            //}
            //db.Entry(tblCity).State = EntityState.Modified;
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblCityExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //return StatusCode(HttpStatusCode.NoContent);

            if (tblCity == null)
            {
                throw new ArgumentNullException("tblCity");
            }

            var data = db.tblCities.Where(w => w.ID == tblCity.ID).Count();//.FirstOrDefault();
            if (data >= 0)
            {
                tblCity city = new tblCity();
                city.ID = tblCity.ID;
                city.StateID = tblCity.StateID;
                city.Name = tblCity.Name;
                city.Created = System.DateTime.Now;
                city.CreatedBy = tblCity.CreatedBy;
                city.Updated = System.DateTime.Now;
                city.UpdatedBy = tblCity.UpdatedBy;
                city.Active = Convert.ToBoolean(tblCity.Active);

                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new { code = 0, data = "City updated successfully." });
            }
            else
            {
                return Ok(new { code = 1, data = "City not found." });
            }
        }

        // POST: api/Cities
        [ResponseType(typeof(tblCity))]
        public IHttpActionResult PosttblCity(tblCity tblCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.tblStates.Where(w => w.Name == tblCity.Name).FirstOrDefault();

            if (data == null)
            {

                db.tblCities.Add(tblCity);
                db.SaveChanges();

                return Ok(new { code = 0, data = "City added successfully." });
            }
            else
            {
                return Ok(new { code = 1, data = "City already exists." });
            }
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(tblCity))]
        public IHttpActionResult DeletetblCity(int id)
        {
            var data1 = db.tblShops.Where(w => w.CityID == id).Count();
            if (data1 > 0)
            {
                return Ok(new { code = 1, data = "City available in shop" });
            }

            tblCity tblCity = new tblCity();

            tblCity = db.tblCities.Find(id);
            db.tblCities.Remove(tblCity);
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

        private bool tblCityExists(int id)
        {
            return db.tblCities.Count(e => e.ID == id) > 0;
        }
    }
}