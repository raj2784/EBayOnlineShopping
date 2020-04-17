using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using EbayOnlineShopping.Models;
using EbayOnlineShopping.ViewModels;
using EbayOnlineShopping.ViewModel;

namespace EbayOnlineShopping.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            APIContext aPIContext = PayPalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerId"];
                if (string.IsNullOrEmpty(payerId) && payerId != null)
                {
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority +
                         "PaymentWithPaypal/PaymentWithPaypal?";

                    var guid = Convert.ToString((new Random()).Next(1000000000));
                    var createPayment = this.CreatePayment(aPIContext, baseUri + "guid=" + guid);

                    var links = createPayment.links.GetEnumerator();

                    string paypalRedirectURL = null;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;

                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectURL = link.href;
                        }

                    }
                }
                else
                {
                    var guid = Request.Params["guid"];

                    var excecutedPayment = ExecutePayment(aPIContext, payerId, Session[guid] as string);

                    if (excecutedPayment.ToString().ToLower() != "approved")
                    {
                        return View("PaymentFailurView");
                    }
                }
            }
            catch (Exception)
            {
                return View("PaymentFailurView");
                //throw;
            }

            return View("PaymentSuccessView");
        }

        private PayPal.Api.Payment payment;
        private object ExecutePayment(APIContext aPIContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(aPIContext, paymentExecution);
        }
           
        private Payment CreatePayment(APIContext aPIContext, string redirectURL)
        {
            var itemList = new ItemList() { items = new List<Item>() };

            if (Session["cart"] != null)
            {
                List<ItemModel> cart = (List<ItemModel>)(Session["cart"]);
                foreach (var item in cart)
                {
                    itemList.items.Add(new Item()
                    {
                        name = item.Product.ProductName.ToString(),
                        currency = "AUD",
                        price = item.Product.Price.ToString(),
                        quantity = item.Product.Quantity.ToString(),
                        sku = "sku"

                    });

                }
                var payer = new Payer() { payment_method = "paypal" };

                var redirUrl = new RedirectUrls()
                {
                    cancel_url = redirectURL + "&cancel = true",
                    return_url = redirectURL
                };

                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = "1"
                };

                var amount = new Amount()
                {
                    currency = "AUD",
                    total = Session["SesTotal"].ToString(),
                    details = details,
                };

                var transactionList = new List<Transaction>();

                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = "#100000",
                    amount = amount,
                    item_list = itemList

                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrl
                };
            }

            return this.payment.Create(aPIContext);

        }
    }
}