using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlow.Models;
using System.Data.SqlClient;
using System.Data;

namespace CashFlow.Controllers
{
    public class ProfileController : Controller
    {
        ProfileModel modelPro = new ProfileModel();
        NewProject.ProjectDBContext dbProjet = new NewProject.ProjectDBContext();
        SqlConnection m_con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\dbCashFlow.mdf;Integrated Security=True;User Instance=True");    
        public ActionResult Profile()
        {
            SqlCommand toutesDonnees = new SqlCommand();
            SqlDataReader reader;

            toutesDonnees.CommandText = "SELECT * FROM tableUtilisateur WHERE Username ='" + User.Identity.Name + "'";
            toutesDonnees.CommandType = CommandType.Text;
            toutesDonnees.Connection = m_con;

            m_con.Open();
            ProfileModel aideUser = new ProfileModel();
            object valeur = toutesDonnees.ExecuteScalar();
            if((bool)valeur)
            {
                reader = toutesDonnees.ExecuteReader();
                aideUser.NomComplet = reader.GetString(3);
                aideUser.Description = reader.GetString(4);
                aideUser.ImageProfile = reader.GetString(5);
                aideUser.Location = reader.GetString(4); 
                aideUser.nomTwitter = reader.GetString(4); 
                aideUser.lienFacebook = reader.GetString(4);
                aideUser.lienYoutube = reader.GetString(4); 
            }
            
            
            return View(aideUser);                             
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
        public ActionResult Profile(ProfileModel model)
        {
            if (model.nomTwitter[0] != '@')
            {
                model.nomTwitter = '@' + model.nomTwitter;
            }
            TempData["info"] = "Votre profil a été modifié !";
            SqlCommand insert = new SqlCommand("INSERT INTO tableUtilisateur (Nom, Description, PhotoProfile, Location, Twitter, Facebook, GooglePlus) VALUES ('" + model.NomComplet + "','" + model.Description + "','" + model.ImageProfile
                 + "','" + model.Location + "','" + model.nomTwitter + "','" + model.lienFacebook + "','" + model.lienYoutube +"')", m_con);
            insert.ExecuteNonQuery();
            return RedirectToAction("Index", "Home");
            
        }

    }
}
