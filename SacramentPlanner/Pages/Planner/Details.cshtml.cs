﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Data;
using SacramentPlanner.Models;

namespace SacramentPlanner.Pages.Planner
{
    public class DetailsModel : PageModel
    {
        private readonly SacramentPlanner.Data.SacramentPlannerContext _context;

        public DetailsModel(SacramentPlanner.Data.SacramentPlannerContext context)
        {
            _context = context;
        }

        public SacramentPlan SacramentPlan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SacramentPlan = await _context.SacramentPlanner.FirstOrDefaultAsync(m => m.SacramentPlannerId == id);

            if (SacramentPlan == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}