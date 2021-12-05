using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Hymn
    {
        [Key]
        public int HymnId { get; set; }

        public String Name { get; set; }

        public Hymn( string name)
        {
            Name = name;
        }


    }
}
