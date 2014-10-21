using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlow.Models;
using RestSharp;

namespace CashFlow.Controllers
{
    public class ProfileController : Controller
    {
        ProfileModel modelPro = new ProfileModel();
        public ActionResult Profile()
        {
            return View(modelPro);                             
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
