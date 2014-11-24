using System.Collections.Generic;

namespace CashFlow.Models
{
    public class ListeCommentaireModel : List<CommentaireModel>
    {
        public CommentaireModel ListeCommentaire { get; set; }
    }
}
