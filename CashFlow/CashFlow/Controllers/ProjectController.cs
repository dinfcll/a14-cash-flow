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
                return View(new NewProject());
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Project(NewProject model)
        {
            model.DateFin = Convert.ToDateTime(model.DateString);
            model.DateDepart = DateTime.Today;
            TempData["info"] = "Votre projet " + model.Titre + " est désormais lancé! Le financement prendra fin le " 
                + model.DateFin.ToLongDateString() + ".";
            return RedirectToAction("Index", "Home", model);
        }
    }
}
