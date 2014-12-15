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
        public ActionResult ValidationCommande(string TitreProjet, string Montant, string Hash)
        {
            bool useSandBox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPalModel(useSandBox);

            paypal.item_name = TitreProjet;
            paypal.amount = Montant;
            Session["montant"] = Montant;
            Session["hash"] = Hash;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
            con.Open();
            SqlCommand read = new SqlCommand("Select MontantRecu FROM tableProject WHERE Hash = '" + Session["hash"].ToString() + "'", con);
            SqlDataReader reader = read.ExecuteReader();
            reader.Read();
            int MontantRecu = reader.GetInt32(0);
            reader.Close();

            MontantRecu += Convert.ToInt32(Session["montant"]);
            SqlCommand update = new SqlCommand("UPDATE tableProject SET MontantRecu = " + MontantRecu.ToString() + " WHERE  Hash = '" + Session["hash"] + "'", con);
            update.ExecuteNonQuery();

            con.Close();
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
