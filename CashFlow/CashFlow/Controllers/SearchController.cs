﻿using System;
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

        public ActionResult ListeProject(string focus, string categorie, string ordre)
        {
            List<NewProject> Resultats = Recherche(focus, categorie, ordre);
            TempData["message"] = "Résultats de recherche pour \"" + focus + "\"";
            return View(Resultats);
        }

        List<NewProject> Recherche(string MotCle, String Categorie, string OrderBy)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
            con.Open();
            var Resultats = new List<NewProject>();
            try
            {
                string commande = "Select * FROM tableProject WHERE (Titre LIKE '%" + MotCle + "%' OR Description LIKE '%" + MotCle + "%')";

                if (Categorie != "Aucune")
                {
                    commande += " AND Categorie = " + Categorie;
                }

                if (OrderBy != "Aucun")
                {
                    commande += " ORDER BY " + OrderBy;
                }

                SqlCommand Recherche = new SqlCommand(commande, con);
                SqlDataReader reader = Recherche.ExecuteReader();

                while (reader.Read())
                {
                    Resultats.Add(new NewProject
                    {
                        Hash = reader.GetString(0),
                        Titre = reader.GetString(1),
                        Description = reader.GetString(2),
                        Ville = reader.GetInt32(3),
                        MontantRecu = reader.GetInt32(4),
                        MontantRequis = reader.GetInt32(5),
                        DateDepart = reader.GetDateTime(6),
                        DateFin = reader.GetDateTime(7),
                        Categorie = reader.GetInt32(8),
                        Createur = reader.GetString(9),
                        Image = reader.GetString(10)
                    });
                }
                

            }
            catch (Exception Ex)
            {
                throw new Exception("Erreur");
            }
            finally
            {
                con.Close();
            }

            return Resultats;
        }
    }
}
