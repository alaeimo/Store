using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.ViewModels;

namespace Store.ViewModels.Admin
{
    public class AddProductViewModel 
    {
        public IEnumerable<Store.Models.DomainModels.Group> Groups { get; set; }
        public Store.Models.DomainModels.Product Product { get; set; }
    }
}