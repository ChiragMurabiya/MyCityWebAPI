using MyCityWepAPI.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

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
            try
            {
                //tblCategory tblCategory = db.tblCategories.Find(id);
                //if (tblCategory == null)
                //{
                //    return NotFound();
                //}
                //return Ok(tblCategory);
                var data = db.tblCategories.Where(w => w.ID == id).FirstOrDefault();

                if (data == null)
                {
                    return Ok(new { code = 1, data = "Not found" });
                }
                else
                {
                    tblCategory model = new tblCategory();
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

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCategory(tblCategory tblCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (tblCategory == null)
                {
                    throw new ArgumentNullException("tblCategory");
                }

                var data = db.tblCategories.Where(w => w.ID == tblCategory.ID).Count();//.FirstOrDefault();
                if (data >= 0)
                {
                    tblCategory category = new tblCategory();
                    category.ID = tblCategory.ID;
                    category.Name = tblCategory.Name;
                    category.Created = System.DateTime.Now;
                    category.CreatedBy = tblCategory.CreatedBy;
                    category.Updated = System.DateTime.Now;
                    category.UpdatedBy = tblCategory.UpdatedBy;
                    category.Active = Convert.ToBoolean(tblCategory.Active);

                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();

                    return Ok(new { code = 0, data = "Category updated successfully." });
                }
                else
                {
                    return Ok(new { code = 1, data = "Category not found." });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Categories
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult PosttblCategory(tblCategory tblCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var data = db.tblCategories.Where(w => w.Name == tblCategory.Name).FirstOrDefault();

                if (data == null)
                {

                    db.tblCategories.Add(tblCategory);
                    db.SaveChanges();

                    return Ok(new { code = 0, data = "Category added successfully." });
                }
                else
                {
                    return Ok(new { code = 1, data = "Category already exists." });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult DeletetblCategory(int id)
        {
            tblCategory tblCategory = new tblCategory();

            tblCategory = db.tblCategories.Find(id);
            db.tblCategories.Remove(tblCategory);
            db.SaveChanges();

            return Ok(new { code = 0, message = "Category deleted successfully!" });
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