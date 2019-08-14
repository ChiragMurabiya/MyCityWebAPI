using MyCityWepAPI.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace MyCityWepAPI.Controllers
{
    public class StatesController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/States
        public IQueryable<tblState> GettblStates()
        {
            return db.tblStates;
        }

        // GET: api/States/5
        [ResponseType(typeof(tblState))]
        public IHttpActionResult GettblState(int id)
        {
            try
            {
                //tblState tblState = db.tblStates.Find(id);
                //if (tblState == null)
                //{
                //    return NotFound();
                //}

                //return Ok(tblState);
                var data = db.tblStates.Where(w => w.ID == id).FirstOrDefault();

                if (data == null)
                {
                    return Ok(new { code = 1, data = "Not found" });
                }
                else
                {
                    tblState model = new tblState();
                    model.ID = data.ID;
                    model.Name = data.Name;
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

        // PUT: api/States/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblState(tblState tblState)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != tblState.ID)
            //{
            //    return BadRequest();
            //}

            //db.Entry(tblState).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblStateExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);
            if (tblState == null)
            {
                throw new ArgumentNullException("tblState");
            }

            var data = db.tblStates.Where(w => w.ID == tblState.ID).Count();//.FirstOrDefault();
            if (data >= 0)
            {
                tblState state = new tblState();
                state.ID = tblState.ID;
                state.Name = tblState.Name;
                state.Created = System.DateTime.Now;
                state.CreatedBy = tblState.CreatedBy;
                state.Updated = System.DateTime.Now;
                state.UpdatedBy = tblState.UpdatedBy;
                state.Active = Convert.ToBoolean(tblState.Active);

                db.Entry(state).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new { code = 0, data = "State updated successfully." });
            }
            else
            {
                return Ok(new { code = 1, data = "State not found." });
            }
        }

        // POST: api/States
        [ResponseType(typeof(tblState))]
        public IHttpActionResult PosttblState(tblState tblState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.tblStates.Where(w => w.Name == tblState.Name).FirstOrDefault();

            if (data == null)
            {

                db.tblStates.Add(tblState);
                db.SaveChanges();

                return Ok(new { code = 0, data = "State added successfully." });
            }
            else
            {
                return Ok(new { code = 1, data = "State already exists." });
            }

            //return CreatedAtRoute("DefaultApi", new { id = tblState.ID }, tblState);            
        }

        // DELETE: api/States/5
        [ResponseType(typeof(tblState))]
        public IHttpActionResult DeletetblState(int id)
        {
            var data = db.tblCities.Where(w => w.StateID == id).Count();

            if (data > 0)
            {
                return Ok(new { code = 1, data = "State available in city" });
            }

            var data1 = db.tblShops.Where(w => w.StateID == id).Count();
            if (data1 > 0)
            {
                return Ok(new { code = 2, data = "State available in shops" });
            }

            tblState tblState = new tblState();

            tblState = db.tblStates.Find(id);
            db.tblStates.Remove(tblState);
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

        private bool tblStateExists(int id)
        {
            return db.tblStates.Count(e => e.ID == id) > 0;
        }
    }
}