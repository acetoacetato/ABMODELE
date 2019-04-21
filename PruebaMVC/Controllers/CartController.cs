using PruebaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Khipu.Api;
using Khipu.Client;
using Khipu.Model;

namespace PruebaMVC.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy(string id)
        {
            ProductModel productModel = new ProductModel();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }

        private InfoCompra infoCompra()
        {
            InfoCompra info = new InfoCompra();
            if(Session["cart"] == null)
            {
                info.descripcion = "nada se ha comprado";
                info.total = 0;
                info.transactionId = "null00";
                return info;
            }



            List<Item> cart = (List<Item>)Session["cart"];
            long suma = 0;
            foreach(Item i in cart){
                Product p = i.Product;
                suma += i.Quantity * (long)p.Price;
                info.descripcion += "\"" + p.Name + "\"  x " + i.Quantity + " \n";
            }
            info.total = suma;
            return info;

        }

        public ActionResult pagar()
        {
            InfoCompra info = infoCompra();
            info.transactionId = new Random(999).Next(100000, 1000000).ToString();
            Configuration.ReceiverId = 235516;
            Configuration.Secret = "1f60c86e4ed36fe44cba787a3a42ccc3711ca11d";
            if(Session["Recibos"] == null)
            {
                Session["recibos"] = new List<long>();
            }

            List<long> l = (List<long>)Session["recibos"];
            l.Add(Configuration.ReceiverId);

            PaymentsApi a = new PaymentsApi();


            try
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddDays(5);
                PaymentsCreateResponse response = a.PaymentsPost(
                    "Compra de Productos",
                    "CLP",
                    info.total,
                    transactionId: info.transactionId,
                    expiresDate: dt,
                    body: info.descripcion,
                    pictureUrl: "https://img.clasf.co/2017/08/22/Perritos-Pug-Arena-Cachorros-Disponibles-20170822141319.jpg",
                    returnUrl: "http://localhost:54385",
                    cancelUrl: "http://localhost:54385",
                    notifyUrl: "https://pruebamvc.conveyor.cloud/NotificaPago",
                    notifyApiVersion: "1.3"
                 );
                ViewData["response"] = response.PaymentUrl;
                return Redirect(response.PaymentUrl);
                //return RedirectToAction("Index");
            }
            catch (ApiException e)
            {
                Console.WriteLine(e);
                ViewData["response"] = "https://google.com";
                Session["excepcion"] = e.ToString();
                return RedirectToAction("Index");
            }
            
        }

        
    }
}