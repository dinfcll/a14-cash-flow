using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CashFlow.Models;

namespace CashFlow.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult ValidationCommande(string TitreProjet, string Montant)
        {
            bool useSandBox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPalModel(useSandBox);

            paypal.item_name = TitreProjet;
            paypal.amount = Montant;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            return View();
        }

        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
    }
}
