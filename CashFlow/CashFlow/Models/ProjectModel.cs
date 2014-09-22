using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel;


namespace CashFlow.Models
{
    public class ProjectModel
    {
        [Required]
        public string Titre { get; set; }
        string Createur { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UniteMonetaire;
        public Image Img;
        DateTime DateDepart;
        [Required]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "Date invalide")]
        public DateTime DateFin;
        public int MontantRequis;

        [DisplayName("Catégorie du projet")]
        [Required]
        public string Categorie { get; set; }

        public class ValeurCategorie
        {
            public int ID { get; set; }
            public string Value { get; set; }

        }
        public IEnumerable<ValeurCategorie> ListeCategories = new List<ValeurCategorie>
        {
            new ValeurCategorie { ID = 0, Value = "Art" },
            new ValeurCategorie { ID = 1, Value = "Electronique" },
            new ValeurCategorie { ID = 2, Value = "Musique" }
        };

        [DisplayName("Choisir une ville")]
        [Required]
        public string Ville { get; set; }

        public class ValeurVille
        {
            public int ID { get; set; }
            public string Value { get; set; }

        }
        public IEnumerable<ValeurVille> ListeVille = new List<ValeurVille>
        {
            new ValeurVille { ID = 0, Value = "Montréal" },
            new ValeurVille { ID = 1, Value = "Québec" },
            new ValeurVille { ID = 2, Value = "Toronto" }
        };
    }
}