//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCityWepAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblShopImage
    {
        public int ID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
    
        public virtual tblShop tblShop { get; set; }
    }
}
