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
    //REDIRECCIONAR EL PAGO A LA URL INTERNA DEL PROYECTO.
    public class CrearPagoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Index(Orden orden)
        {
            Configuration.ReceiverId = 246710;
            Configuration.Secret = "7cb89cb138e927b3defdd3fbdf84c6c0ffd9ffc0";

            
            PaymentsApi a = new PaymentsApi();

            try
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddDays(5);
                PaymentsCreateResponse response = a.PaymentsPost(
                    "Compra de prueba de la API",
                    "CLP",
                    //creo que acá tengo que buscar una forma de conseguir el valor total del carrito???
                    orden.Monto,
                                   
                    transactionId: Convert.ToString(orden.NumOrden),
                    expiresDate: dt,
                    //Ni idea como conseguir esto
                    body: "Descripción de la compra",
                    //la foto que se ingresara, será del producto, sera una imagen para todo??, por ahora dejare la del
                    //pug que es bonita
                    pictureUrl: "https://img.clasf.co/2017/08/22/Perritos-Pug-Arena-Cachorros-Disponibles-20170822141319.jpg",
                    returnUrl: "localhost:57488",
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
        }
    }
}