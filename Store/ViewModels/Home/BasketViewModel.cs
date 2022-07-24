using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels.Home
{
    public class BasketViewModel 
    {
      
        public Store.Models.DomainModels.Product Product { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
      
    }

}