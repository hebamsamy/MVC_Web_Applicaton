using Ecomaerce.Hubs;
using Ecomaerce.Models;
using Ecomaerce.Models.Home;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecomaerce.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();


        //dbMyOnlineShoppingEntities ctx = new dbMyOnlineShoppingEntities();
        public ActionResult Index(string searchKey, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(searchKey, 4, page));
        }
        [HttpPost]
        public ActionResult Details(int productid)
        {
            var prod = context.Products.FirstOrDefault(p => p.ID == productid);
            return View(prod);
        }
        [HttpPost]
        public ActionResult AddToCartfromcart(int productid)
        {
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                var prod = context.Products.Find(productid);
               
                cart.Add(new item
                {
                    Product = prod,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<item> cart = (List<item>)Session["cart"];
                var prod = context.Products.Find(productid);
                foreach (var item in cart.ToArray())
                {
                    int prevQty = item.Quantity;
                    if (item.Product.ID == productid)
                    {
                        cart.Remove(item);
                        cart.Add(new item
                        {
                            Product = prod,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new item
                        {
                            Product = prod,
                            Quantity = 1
                        });
                    }
                    break;
                }
                context.Products.FirstOrDefault(d => d.ID == productid).Quantity = context.Products.FirstOrDefault(d => d.ID == productid).Quantity - 1;
                context.SaveChanges();
                Session["cart"] = cart;
            }

            return RedirectToAction("cartDetail");
        }

        public ActionResult cartDetail()
        {
            return View((List<item>)Session["cart"]);
        }
        
        [System.Web.Mvc.Authorize]
        [HttpPost]
        public ActionResult AddToCart(int productid)
        {
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                var prod = context.Products.Find(productid);
                context.Products.FirstOrDefault(d => d.ID == productid).Quantity = context.Products.FirstOrDefault(d => d.ID == productid).Quantity - 1;
                context.SaveChanges();
                cart.Add(new item
                {
                    Product = prod,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else 
            {
                List<item> cart = new List<item>();
                var prod = context.Products.Find(productid);
                bool isfound = false;
            foreach (var item in cart)
            {
                if (item.Product.ID == productid)
                {
                    int preQty = item.Quantity;
                    cart.Remove(item);
                    cart.Add(new item()
                    {
                        Product = prod
                        ,
                        Quantity = preQty + 1
                    });
                    isfound = true;
                    break;
                }
            }
            if (isfound == false)
            {
                cart.Add(new item()
                {
                    Product = prod
                    ,
                    Quantity = 1
                });
            }
            context.Products.FirstOrDefault(d => d.ID == productid).Quantity = context.Products.FirstOrDefault(d => d.ID == productid).Quantity - 1;
                context.SaveChanges();
                Session["cart"] = cart;
            }
            IHubContext hubContext =
                  GlobalHost.ConnectionManager.GetHubContext<ProductsHub>();
            hubContext.Clients.All.ReceiveChanges(productid, context.Products.FirstOrDefault(p => p.ID == productid).Quantity);
                     
            return Redirect("Index");
        }


        public ActionResult RemoveFromCart(int productid)
        {
            List<item> cart = (List<item>)Session["cart"];

            foreach (var item in cart)
            {
                if (item.Product.ID == productid)
                {
                    context.Products.FirstOrDefault(d => d.ID == productid).Quantity = context.Products.FirstOrDefault(d => d.ID == productid).Quantity +item.Quantity;
                    context.SaveChanges();
                    cart.Remove(item);
                    break;
                }
            }
            IHubContext hubContext =
                 GlobalHost.ConnectionManager.GetHubContext<ProductsHub>();
            hubContext.Clients.All.ReceiveChanges(productid, context.Products.FirstOrDefault(p => p.ID == productid).Quantity);

            Session["cart"] = cart;
            return Redirect("cartDetail");
        }


        public ActionResult DecreaseQty(int productid)
        {
            if (Session["cart"] != null)
            {
                List<item> cart = (List<item>)Session["cart"];
                var prod = context.Products.Find(productid);
                foreach (var item in cart)
                {
                    if (item.Product.ID == productid)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new item
                            {
                                Product = prod,
                                Quantity = prevQty - 1
                            });
                        }
                        else if (prevQty < 1)
                        {
                            cart.Remove(item);
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("cartDetail");
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}