using Khipu.Api;
using Khipu.Client;
using Khipu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            Configuration.ReceiverId = 235516;
            Configuration.Secret = "1f60c86e4ed36fe44cba787a3a42ccc3711ca11d";
            PaymentsApi a = new PaymentsApi();

            try
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddDays(5);
                PaymentsCreateResponse response = a.PaymentsPost(
                    "Compra de prueba de la API",
                    "CLP",
                    100.0,
                    transactionId: "FACT2001",
                    expiresDate: dt,
                    body: "Descripción de la compra",
                    pictureUrl: "https://img.clasf.co/2017/08/22/Perritos-Pug-Arena-Cachorros-Disponibles-20170822141319.jpg",
                    returnUrl: "localhost:54385",
                    cancelUrl: "localhost",
                    notifyUrl: "localhost",
                    notifyApiVersion: "1.3"
                 );
                ViewData["response"] = response.PaymentUrl;
            }
            catch (ApiException e)
            {
                Console.WriteLine(e);
            }
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