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

        // GET: api/Shops/5
        [ResponseType(typeof(tblShop))]
        [System.Web.Http.Route("api/GetShopAndImageByShopID")]
        public IHttpActionResult GetShopAndImageByShopID(int ShopID)
        {
            var shopAndImageData = db.tblShops.Join(db.tblShopImages, a => a.ID, b => b.ShopID, (a, b) => new { a, b })
                                    .Select(s => new ShopAndImageModel
                                    {
                                        ID = s.a.ID,
                                        UserID = s.a.UserID,
                                        CategoryID = s.a.CategoryID,
                                        CityID = s.a.CityID,
                                        StateID = s.a.StateID,
                                        Name = s.a.Name,
                                        Address = s.a.Address,
                                        Mobile = s.a.Mobile,
                                        Phone = s.a.Phone,
                                        Active = s.a.Active,
                                        ShopImageID = s.b.ID,
                                        ImageName = s.b.ImageName,
                                        ImagePath = s.b.ImagePath,
                                        ImageActive = s.b.Active
                                    }).Where(x => x.ID == ShopID).ToList();

            if (shopAndImageData == null)
            {
                return Ok(new { code = 1, data = "Not found" });
            }
            else
            {
                //return Ok(shopAndImageData);
                return Json(new { code = 0, data = shopAndImageData });
            }
        }

        // GET: api/Shops/5
        [ResponseType(typeof(tblShop))]
        [System.Web.Http.Route("api/GetShopAndImage")]
        public IHttpActionResult GetShopAndImage()
        {
            var shopAndImageData = db.tblShops.Join(db.tblShopImages, a => a.ID, b => b.ShopID, (a, b) => new { a, b })
                                   .Select(s => new ShopAndImageModel
                                   {
                                       ID = s.a.ID,
                                       UserID = s.a.UserID,
                                       CategoryID = s.a.CategoryID,
                                       CityID = s.a.CityID,
                                       StateID = s.a.StateID,
                                       Name = s.a.Name,
                                       Address = s.a.Address,
                                       Mobile = s.a.Mobile,
                                       Phone = s.a.Phone,
                                       Active = s.a.Active,
                                       ShopImageID = s.b.ID,
                                       ImageName = s.b.ImageName,
                                       ImagePath = s.b.ImagePath,
                                       ImageActive = s.b.Active
                                   }
                                            ).ToList();

            return Ok(shopAndImageData);
        }

        // PUT: api/Shops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblShop(tblShop tblShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.tblShops.Where(w => w.ID == tblShop.ID).Count();//.FirstOrDefault();
            if (data >= 0)
            {
                tblShop Shop = new tblShop();
                Shop.ID = tblShop.ID;
                Shop.UserID = tblShop.UserID;
                Shop.CategoryID = tblShop.CategoryID;
                Shop.StateID = tblShop.StateID;
                Shop.CityID = tblShop.CityID;
                Shop.Name = tblShop.Name;
                Shop.Address = tblShop.Address;
                Shop.Mobile = tblShop.Mobile;
                Shop.Phone = tblShop.Phone;
                Shop.Created = System.DateTime.Now;
                Shop.CreatedBy = tblShop.CreatedBy;
                Shop.Updated = System.DateTime.Now;
                Shop.UpdatedBy = tblShop.UpdatedBy;
                Shop.Active = Convert.ToBoolean(tblShop.Active);

                db.Entry(Shop).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new { code = 0, data = "Shop updated successfully.", id = tblShop.ID });
            }
            else
            {
                return Ok(new { code = 1, data = "Shop not found." });
            }
        }

        // POST: api/Shops
        [ResponseType(typeof(tblShop))]
        public IHttpActionResult PosttblShop(tblShop tblShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.tblShops.Where(w => w.Name == tblShop.Name).FirstOrDefault();

            if (data == null)
            {

                db.tblShops.Add(tblShop);
                db.SaveChanges();

                return Ok(new { code = 0, data = "Shop added successfully.", id = tblShop.ID });
            }
            else
            {
                return Ok(new { code = 1, data = "Shop already exists." });
            }
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

        private bool tblShopExists(int id)
        {
            return db.tblShops.Count(e => e.ID == id) > 0;
        }
    }
}