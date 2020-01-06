using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCityWepAPI.Models
{
    public class ShopAndImageModel
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> ShopImageID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> ImageActive { get; set; }
    }
}