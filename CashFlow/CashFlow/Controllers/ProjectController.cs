using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Data.SqlClient;
using CashFlow.Models;
using System.Data;
using System.Web.Security;
using System.Web.Helpers;
using System.IO;


namespace CashFlow.Controllers
{
    public class ProjectController : Controller
    {
        NewProject.ProjectDBContext dbProjet = new NewProject.ProjectDBContext();
        SqlConnection m_con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
		
        public ActionResult NewProject()
        {
            return View(new NewProject());
            /*if (User.Identity.Name != "")
                return View(new NewProject());
            else
                return RedirectToAction("Login", "Account");*/
        }

        [HttpPost]
        public ActionResult NewProject(NewProject model, HttpPostedFileBase fichier)
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

                fichier.SaveAs("C:/Users/Usager/Desktop/a14-cash-flow/CashFlow/CashFlow/Images/Uploads/" + fichier.FileName);

                EnregistrerDansBD(model);

                TempData["info"] = "Votre projet " + model.Titre + " est désormais lancé! Le financement prendra fin le "
                    + model.DateFin.ToLongDateString() + ".";

                return RedirectToAction("Index", "Home");
            }
            else
                return View(new NewProject());
        }

        public ActionResult ListeProject()
        {
            
            SqlCommand toutesDonnees = new SqlCommand();
            SqlDataReader reader;

            toutesDonnees.CommandText = "SELECT * FROM tableProject";
            toutesDonnees.CommandType = CommandType.Text;
            toutesDonnees.Connection = m_con;

            m_con.Open();

            reader = toutesDonnees.ExecuteReader();

            var aideProjet = new List<NewProject>();
			
            while (reader.Read())
            {
				aideProjet.Add(new NewProject
                {
                    Hash = reader.GetString(0),
					Titre = reader.GetString(1),
					Description = reader.GetString(2),
					Ville = reader.GetString(3),
					MontantRecu = reader.GetInt32(4),
					MontantRequis = reader.GetInt32(5),
					DateDepart = reader.GetDateTime(6),
					DateFin = reader.GetDateTime(7),
					Categorie = reader.GetString(8),
					Createur = reader.GetString(9)
				});
            }
            m_con.Close();
			TempData["message"] = "Voici la liste de tous les projets qui sont en cours de financement.";
            return View(aideProjet);
        }

        public ActionResult ProjectComplet(NewProject Projet)
        {
            return View(Projet);
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
    }
}
