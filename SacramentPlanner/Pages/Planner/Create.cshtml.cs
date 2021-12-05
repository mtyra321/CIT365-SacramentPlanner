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
            Speakers = new List<Speaker>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SacramentPlan SacramentPlan { get; set; }
        public List<Speaker> Speakers { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // create the speaker and hymn objects from the form inputs based on the names;
            String openingHymnName = Request.Form["openingHymnName"];
            String sacramentHymnName = Request.Form["sacramentHymnName"];
            String intermediateHymnName = Request.Form["intermediateHymnName"];
            String closingHymnName = Request.Form["closingHymnName"];
            Hymn openingHymn = new Hymn(openingHymnName);
            Hymn intermediateHymn;          
            Hymn sacramentHymn = new Hymn(sacramentHymnName);
            if (intermediateHymnName != "")
            {
                 intermediateHymn = new Hymn(intermediateHymnName);
                _context.Hymn.Add(intermediateHymn);

            }
            Hymn closingHymn = new Hymn(closingHymnName);
            _context.Hymn.Add(openingHymn);
            _context.Hymn.Add(sacramentHymn);

            _context.Hymn.Add(closingHymn);
           
            SacramentPlan.CreationDate = DateTime.Now;


            _context.SacramentPlanner.Add(SacramentPlan);
            await _context.SaveChangesAsync();
            String speakerName = "";
            Speaker speaker= new Speaker(); ;
            int x = 0;
            while (speakerName != null) 
            {
                speakerName = Request.Form["speakerName"+x];
                speaker = new Speaker();
                speaker.Name = speakerName;
                speaker.SacramentPlannerId = SacramentPlan.SacramentPlannerId;
                _context.Speaker.Add(speaker);
                x++;
            }
           
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
