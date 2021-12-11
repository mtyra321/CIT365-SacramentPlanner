using System;
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
    public class DeleteModel : PageModel
    {
        private readonly SacramentPlanner.Data.SacramentPlannerContext _context;

        public DeleteModel(SacramentPlanner.Data.SacramentPlannerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SacramentPlan SacramentPlan { get; set; }

        public IQueryable<Speaker> Speakers { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             
            SacramentPlan = await _context.SacramentPlanner.FindAsync(id);


            if (SacramentPlan != null)
            {

                //Speakers = from s in _context.Speaker
                //           select s;
                //Speakers = Speakers.Where(x => x.SacramentPlannerId == SacramentPlan.SacramentPlannerId).OrderByDescending(x => x.SpeakerId);
                //then loop through speakers and remove each one from context.

                Speakers = from s in _context.Speaker
                           select s;
                Speakers = Speakers.Where(x => x.SacramentPlannerId == SacramentPlan.SacramentPlannerId).OrderBy(x => x.SpeakerId);

                foreach (var Speaker in Speakers)
                {
                    _context.Speaker.Remove(Speaker);
                }
                await _context.SaveChangesAsync();

                _context.SacramentPlanner.Remove(SacramentPlan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
