using Khipu.Api;
using Khipu.Client;
using Khipu.Model;
using ABMODELE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMODELE.Controllers
{
    public class NotificarPagosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(String api_version, string notification_token)
        {
            Configuration.ReceiverId = 246710;
            Configuration.Secret = "7cb89cb138e927b3defdd3fbdf84c6c0ffd9ffc0";
            //PENDIENTE, CONSEGUIR UNA URL PARA HOSTEAR LOS SERVICIOS DE PAGO.
            string notificacionToken = "obtener-desde-los-parametros-del-request";
            string apiVersion = api_version;
          
            if (apiVersion.Equals("1.3"))
            {
                PaymentsApi a = new PaymentsApi();

                try
                {
                    PaymentsResponse response = a.PaymentsGet(notificacionToken);
                    var amount = response.Amount;

                    if (response.ReceiverId.Equals(Configuration.ReceiverId)
                        && response.Status.Equals("done") && response.Amount == amount)
                    {
                        //marcar pago como completo y entregar bien o servicio.
                    }
                    else
                    {
                        //ignorar la invocación.
                    }
                }

                catch(ApiException e)
                {
                    Console.WriteLine(e);
                }
            }

        }
    }
}