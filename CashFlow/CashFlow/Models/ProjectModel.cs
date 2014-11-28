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
        [Required]
        public string Titre { get; set; }
        public string Hash { get; set; }
        public string Createur { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string UniteMonetaire;


        public string Image { get; set; }
        public DateTime DateDepart { get; set; }
        [DisplayName("Date limite")]
        [Required]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "Date invalide")]
        public string DateString { get; set; }
        public DateTime DateFin { get; set; }
        [DisplayName("Montant demandé")]
        [Required]
        [RegularExpression(@"^\d+$")]
        public string MontantString { get; set; }
        public int MontantRecu { get; set; }
        public int MontantRequis { get; set; }

        [DisplayName("Monnaie")]
        [Required]
        public string Monnaie { get; set; }

        public class ValeurMonnaie
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<ValeurMonnaie> ListeMonnaie = new List<ValeurMonnaie>
        {
            new ValeurMonnaie { Id = 0, Value = "CAD"},
            new ValeurMonnaie { Id = 1, Value = "USD"},
            new ValeurMonnaie { Id = 2, Value = "RUB"},
            new ValeurMonnaie { Id = 3, Value = "EUR"},
            new ValeurMonnaie { Id = 4, Value = "AUD"},
            new ValeurMonnaie { Id = 5, Value = "GBP"},
            new ValeurMonnaie { Id = 6, Value = "MXN"},
            new ValeurMonnaie { Id = 7, Value = "BRL"},
            new ValeurMonnaie { Id = 8, Value = "JPY"},
            new ValeurMonnaie { Id = 9, Value = "INR"},
            new ValeurMonnaie { Id = 10, Value = "PHP"},
            new ValeurMonnaie { Id = 11, Value = "CNY"},
            new ValeurMonnaie { Id = 12, Value = "ARS"},
            new ValeurMonnaie { Id = 13, Value = "JMD"},
            new ValeurMonnaie { Id = 14, Value = "KRW"},
            new ValeurMonnaie { Id = 15, Value = "SEK"},
        };

        [DisplayName("Catégorie")]
        [Required]
        public int Categorie { get; set; }

        public class ValeurCategorie
        {
            public int Id { get; set; }
            public string Value { get; set; }

        }
        public IEnumerable<ValeurCategorie> ListeCategories = new List<ValeurCategorie>
        {
            new ValeurCategorie { Id = 0, Value = "Art" },
            new ValeurCategorie { Id = 1, Value = "Jeux" },
            new ValeurCategorie { Id = 3, Value = "Musique" },
            new ValeurCategorie { Id = 4, Value = "Technologie" },
            new ValeurCategorie { Id = 5, Value = "Sport" },
            new ValeurCategorie { Id = 6, Value = "Film" },
            new ValeurCategorie { Id = 7, Value = "Mode" },
            new ValeurCategorie { Id = 8, Value = "Publications" },
            new ValeurCategorie { Id = 9, Value = "Journalisme" },
            new ValeurCategorie { Id = 10, Value = "Théatre" },
            new ValeurCategorie { Id = 11, Value = "Alimentation" },
            new ValeurCategorie { Id = 12, Value = "Transport" }
        };

        [DisplayName("Ville")]
        [Required]
        public int Ville { get; set; }

        public class ValeurVille
        {
            public int Id { get; set; }
            public string Value { get; set; }

        }
        public IEnumerable<ValeurVille> ListeVille = new List<ValeurVille>
        {
            new ValeurVille { Id = 0, Value = "Montréal" },
            new ValeurVille { Id = 1, Value = "Québec" },
            new ValeurVille { Id = 2, Value = "Toronto" },
            new ValeurVille { Id = 3, Value = "Ottawa" },
            new ValeurVille { Id = 4, Value = "Calgary" },
            new ValeurVille { Id = 5, Value = "Edmonton" },
            new ValeurVille { Id = 6, Value = "Saskatchewan" },
            new ValeurVille { Id = 7, Value = "Vancouver" },
            new ValeurVille { Id = 8, Value = "New York" },
            new ValeurVille { Id = 9, Value = "Boston" },
            new ValeurVille { Id = 10, Value = "New Jersey" },
            new ValeurVille { Id = 11, Value = "Miami" },
            new ValeurVille { Id = 12, Value = "Detroit" },
            new ValeurVille { Id = 13, Value = "Chicago" },
            new ValeurVille { Id = 14, Value = "Denver" },
            new ValeurVille { Id = 15, Value = "Las Vegas" },
            new ValeurVille { Id = 16, Value = "Tampa Bay" },
            new ValeurVille { Id = 17, Value = "Los Angeles" },
            new ValeurVille { Id = 18, Value = "San Francisco" },
            new ValeurVille { Id = 19, Value = "Mexico" },
            new ValeurVille { Id = 20, Value = "Hawai" },
            new ValeurVille { Id = 21, Value = "Paris" },
            new ValeurVille { Id = 22, Value = "Berlin" },
            new ValeurVille { Id = 23, Value = "Londres" },
            new ValeurVille { Id = 24, Value = "Amsterdam" },
            new ValeurVille { Id = 25, Value = "Barcelone" },
            new ValeurVille { Id = 26, Value = "Moscou" },
            new ValeurVille { Id = 27, Value = "Tokyo" },
            new ValeurVille { Id = 28, Value = "Pyongyang" },
            new ValeurVille { Id = 29, Value = "Lévis" },
        };

        public class ProjectDBContext : DbContext
        {
            public DbSet<NewProject> Project { get; set; }
        }
    }
}
