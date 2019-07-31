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
    public class RolesController : ApiController
    {
        private ShoppyDBEntities db = new ShoppyDBEntities();

        // GET: api/Roles
        public IQueryable<tblRole> GettblRoles()
        {
            return db.tblRoles;
        }

        // GET: api/Roles/5
        [ResponseType(typeof(tblRole))]
        public IHttpActionResult GettblRole(int id)
        {
            tblRole tblRole = db.tblRoles.Find(id);
            if (tblRole == null)
            {
                return NotFound();
            }

            return Ok(tblRole);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblRole(int id, tblRole tblRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblRole.ID)
            {
                return BadRequest();
            }

            db.Entry(tblRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblRoleExists(id))
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

        // POST: api/Roles
        [ResponseType(typeof(tblRole))]
        public IHttpActionResult PosttblRole(tblRole tblRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblRoles.Add(tblRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblRole.ID }, tblRole);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(tblRole))]
        public IHttpActionResult DeletetblRole(int id)
        {
            tblRole tblRole = db.tblRoles.Find(id);
            if (tblRole == null)
            {
                return NotFound();
            }

            db.tblRoles.Remove(tblRole);
            db.SaveChanges();

            return Ok(tblRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblRoleExists(int id)
        {
            return db.tblRoles.Count(e => e.ID == id) > 0;
        }
    }
}