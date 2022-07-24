using Store.Models.EntityModels;
using Store.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Store.Models.EntityModels
{
    internal class GroupMetaData
    {
        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int Id { get; set; }


        [Required(ErrorMessage = "نام دسته را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("نام دسته")]
        [Display(Name = "نام دسته")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        public string Name { get; set; }


        [DisplayName("نام دسته پدر")]
        [Display(Name = "نام دسته پدر")]
        [ScaffoldColumn(false)]
        public Nullable<int> ParentId { get; set; }

      

    }

}

namespace Store.Models.DomainModels
{

    [MetadataType(typeof(GroupMetaData))]
    public partial class Group
    {
       


    }
}
