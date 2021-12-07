using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Data;
using SacramentPlanner.Models;

namespace SacramentPlanner.Pages.Planner
{
    public class EditModel : PageModel
    {
        private readonly SacramentPlanner.Data.SacramentPlannerContext _context;

        public EditModel(SacramentPlanner.Data.SacramentPlannerContext context)
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


            Speakers = from s in _context.Speaker
                       select s;
            Speakers = Speakers.Where(x => x.SacramentPlannerId == SacramentPlan.SacramentPlannerId).OrderBy(x => x.SpeakerId);



         






            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SacramentPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SacramentPlannerExists(SacramentPlan.SacramentPlannerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            String speakerName = "";
            Speaker speaker = new Speaker(); ;
            int x = 0;
            while (speakerName != null)
            {
                speakerName = Request.Form["speakerName" + x];
                if (speakerName == null)
                    break;

                speaker = new Speaker();
                speaker.Name = speakerName;
                speaker.Topic = Request.Form["speakerTopic" + x];
                speaker.SacramentPlannerId = SacramentPlan.SacramentPlannerId;
                _context.Speaker.Add(speaker);
                x++;
            }

            await _context.SaveChangesAsync();



            return RedirectToPage("./Index");
        }

        private bool SacramentPlannerExists(int id)
        {
            return _context.SacramentPlanner.Any(e => e.SacramentPlannerId == id);
        }
    }
}
