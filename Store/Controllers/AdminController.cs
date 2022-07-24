using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models.Repositories;
using Store.ViewModels.Admin;
using Store.Models.DomainModels;
using Store.Helpers.Filters;

namespace Store.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        GroupRepository blGroup = new GroupRepository();
        ProductRepository blProduct = new ProductRepository();
        

     
        [HttpGet]
        public ActionResult Groups()
        {
            var model = new AddGroupViewModel();
            model.Groups = blGroup.Select().ToList();
          
            return View(model);
        }

        [HttpPost]
        public ActionResult Groups(Group group,String btnAdd,String btnDelete)
        {
            if (btnAdd != null)
            {
                return (AddGroups(group));
            }
            else
            {
                return (DeleteGroups(group.ParentId));
            }
          
        }

        [HttpPost]
        public ActionResult AddGroups(Group group)
        {
           
            if (ModelState.IsValid)
            {
                string[] subGroups = group.Name.Split(',');
                bool check = false;
                for (int i = 0; i < subGroups.Length;i++)
                {
                    Group subGroup = new Group() ;
                    subGroup.Name = subGroups[i];
                    subGroup.ParentId = group.ParentId;
                    check = blGroup.Add(subGroup);
                }

                if (check)
                    ViewBag.Success = "با موفقیت ثبت شد";
                else
                    ViewBag.Error = "ثبت نشد،مقادیر ورودی را بررسی کنید.";

            }
            else
            {
                ViewBag.Warning = "مقادیر ورودی را بررسی کنید";
            }

            var model = new AddGroupViewModel();
            model.Groups = blGroup.Select().ToList();
            return View("Groups",model);
        }

        [HttpPost]
        public ActionResult DeleteGroups(int? ParentId)
        {

            if (blGroup.Delete(ParentId))
            {
                foreach (var item in blGroup.Select())
                {
                    if (item.ParentId == ParentId)
                    {
                        blGroup.Delete(item.Id);
                    }
                }
                blGroup.Save();
                ViewBag.DeleteSuccess = "با موفقیت حذف شد";
            }
                    
                else
                    ViewBag.DeleteWarning = "حذف نشد،مقادیر ورودی را بررسی کنید.";

            var model = new AddGroupViewModel();
            model.Groups = blGroup.Select().ToList();
            return View("Groups",model);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            var model = new AddProductViewModel();
            model.Groups = blGroup.Select().ToList();
            return View(model);
        }

        public ActionResult Products()
        {

            return View(blProduct.Select());
        }

        [HttpPost]
        public ActionResult AddProduct(Product product,HttpPostedFileBase UploadImage)
        {
            String ImagePath = null;
            if (UploadImage != null)
            {
                product.Image = UploadImage.FileName;
                ImagePath = Server.MapPath("~") + "Files\\UploadImage\\" + UploadImage.FileName;
                UploadImage.InputStream.ResizeImageByWidth(500, ImagePath, Utilty.ImageComperssion.Normal);
            }

            if (ModelState.IsValid)
            {
                
                if (blProduct.Add(product))
                    ViewBag.Success = "با موفقیت ثبت شد";
                else
                    ViewBag.Error = "ثبت نشد،مقادیر ورودی را بررسی کنید.";
            }
            else
            {
                try
                {
                    System.IO.File.Delete(ImagePath);
                }
                catch { }
                ViewBag.Warning = "مقادیر ورودی را بررسی کنید";
            }
            var model = new AddProductViewModel();
            model.Groups = blGroup.Select().ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var model = new AddProductViewModel();
            model.Groups = blGroup.Select().ToList();
            model.Product = blProduct.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase UploadImage)
        {
            String ImagePath = null;
            if (UploadImage != null)
            {
                try
                {
                    String DeletedImage = Server.MapPath("~") + "Files\\UploadImage\\" + (blProduct.Find(product.Id)).Image;
                    System.IO.File.Delete(DeletedImage);
                }
                catch { }
                product.Image = UploadImage.FileName;
                ImagePath = Server.MapPath("~") + "Files\\UploadImage\\" + UploadImage.FileName;
                UploadImage.InputStream.ResizeImageByWidth(500, ImagePath, Utilty.ImageComperssion.Normal);
            }
            else
                product.Image = (blProduct.Find(product.Id)).Image;
            if (ModelState.IsValid)
            {
                int deleted = product.Id;
               // product.Id = 0;
                if (blProduct.Update(product) )
                    ViewBag.Success = "با موفقیت ویرایش شد";
                else
                    ViewBag.Error = "ویرایش نشد";
            }
            else
            {
                try
                {
                    System.IO.File.Delete(ImagePath);
                }
                catch { }
                ViewBag.Warning = "مقادیر ورودی را بررسی کنید";
            }

            return View("Products", blProduct.Select());
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if(blProduct.Delete(id))
                ViewBag.Success = "با موفقیت حذف شد";
            else
                ViewBag.Error = "حذف نشد";
            return View("Products",blProduct.Select());
        }
    }

}
