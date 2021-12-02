using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SacramentPlanner.Data;
using SacramentPlanner.Models;

namespace SacramentPlanner.Pages.Planner
{
    public class CreateModel : PageModel
    {
        private readonly SacramentPlanner.Data.SacramentPlannerContext _context;

        public CreateModel(SacramentPlanner.Data.SacramentPlannerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SacramentPlan SacramentPlan { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // create the speaker and hymn objects from the form inputs based on the names;
            String openingHymnName = Request.Form["openingHymnName"];



            // _context.Speaker.Add(the speaker object);
            SacramentPlan.CreationDate = DateTime.Now;


            _context.SacramentPlanner.Add(SacramentPlan);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
