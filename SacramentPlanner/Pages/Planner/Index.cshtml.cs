using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Data;
using SacramentPlanner.Models;
using Newtonsoft.Json;
using System.IO;

namespace SacramentPlanner.Pages.Planner
{
    public class IndexModel : PageModel
    {
        private readonly SacramentPlanner.Data.SacramentPlannerContext _context;

        public IndexModel(SacramentPlanner.Data.SacramentPlannerContext context)
        {
            _context = context;
        }

        public IList<SacramentPlan> SacramentPlan { get;set; }

        public async Task OnGetAsync()
        {
            SacramentPlan = await _context.SacramentPlanner.ToListAsync();
            
        }
    }
}
