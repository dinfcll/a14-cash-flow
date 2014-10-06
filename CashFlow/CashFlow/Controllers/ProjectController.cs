using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using CashFlow.Models;


namespace CashFlow.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        //dbCashFlowEntities1 dbCashFlow = new dbCashFlowEntities1();
        CashFlow.Models.NewProject.ProjectDBContext dbProjet = new CashFlow.Models.NewProject.ProjectDBContext();
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
                TempData["info"] = "Votre projet " + model.Titre + " est désormais lancé! Le financement prendra fin le "
                    + model.DateFin.ToLongDateString() + ".";
                NewProject p = dbProjet.Project.Find(0);
                //NewProject project = new.New
                //{
                //    Hash = ChaineHasard(),
                //    Titre = model.Titre,
                //    Description = model.Description,
                //    Categorie = model.Categorie,
                //    Location = model.Ville,    //a changer
                //    DateDebut = DateTime.Today,
                //    DateLimite = Convert.ToDateTime(model.DateString),
                //    MontantRecu = 0,
                //    MontantRequis = Convert.ToInt32(model.MontantString)                  
                //};
                
    //            TempData["info"] = "Votre projet " + project.Titre + " est désormais lancé! Le financement prendra fin le "
    //+ project.DateLimite.ToLongDateString() + ".";
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
    }
}
