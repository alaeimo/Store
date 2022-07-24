using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels.Admin
{
    public class AddGroupViewModel 
    {
        public IEnumerable<Store.Models.DomainModels.Group> Groups { get; set; }
        public Store.Models.DomainModels.Group Group { get; set; }
    }
}