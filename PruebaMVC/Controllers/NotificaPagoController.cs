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
    public class NotificaPagoController : Controller
    {
        [HttpPost]
        public ActionResult Index(String api_version, string notification_token)
        {
            Configuration.ReceiverId = 235516;
            Configuration.Secret = "1f60c86e4ed36fe44cba787a3a42ccc3711ca11d";
            string notificationToken = "obtener-desde-los-parametros-del-request";
            string apiVersion = api_version;
            //double amount = monto - original - del - cobro;

            if (apiVersion.Equals("1.3"))
            {
                PaymentsApi a = new PaymentsApi();
                try
                {
                    PaymentsResponse response = a.PaymentsGet(notificationToken);

                    //Acá se debe obtener el monto desde la base de datos con response.transactionId
                    var amount = response.Amount;

                    if (response.ReceiverId.Equals(Configuration.ReceiverId)

                           && response.Status.Equals("done") && response.Amount == amount)
                    {
// marcar el pago como completo y entregar el bien o servicio

                    }
                    else
                    {
// ignorar la invocación
                    }
                }
                catch (ApiException e)
                {
                    Console.WriteLine(e);
                }
            }
            return View();
        }
    }
}