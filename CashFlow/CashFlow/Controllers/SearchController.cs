using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using CashFlow.Models;

namespace CashFlow.Controllers
{
    public class SearchController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListeProject(string focus)
        {
            List<NewProject> Resultats = Recherche(focus);
            TempData["message"] = "Résultats de recherche pour \"" + focus + "\"";
            return View(Resultats);
        }

        List<NewProject> Recherche(string MotCle)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
            con.Open();
            List<NewProject> Resultats = new List<NewProject>();
            try
            {

                SqlCommand Recherche = new SqlCommand("Select * FROM tableProject WHERE Titre LIKE '%" + MotCle + "%' OR Description LIKE '%" + MotCle + "%'", con);
                SqlDataReader reader = Recherche.ExecuteReader();

                while (reader.Read())
                {
                    NewProject projet = new NewProject();
                    projet.Hash = reader.GetString(0);
                    projet.Titre = reader.GetString(1);
                    projet.Description = reader.GetString(2);
                    projet.Ville = reader.GetString(3);
                    projet.MontantRecu = reader.GetInt32(4);
                    projet.MontantRequis = reader.GetInt32(5);
                    projet.DateDepart = reader.GetDateTime(6);
                    projet.DateFin = reader.GetDateTime(7);
                    projet.Categorie = reader.GetString(8);
                    projet.Createur = reader.GetString(9);
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

            return Resultats;
        }
    }
}
