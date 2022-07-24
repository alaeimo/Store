using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models.DomainModels;
using Store.Models.Repositories;
using Store.ViewModels.Home;
using System.Web.Security;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home
        [HttpGet]
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();
            GroupRepository blGroup = new GroupRepository();
            ProductRepository blProduct = new ProductRepository();
            model.Groups = blGroup.Select();
            model.Products = blProduct.Select();
            model.BestSelleingProducts = blProduct.Select().OrderBy(p => p.FactorItems.Count).Take(6);
            model.NewProducts = blProduct.Select().OrderByDescending(p => p.Id).Take(6);
            model.SpecialProducts = blProduct.Select().OrderByDescending(p => p.Price).Take(6);
            model.MostPopularProducts = blProduct.Select().OrderByDescending(p => p.Like).Take(6);
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            UserRepository blUser = new UserRepository();

            if (ModelState.IsValid)
            {
                if (!blUser.Where(p => p.Username == user.Username).Any())
                {
                    user.Role = "User";
                    if (blUser.Add(user))
                        ViewBag.Success = "با موفقیت ثبت شد";
                    else
                        ViewBag.Error = "ثبت نشد مقادیر ورودی را بررسی کنید";
                }
                else
                {
                    ViewBag.Error = "نام کاربری وجود دارد!";
                }
            }
            else
            {
                ViewBag.Warning = "مقادیر ورودی را بررسی کنید";
            }

            return View();
        }

        public class JsonData
        {
            public String Html { get; set; }
            public bool Success { get; set; }
        }

        [HttpPost]
        public ActionResult AddToCart(int Id, byte Count)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("Cart_" + Id.ToString()))
                { 
                     HttpCookie cookie; 
                    if(Count >=1)
                         cookie = new HttpCookie("Cart_" + Id.ToString(), Count.ToString());
                    else
                         cookie = new HttpCookie("Cart_" + Id.ToString(), (Convert.ToByte(Request.Cookies["Cart_" + Id.ToString()].Value) + 1).ToString());
                    
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.HttpOnly = true;
                    Response.Cookies.Set(cookie);
                }
                else
                {
                    
                    var cookie = new HttpCookie("Cart_" + Id.ToString(), Count.ToString());
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                }
                List<HttpCookie> list = new List<HttpCookie>();
                for (int i = Request.Cookies.Count - 1; i >= 0; i--)
                {
                    if (list.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                        list.Add(Request.Cookies[i]);
                }
                int CartCount = list.Where(p => p.Name.StartsWith("Cart_")).Count();
                return Json(new JsonData()
                {
                    Success = true,
                    Html = "لیست خرید (" + CartCount.ToString() + ")"
                });
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Html = ""
                });
            }
        }

        [HttpPost]
        public ActionResult RemoveCart(int Id)
        {
            try
            {
            
                if (Request.Cookies.AllKeys.Contains("Cart_" + Id.ToString()))
                {
                    Response.Cookies["Cart_" + Id.ToString()].Expires = DateTime.Now.AddDays(-1);
                    Request.Cookies.Remove("Cart_" + Id.ToString());
                    return Json(new JsonData()
                    {
                        Success = true,
                        Html = "لیست خرید (" + CartCount() + ")"
                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Success = false,
                        Html = "لیست خرید (" + CartCount() + ")"
                    });
                }
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Html = ""
                });
            }
        }

        public string CartCount()
        {
            List<HttpCookie> list = new List<HttpCookie>();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                list.Add(Request.Cookies[i]);
            }
            int CartCount = list.Where(p => p.Name.StartsWith("Cart_") && p.HttpOnly == false).Count();
            return CartCount.ToString();
        }

        public ActionResult ShowGroups(String groupName , int GroupId)
        {
            GroupRepository blGroup = new GroupRepository();
            ProductRepository blProduct = new ProductRepository();

            if (!String.IsNullOrEmpty(groupName))
            {
                var src = blGroup.Where(p => p.Name.Trim() == groupName.Trim() && p.Id == GroupId).Single().Groups1.ToList();
                if (src.Any())
                {
                    var model = new GroupsShowViewModel();
                    model.Groups = src;
                    model.ParentGroup = blGroup.Where(p => p.Name.Trim() == groupName.Trim() && p.Id == GroupId).Single();
                    List<Product> productList = new List<Product>();
                    foreach (var group in model.Groups)
                    {
                        productList = productList.Concat(blProduct.Where(p => p.GroupId == group.Id).ToList()).ToList();
                      
                    }
                    model.Products = productList;
                    return View(model);
                }
                else
                {
                    var model = new GroupsShowViewModel();
                    model.Groups = blGroup.Select();
                    model.ParentGroup = blGroup.Where(p => p.Name.Trim() == groupName.Trim() && p.Id == GroupId).Single();
                    model.Products = blProduct.Where(p => p.GroupId == GroupId).ToList();
                    return View(model);
                }
            }
            else
            {
                var model = new GroupsShowViewModel();
                model.Groups = blGroup.Select();
                model.Products = blProduct.Select();
                model.ParentGroup = null;
                return View(model);
            }
            
            
        }

        public ActionResult ShowProduct(String productURL) {
            ProductRepository blProduct = new ProductRepository();
            return View(blProduct.Where(p=> p.Url.Trim() == productURL.Trim()).Single());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(String Username,String Password,bool RememberMe)
        {
            UserRepository blUser = new UserRepository();
            if (blUser.Exist(Username, Password))
            {
                FormsAuthentication.SetAuthCookie(Username, RememberMe);
                return RedirectToAction("Profile");
            }
            else
                ViewBag.Message = "نام کاربری یا رمز عبور اشتباه است";
            return View();
        }
        public ActionResult Logout()
        {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index");
            
        }

        public ActionResult Profile()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");
            else
            {
                UserRepository blUser = new UserRepository();
                var model = blUser.Where(p => p.Username == User.Identity.Name).Single();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Basket()
        {
            List<BasketViewModel> BasketList = new List<BasketViewModel>();
            ProductRepository blProduct = new ProductRepository();
            List<HttpCookie> list = new List<HttpCookie>();
            for (int i = Request.Cookies.Count - 1; i >= 0; i--)
            {
                if (list.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                    list.Add(Request.Cookies[i]);
            }
            
            foreach (var item in list.Where(p => p.Name.StartsWith("Cart_") && p.HttpOnly == false))
            {
                Product product = blProduct.Find(Convert.ToInt32(item.Name.Substring(5)));
                int count = Convert.ToInt32(item.Value);
                decimal totalPrice = count * product.Price;
                BasketList.Add(new BasketViewModel{ Product = product , Count = count,TotalPrice = totalPrice});
            }
            return View(BasketList);
        }

        [HttpGet]
        public ActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Factor factor)
        {
            if (ModelState.IsValid)
            {
               
                FactorRepository blFactor = new FactorRepository();
                FactorItemRepository blFactorItem = new FactorItemRepository();
                ProductRepository blProduct = new ProductRepository();
                List<BasketViewModel> listBasket = new List<BasketViewModel>();
                List<HttpCookie> lst = new List<HttpCookie>();
                for (int i = Request.Cookies.Count - 1; i >= 0; i--)
                {
                    if (lst.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                        lst.Add(Request.Cookies[i]);
                }
                foreach (var item in lst.Where(p => p.Name.StartsWith("Cart_")))
                {
                    listBasket.Add(new BasketViewModel { Product = blProduct.Find(Convert.ToInt32(item.Name.Substring(5))), Count = Convert.ToInt32(item.Value) });
                }

                decimal price = 0;
                foreach (var item in listBasket)
                {
                    price += (item.Count * item.Product.Price);
                }

                factor.BuyDate = DateTime.Now;
                factor.Price = price;
                factor.Description = "خرید توسط کاربر " + factor.Name + " در تاریخ " + DateTime.Now.ToPersianDate().ToString() + " به مبلغ" + factor.Price.ToPrice() + " تومان انجام شد";
                if (Session["UserId"] != null)
                {
                    factor.UserId = Convert.ToInt32(Session["UserId"]);
                }
                factor.isFinish = false;
                factor.PostalCode = "5651933635";
                factor.TrackingCode = "12354";
                if (blFactor.Add(factor))
                {
                    int factorId = blFactor.GetLastIdentity();
                    foreach (var item in listBasket)
                    {
                        blFactorItem.Add(new FactorItem() { FactorId = factorId, ProductId = item.Product.Id, Qty = Convert.ToByte(item.Count) });
                    }
                    // redirect to ...
                    
    
                   ViewBag.Message = " در حال تلاش برای اتصال به درگاه بانکی";
                    
                }
                else
                {
                    ViewBag.Message = "اطلاعات شما ثبت نشد";
                }
            }
            else
            {
                ViewBag.Message = "اطلاعات خود را بدرستی وارد کنید";
            }
            return View();
        }
    }
    
       
    }


