using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SacramentPlanner.Models
{
    public class SacramentPlan
    {
        [Key]
        public int SacramentPlannerId { get; set; }
        public String Presiding { get; set; }
        public String Conducting { get; set; }

        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }

        public Hymn OpeningHymn { get; set; }
        public Hymn SacramentHymn { get; set; }

        public Hymn? IntermediateHymn { get; set; }


        public Hymn ClosingHymn { get; set; }


        public String OpeningPrayer { get; set; }
        public String ClosingPrayer { get; set; }

    }
}
