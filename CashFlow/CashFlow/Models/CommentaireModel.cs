using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace CashFlow.Models
{
    public class CommentaireModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Commentaire { get; set; }
        public DateTime Date { get; set; }
    }

}