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
    
    public partial class USER_COMMENTS
    {
        public int commentNo { get; set; }
        public int customerID { get; set; }
        public Nullable<System.DateTime> commentDate { get; set; }
        public string commentDescription { get; set; }
    
        public virtual CUSTOMER CUSTOMER { get; set; }
    }
}