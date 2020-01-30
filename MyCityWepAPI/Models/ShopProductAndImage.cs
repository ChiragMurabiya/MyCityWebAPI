using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCityWepAPI.Models
{
    public class ShopProductAndImage
    {
        public int ID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}