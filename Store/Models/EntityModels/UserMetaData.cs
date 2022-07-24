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
    internal class UserMetaData
    {
        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int Id { get; set; }


        [Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید",AllowEmptyStrings = false)]
        [DisplayName("نام و نام خانوادگی")]
        [Display(Name="نام و نام خانوادگی")]
        [StringLength(50,ErrorMessage="حداکثر 50 کاراکتر")]
        public string Name { get; set; }



        [Required(ErrorMessage ="ایمیل خود را وارد کنید")]
        [DisplayName("ایمیل(نام کاربری)")]
        [Display(Name = "ایمیل(نام کاربری)")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "ایمیل را به درستی وارد کنید")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        public string Username { get; set; }



        [Required(ErrorMessage = "رمز عبور خود را وارد کنید",AllowEmptyStrings = false)]
        [DisplayName("رمز عبور")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,15}$", ErrorMessage = "پسورد خود را به درستی وارد کنید(8-15 کاراکتر شامل اعداد و حروف بزرگ و کوچک)")]

        public string Password { get; set; }



        [DisplayName("تاریخ تولد")]
        [Display(Name = "تاریخ تولد")]
       
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("شماره موبایل")]
        [Display(Name = "شماره موبایل")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        [RegularExpression(@"(0|\+98)?([ ]|,|-|[()]){0,2}9[1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}$", ErrorMessage = "شماره موبایل را به درستی وارد کنید")]
        public string Mobile { get; set; }

        

        [DisplayName("شماره تماس")]
        [Display(Name = "شماره تماس")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        public string Tell { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("آدرس")]
        [Display(Name = "آدرس")]
        [StringLength(500, ErrorMessage = "حداکثر 500 کاراکتر")]
        public string Address { get; set; }

        [Required(ErrorMessage = "کد پستی خود را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("کد پستی")]
        [Display(Name = "کد پستی")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد پستی را به درستی وارد کنید")]
        public string PostalCode { get; set; }


        [DisplayName("جنسیت")]
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }


        [ScaffoldColumn(false)]
        [DisplayName("وضعیت")]
        [Display(Name = "وضعیت")]
        public byte Status { get; set; }
    
    }

}

namespace Store.Models.DomainModels
{

    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور خود را تکرار کنید")]
        [DisplayName("تکرار رمز عبور")]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "تکرار پسورد یکسان نیست")]
        public string ConfirmPassword { get; set; }


    }
}
