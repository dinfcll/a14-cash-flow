using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace CashFlow.Models
{
    public class ListeCommentaireModel : List<CommentaireModel>
    {
        public List<CommentaireModel> Commentaire { get; set; }
    }
}
