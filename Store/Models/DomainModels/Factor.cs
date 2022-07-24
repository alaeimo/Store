//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Store.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factor
    {
        public Factor()
        {
            this.FactorItems = new HashSet<FactorItem>();
        }
    
        public int Id { get; set; }
        public System.DateTime BuyDate { get; set; }
        public string TrackingCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> UserId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public bool isFinish { get; set; }
    
        public virtual ICollection<FactorItem> FactorItems { get; set; }
        public virtual User User { get; set; }
    }
}
