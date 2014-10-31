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
            object[] aide = new object[10];
            if(valeur != null)
            {
                reader = toutesDonnees.ExecuteReader();
                reader.Read();
                reader.GetValues(aide);
                aideUser.NomComplet = aide.ElementAt(3).ToString();
                aideUser.Description = aide.ElementAt(4).ToString();
                aideUser.ImageProfile = aide.ElementAt(5).ToString();
                aideUser.Location = aide.ElementAt(6).ToString();
                aideUser.nomTwitter = aide.ElementAt(7).ToString();
                aideUser.lienFacebook = aide.ElementAt(8).ToString();
                aideUser.lienYoutube = aide.ElementAt(9).ToString();
            }

            m_con.Close();
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
            m_con.Open();
            SqlCommand insert = new SqlCommand("UPDATE tableUtilisateur SET Nom='"+ model.NomComplet + "',Description='"+ model.Description +"',PhotoProfile='"+ model.ImageProfile + "',Location='" + model.Location+"',Twitter='"+model.nomTwitter+"',Facebook='"+ model.lienFacebook +"',GooglePlus='"+model.lienYoutube + "'WHERE Username = '"+ User.Identity.Name + "'" , m_con);
            insert.ExecuteNonQuery();
            m_con.Close();
            return RedirectToAction("Index", "Home");
            
        }

    }
}
