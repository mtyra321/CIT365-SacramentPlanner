using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Speaker
    {

        [Key]
        public int SpeakerId { get; set; }

        public String Name { get; set; }
        public String Topic { get; set; }


        public int SacramentPlannerId { get; set; }

    }
}
