using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace CashFlow.Controllers
{
    public class SearchController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        void Recherche(string MotCle)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
            con.Open();
            List<string[]> Resultats = new List<string[]>();
            try
            {

                SqlCommand Recherche = new SqlCommand("Select Hash,Titre,Description,Categorie FROM tableProject WHERE Titre LIKE '%" + MotCle + "%' OR Description LIKE '%" + MotCle + "%'", con);
                SqlDataReader reader = Recherche.ExecuteReader();

                int i = 0;
                string[] projet = new string[4];
                while (reader.Read())
                {
                    projet[0] = reader.GetValue(0).ToString();
                    projet[1] = reader.GetValue(1).ToString();
                    projet[2] = reader.GetValue(2).ToString();
                    projet[3] = reader.GetValue(3).ToString();
                    Resultats.Add(projet);
                }

            }
            catch (Exception Ex)
            {

            }
            finally
            {
                con.Close();
            }
        }

    }
}
