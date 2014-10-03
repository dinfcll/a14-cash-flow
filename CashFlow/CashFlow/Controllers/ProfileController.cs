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
        //
        // GET: /Profile/

        public ActionResult Profile()
        {            
            return View();                             
        }

        [HttpPost]
        public ActionResult Profile(ProfileModel model)
        {
            if (model.nomTwitter[0] != '@')
            {
                model.nomTwitter = '@' + model.nomTwitter;
            }
            TempData["info"] = "Votre profile a été modifié !";
            return RedirectToAction("Index", "Home");
        }

    }
}
