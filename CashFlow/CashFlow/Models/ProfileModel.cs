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
    public class ProfileModel
    {
        [Required]
        [DisplayName("Nom complet")]
        public string NomComplet { get; set; }      
       
        public string Location { get; set; }

        [DisplayName("Image de profile")]
        public Image ImageProfile { get; set; }

        [DisplayName("Biographie/Description")]
        public string Description { get; set; }

        [DisplayName("Handle Twitter")]
        public string nomTwitter { get; set; }

        [DisplayName("Lien Facebook")]
        public string lienFacebook { get; set; }

        [DisplayName("Youtube/Google +")]
        public string nomGooglePlus { get; set; }

        [DisplayName("Site Web")]
        public string lienSiteWeb { get; set; }

    }
}