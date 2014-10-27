using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlow.Models;

namespace CashFlow.Controllers
{
    public class ProfileController : Controller
    {
        ProfileModel modelPro = new ProfileModel();
        public ActionResult Profile()
        {
            return View(modelPro);                             
        }

        public ActionResult Verif()
        {
            return View(modelPro);
        }



        [HttpPost]
        public ActionResult Verif(ProfileModel Model)
        {
            modelPro.codeVerif= TempData["CodeVerif"].ToString();
            if (modelPro.codeVerif == Model.codeVerif)
            {
                TempData["info"] = "Votre profil a été vérifié !";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["info"] = "Erreur ! Veuillez entrer le bon code de vérification !";
                return RedirectToAction("Verif", "Profile");
            }
            
        }

        [HttpPost]
        public ActionResult Profile(ProfileModel Model)
        {
            if (Model.nomTwitter[0] != '@')
            {
                Model.nomTwitter = '@' + Model.nomTwitter;
            }
            TempData["info"] = "Votre profil a été modifié !";
            return RedirectToAction("Index", "Home");
        }

    }
}
