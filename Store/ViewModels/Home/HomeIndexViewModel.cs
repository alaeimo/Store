using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels.Home
{
    public class HomeIndexViewModel 
    {
        public IEnumerable<Store.Models.DomainModels.Group> Groups { get; set; }
        public IEnumerable<Store.Models.DomainModels.Product> Products { get; set; }
        public IEnumerable<Store.Models.DomainModels.Product> BestSelleingProducts { get; set; }
        public IEnumerable<Store.Models.DomainModels.Product> NewProducts { get; set; }
        public IEnumerable<Store.Models.DomainModels.Product> SpecialProducts { get; set; }
        public IEnumerable<Store.Models.DomainModels.Product> MostPopularProducts { get; set; }
    }

}