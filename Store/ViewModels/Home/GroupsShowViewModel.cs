using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels.Home
{
    public class GroupsShowViewModel 
    {
        public IEnumerable<Store.Models.DomainModels.Group> Groups { get; set; }
        public Store.Models.DomainModels.Group ParentGroup { get; set; }
        public IEnumerable<Store.Models.DomainModels.Product> Products { get; set; }
    }

}