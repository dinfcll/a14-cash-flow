﻿using System;
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
        //
        // GET: /Project/
        NewProject.ProjectDBContext dbProjet = new NewProject.ProjectDBContext();

        public ActionResult Project()
        {
            if (User.Identity.IsAuthenticated)
                return View(new NewProject());
            else
                //return RedirectToAction("Login", "Account");
                return View(new NewProject());
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
                TempData["TitreProjet"] = model.Titre;

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
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");
            con.Open();

            SqlCommand insert = new SqlCommand("INSERT INTO tableProject VALUES ('" + ChaineHasard() + "','" + model.Titre + "','" + model.Description
                 + "','" + model.Ville + "','0','" + model.MontantString + "','" + DateTime.Now.ToShortDateString() + "','" + model.DateString
                  + "','" + model.Categorie + "','" + model.Createur + "')", con);
            insert.ExecuteNonQuery();
            con.Close();
            
        }

    }
}
