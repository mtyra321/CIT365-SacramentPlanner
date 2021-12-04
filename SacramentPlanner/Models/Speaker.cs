using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SacramentPlanner.Models
{
    public class Speaker
    {
        [Key]
        public int SpeakerId { get; set; }

        public String Name { get; set; }
        public String Topic { get; set; }

        public int SacramentPlannerId { get; set; }
        
        [ForeignKey("SacramentPlannerId")]
        public SacramentPlan SacramentPlan { get; set; }
        }
}
