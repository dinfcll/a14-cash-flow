using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Data.SqlClient;
using CashFlow.Models;
using System.Data;
using System.Globalization;


namespace CashFlow.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        NewProject.ProjectDBContext dbProjet = new NewProject.ProjectDBContext();
        SqlConnection m_con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
            

        public ActionResult Project()
        {
            if (User.Identity.Name != "")
                return View(new NewProject());
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Project(NewProject model)
        {
            if (ModelState.IsValid)
            {
                model.DateFin = Convert.ToDateTime(model.DateString);
                model.DateDepart = DateTime.Today;
                model.MontantRequis = Convert.ToInt32(model.MontantString);
                model.Createur = User.Identity.Name;

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


            List<NewProject> aideProjet = new List<NewProject>();
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
                aideProjet.Add(projet);
            }
            m_con.Close();
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
