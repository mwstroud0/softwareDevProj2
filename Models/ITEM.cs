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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.ITEM_DELIVERY = new HashSet<ITEM_DELIVERY>();
            this.SHOPPING_CART = new HashSet<SHOPPING_CART>();
        }
    
        public int productID { get; set; }
        public int brandID { get; set; }
        public int categoryID { get; set; }
        public int adminID { get; set; }
        public int departmentID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Price")]
        public double productPrice { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Inventory")]
        public int productQty { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Purchase Quantity")]
        public int purchaseAmt { get; set; }
    
        public virtual ADMINISTRATOR ADMINISTRATOR { get; set; }
        public virtual BRAND BRAND { get; set; }
        public virtual CATEGORY CATEGORY { get; set; }
        public virtual DEPARTMENT DEPARTMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_DELIVERY> ITEM_DELIVERY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SHOPPING_CART> SHOPPING_CART { get; set; }
    }
}
