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
    
    public partial class User
    {
        public User()
        {
            this.Factors = new HashSet<Factor>();
            this.Messages = new HashSet<Message>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Mobile { get; set; }
        public string Tell { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public bool Gender { get; set; }
        public byte Status { get; set; }
        public string Role { get; set; }
    
        public virtual ICollection<Factor> Factors { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
