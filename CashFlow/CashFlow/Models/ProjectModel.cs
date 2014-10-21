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
    public class NewProject
    {
        //Classe utilisée lorsqu'on veux créer un nouveau projet
        [Required]
        public string Titre { get; set; }
        public string Createur { get; set; }
        
        [Required]
        public string Description { get; set; }
        [Required]
        public string UniteMonetaire;
		
        public Image Image;
        public DateTime DateDepart;
        [DisplayName("Date limite")]
        [Required]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "Date invalide")]
        public string DateString { get; set; }
        public DateTime DateFin;
        [DisplayName("Montant demandé")]
        [Required]
        [RegularExpression(@"^\d+$")]
        public string MontantString { get; set; }
        public int MontantRequis;

        [DisplayName("Monnaie")]
        [Required]
        public string Monnaie { get; set; }

        public class ValeurMonnaie
        {
            public int ID { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<ValeurMonnaie> ListeMonnaie = new List<ValeurMonnaie>
        {
            new ValeurMonnaie { ID = 0, Value = "CAD"},
            new ValeurMonnaie { ID = 1, Value = "USD"},
            new ValeurMonnaie { ID = 2, Value = "RUB"},
            new ValeurMonnaie { ID = 3, Value = "EUR"},
            new ValeurMonnaie { ID = 4, Value = "AUD"},
            new ValeurMonnaie { ID = 5, Value = "GBP"},
            new ValeurMonnaie { ID = 6, Value = "MXN"},
            new ValeurMonnaie { ID = 7, Value = "BRL"},
            new ValeurMonnaie { ID = 8, Value = "JPY"},
            new ValeurMonnaie { ID = 9, Value = "INR"},
            new ValeurMonnaie { ID = 10, Value = "PHP"},
            new ValeurMonnaie { ID = 11, Value = "CNY"},
            new ValeurMonnaie { ID = 12, Value = "ARS"},
            new ValeurMonnaie { ID = 13, Value = "JMD"},
            new ValeurMonnaie { ID = 14, Value = "KRW"},
            new ValeurMonnaie { ID = 15, Value = "SEK"},
        };

        [DisplayName("Catégorie")]
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
            new ValeurCategorie { ID = 1, Value = "Jeux" },
            new ValeurCategorie { ID = 3, Value = "Musique" },
            new ValeurCategorie { ID = 4, Value = "Technologie" },
            new ValeurCategorie { ID = 5, Value = "Sport" },
            new ValeurCategorie { ID = 6, Value = "Film" },
            new ValeurCategorie { ID = 7, Value = "Mode" },
            new ValeurCategorie { ID = 1, Value = "Publications" },
            new ValeurCategorie { ID = 8, Value = "Journalisme" },
            new ValeurCategorie { ID = 9, Value = "Théatre" },
            new ValeurCategorie { ID = 10, Value = "Alimentation" },
            new ValeurCategorie { ID = 11, Value = "Transport" }
        };

        [DisplayName("Ville")]
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
            new ValeurVille { ID = 2, Value = "Toronto" },
            new ValeurVille { ID = 3, Value = "Ottawa" },
            new ValeurVille { ID = 4, Value = "Calgary" },
            new ValeurVille { ID = 5, Value = "Edmonton" },
            new ValeurVille { ID = 6, Value = "Saskatchewan" },
            new ValeurVille { ID = 7, Value = "Vancouver" },
            new ValeurVille { ID = 8, Value = "New York" },
            new ValeurVille { ID = 9, Value = "Boston" },
            new ValeurVille { ID = 10, Value = "New Jersey" },
            new ValeurVille { ID = 11, Value = "Miami" },
            new ValeurVille { ID = 12, Value = "Detroit" },
            new ValeurVille { ID = 13, Value = "Chicago" },
            new ValeurVille { ID = 14, Value = "Denver" },
            new ValeurVille { ID = 15, Value = "Nashville" },
            new ValeurVille { ID = 16, Value = "Tampa Bay" },
            new ValeurVille { ID = 17, Value = "Los Angeles" },
            new ValeurVille { ID = 18, Value = "San Francisco" },
            new ValeurVille { ID = 19, Value = "Mexico" },
            new ValeurVille { ID = 20, Value = "Hawai" },
            new ValeurVille { ID = 21, Value = "Paris" },
            new ValeurVille { ID = 22, Value = "Berlin" },
            new ValeurVille { ID = 23, Value = "Londres" },
            new ValeurVille { ID = 24, Value = "Amsterdam" },
            new ValeurVille { ID = 25, Value = "Barcelone" },
            new ValeurVille { ID = 26, Value = "Moscou" },
            new ValeurVille { ID = 27, Value = "Tokyo" },
            new ValeurVille { ID = 28, Value = "Pyongyang" },
            new ValeurVille { ID = 29, Value = "Lévis" },
        };

        public class ProjectDBContext : DbContext
        {
            public DbSet<NewProject> Project { get; set; }
        }
    }

    public class EditProject
    {
        //Classe utilisée lorsqu'on veut modifier les informations d'un projet déja existant
    }

}