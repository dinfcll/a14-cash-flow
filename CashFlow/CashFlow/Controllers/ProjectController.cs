using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Data.SqlClient;
using CashFlow.Models;


namespace CashFlow.Controllers
{
    public class ProjectController : Controller
    {
        NewProject.ProjectDBContext dbProjet = new NewProject.ProjectDBContext();
        SqlConnection m_con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");   

        public ActionResult Project()
        {/*
            if (User.Identity.Name != "")
                return View(new NewProject());
            else
                return RedirectToAction("Login", "Account");
          */
            return View(new NewProject());
        }

        [HttpPost]
        public ActionResult Project(NewProject model)
        {
            if (ModelState.IsValid)
            {
                string Annee = model.DateString[6].ToString() + model.DateString[7].ToString() + model.DateString[8].ToString() + model.DateString[9].ToString();
                string Mois = model.DateString[3].ToString() + model.DateString[4].ToString();
                string Jour = model.DateString[0].ToString() + model.DateString[1].ToString();
                model.DateFin = new DateTime(Convert.ToInt32(Annee), Convert.ToInt32(Mois), Convert.ToInt32(Jour), 23, 59, 59);
                model.DateDepart = DateTime.Today;
                model.MontantRequis = Convert.ToInt32(model.MontantString);
                model.Createur = User.Identity.Name;
                Recherche("fuck");
                EnregistrerDansBD(model);

                TempData["info"] = "Votre projet " + model.Titre + " est désormais lancé! Le financement prendra fin le "
                    + model.DateFin.ToLongDateString() + ".";

                return RedirectToAction("Index", "Home");
            }
            else
                return View(new NewProject());
            
        }


        public string ChaineHasard()
        {
            Random Lettre = new Random();
            Random Chiffre = new Random();
            Random VraiOuFaux = new Random();
            int LettreAscii = 0;
            string Chaine = "";
            for (int i = 0; i < 6; i++)
            {
                if (VraiOuFaux.Next(0, 2) == 1)
                {
                    LettreAscii = Lettre.Next(65, 91);
                    if (VraiOuFaux.Next(0, 2) == 1)
                    {

                        LettreAscii += 32;
                    }
                    Chaine += (char)(LettreAscii);
                }
                else
                {
                    Chaine += (char)Chiffre.Next(48, 58);
                }

            }
            return Chaine;
        }

        void EnregistrerDansBD(NewProject model)
        {
            m_con.Open();

            SqlCommand insert = new SqlCommand("INSERT INTO tableProject VALUES ('" + ChaineHasard() + "','" + model.Titre + "','" + model.Description
                 + "','" + model.Ville + "','0','" + model.MontantString + "','" + DateTime.Now.ToShortDateString() + "','" + model.DateString
                  + "','" + model.Categorie + "','" + model.Createur + "')", m_con);
            insert.ExecuteNonQuery();
            m_con.Close();
            
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
