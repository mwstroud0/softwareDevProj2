//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Group11_iCLOTHINGApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ITEM_DELIVERY
    {
        public int stickerID { get; set; }
        public int customerID { get; set; }
        public int productID { get; set; }
        public Nullable<System.DateTime> stickerDate { get; set; }
    
        public virtual CUSTOMER CUSTOMER { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}