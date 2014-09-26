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

        public ActionResult Project()
        {
            if (User.Identity.Name != "")
                return View(new ProjectModel());
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Project(ProjectModel model)
        {
            TempData["info"] = "Votre projet " + model.Titre + " est désormais lancé!";
            return RedirectToAction("Index", "Home", model);
        }
    }
}
