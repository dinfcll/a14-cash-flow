using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class CommentaireModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Commentaire { get; set; }
        public DateTime Date { get; set; }
    }
}