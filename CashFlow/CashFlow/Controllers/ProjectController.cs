﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Data.SqlClient;
using CashFlow.Models;
using System.Data;


namespace CashFlow.Controllers
{
    public class ProjectController : Controller
    {
        NewProject.ProjectDBContext dbProjet = new NewProject.ProjectDBContext();
        SqlConnection m_con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");    

        public ActionResult Project()
        {
            if (User.Identity.Name != "")
                return View(new NewProject());
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Project(NewProject model)
        {
            if (ModelState.IsValid)
            {
                string annee = string.Format("{0}{1}{2}{3}", model.DateString[6].ToString(), model.DateString[7].ToString(), model.DateString[8].ToString(), model.DateString[9].ToString());
                string mois = string.Format("{0}{1}", model.DateString[3].ToString(), model.DateString[4].ToString());
                string jour = string.Format("{0}{1}", model.DateString[0].ToString(), model.DateString[1].ToString());
                model.DateFin = new DateTime(Convert.ToInt32(annee),Convert.ToInt32(mois),Convert.ToInt32(jour), 23, 59, 59);
                model.DateDepart = DateTime.Today;
                model.MontantRequis = Convert.ToInt32(model.MontantString);
                model.Createur = User.Identity.Name;

                EnregistrerDansBd(model);

                TempData["info"] = "Votre projet " + model.Titre + " est désormais lancé! Le financement prendra fin le "
                    + model.DateFin.ToLongDateString() + ".";

                return RedirectToAction("Index", "Home");
            }
            return View(new NewProject());
        }

        public ActionResult ListeProject()
        {
            var toutesDonnees = new SqlCommand
            {
                CommandText = "SELECT * FROM tableProject",
                CommandType = CommandType.Text,
                Connection = m_con
            };

            m_con.Open();

            SqlDataReader reader = toutesDonnees.ExecuteReader();

            var aideProjet = new List<NewProject>();
            while (reader.Read())
            {
                var projet = new NewProject
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
                };
                aideProjet.Add(projet);
            }
            m_con.Close();
            return View(aideProjet);
        }

        public ActionResult ProjectComplet(NewProject projet)
        {
            return View(Tuple.Create(projet,RechercheCommentaire(projet.Hash), new CommentaireModel()));
        }

        [HttpPost]
        public ActionResult ProjectComplet(NewProject projet, [Bind(Prefix = "Item3")] CommentaireModel commentaire)
        {
            AjoutCommentaire(projet.Hash,commentaire.Commentaire);
            ModelState.Clear();
            return View(Tuple.Create(projet, RechercheCommentaire(projet.Hash), new CommentaireModel()));
        }

        public string ChaineHasard()
        {
            var builder = new StringBuilder();
            var random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 6; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        void EnregistrerDansBd(NewProject model)
        {
            m_con.Open();

            var insert = new SqlCommand("INSERT INTO tableProject VALUES ('" + ChaineHasard() + "','" + model.Titre + "','" + model.Description
                 + "','" + model.Ville + "','0','" + model.MontantString + "','" + DateTime.Now.ToShortDateString() + "','" + model.DateString
                  + "','" + model.Categorie + "','" + model.Createur + "')", m_con);
            insert.ExecuteNonQuery();
            m_con.Close();    
        }

        private ListeCommentaireModel RechercheCommentaire(string hash)
        {
            var toutesDonnees = new SqlCommand
            {
                CommandText = "SELECT * FROM tableCommentaire WHERE Hash ='" + hash + "'",
                CommandType = CommandType.Text,
                Connection = m_con
            };

            var listeCommentaire = new ListeCommentaireModel();

            m_con.Open();

            SqlDataReader reader = toutesDonnees.ExecuteReader();
            while (reader.Read())
            {
                var commentaire = new CommentaireModel
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Hash = reader.GetString(2),
                    Commentaire = reader.GetString(3),
                    Date = reader.GetDateTime(4)
                };
                listeCommentaire.Add(commentaire);
            }

            return listeCommentaire;
        }

        private void AjoutCommentaire(string hash, string commentaire)
        {
            m_con.Open();
            DateTime maintenant = DateTime.Now;
            string sqlmaintenant = maintenant.ToString("yyyy-MM-dd HH:mm:ss");

            var insert = new SqlCommand("INSERT INTO tableCommentaire (Username,Hash,Commentaire,DateTime) VALUES  ('"+ User.Identity.Name + "','" + hash
                 + "','" + commentaire + "','" + sqlmaintenant + "')", m_con);
            insert.ExecuteNonQuery();
            m_con.Close();    

        }
    }
}

