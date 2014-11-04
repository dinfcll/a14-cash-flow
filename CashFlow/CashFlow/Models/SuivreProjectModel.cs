using System.Collections.Generic;

namespace CashFlow.Models
{
    public class SuivreProjectModel : List<NewProject>
    {
        public NewProject ListeNewProject { get; set; }
    }
}