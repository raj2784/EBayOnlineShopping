using EbayOnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EbayOnlineShopping.Models;
using EbayOnlineShopping.DAL;
using EbayOnlineShopping.ViewModels;

namespace EbayOnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        EbayOnlineShoppingEntities context = new EbayOnlineShoppingEntities();

        public ActionResult Index(string search, int? page)

        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }

        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                var cart = new List<ItemModel>();
                var product = context.tblProducts.Find(productId);
                cart.Add(new ItemModel()
                {
                    Product = product,
                    Quntity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<ItemModel> cart = (List<ItemModel>)Session["cart"];
                var count = cart.Count();
                var product = context.tblProducts.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int previousQuantity = cart[i].Quntity;
                        cart.Remove(cart[i]);
                        cart.Add(new ItemModel()
                        {
                            Product = product,
                            Quntity = previousQuantity + 1
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new ItemModel()
                        {
                            Product = product,
                            Quntity = 1
                        });
                    }

                }
                Session["cart"] = cart;
            }
            return Redirect("Index");

        }
        public ActionResult DecreaseQuantity(int productId)
        {

            if (Session["cart"] != null)
            {
                List<ItemModel> cart = (List<ItemModel>)Session["cart"];
                var product = context.tblProducts.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int previousQuantity = item.Quntity;
                        if (previousQuantity > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new ItemModel()
                            {
                                Product = product,
                                Quntity = previousQuantity - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");

        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<ItemModel> cart = (List<ItemModel>)Session["cart"];
            //var product = context.tblProducts.Find(productId);
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");

        }
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult CheckOutDetails()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}