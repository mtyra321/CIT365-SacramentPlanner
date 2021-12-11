using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using SacramentPlanner.Data;
using SacramentPlanner.Models;

namespace SacramentPlanner.Pages.Planner
{
    public class CreateModel : PageModel
    {
        private readonly SacramentPlanner.Data.SacramentPlannerContext _context;
        //public IQueryable<Hymn> HymnList { get; set; }

        public CreateModel(SacramentPlanner.Data.SacramentPlannerContext context)
        {
            _context = context;
            Speakers = new List<Speaker>();
        }

        public async void PopulateHymns()
        {
            String line;
            String data = "{";
            List<Hymn> HymnList = new List<Hymn>();
            List<SelectListItem> Hymns = new List<SelectListItem>();
            try
            {
                StreamReader sr = new StreamReader("./JSON/Hymns.json");
                line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                    data += line;
                }
               
                sr.Close();
                var details = JObject.Parse(data);

                for (int i = 1; i <= 341; i++)
                {
                   Hymn hymn = new Hymn((string)details[i.ToString()]["name"]);
                   _context.Hymn.Add(hymn);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
                await _context.SaveChangesAsync();
            }
            
        }
        public IActionResult OnGet()
        {

            if (!_context.Hymn.Any())
            {
                PopulateHymns();
            }

            var HymnList = _context.Hymn.Select(h => new { HymnId = h.HymnId, HymnValue = $"{h.HymnId} - {h.Name}" }).ToList();
            var NoHymn = new { HymnId = 0, HymnValue = "No Hymn Selected" };
            HymnList.Insert(0, NoHymn);

            SelectList HymnSelectList = new SelectList(HymnList, "HymnId", "HymnValue");
            ViewData["HymnsList"] = HymnSelectList;

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
            var openingHymnId = Request.Form["HymnsListOpening"];
            var sacramentHymnId = Request.Form["HymnsListSacrament"];
            var intermediateHymnId = Request.Form["HymnsListIntermediate"];
            var closingHymnId = Request.Form["HymnsListClosing"];
            
            
            var HymnList = from h in _context.Hymn
                       select h;

            var OpeningHymn = HymnList.FirstOrDefault(x => x.HymnId == Int32.Parse(openingHymnId));
            var SacramentHymn = HymnList.FirstOrDefault(x => x.HymnId == Int32.Parse(sacramentHymnId));
            var IntermediateHymn = HymnList.FirstOrDefault(x => x.HymnId == Int32.Parse(intermediateHymnId));
            var ClosingHymn = HymnList.FirstOrDefault(x => x.HymnId == Int32.Parse(closingHymnId));

            SacramentPlan.OpeningHymn = OpeningHymn;
            SacramentPlan.SacramentHymn = SacramentHymn;
            SacramentPlan.IntermediateHymn = IntermediateHymn;
            SacramentPlan.ClosingHymn = ClosingHymn;
            //SacramentPlan.OpeningHymn = openingHymnName;

            SacramentPlan.CreationDate = DateTime.Now;


            _context.SacramentPlanner.Add(SacramentPlan);
            await _context.SaveChangesAsync();
            String speakerName = "";
            Speaker speaker= new Speaker(); ;
            int x = 0;
            while (speakerName != null) 
            {
                speakerName =   Request.Form["speakerName"+x];
                if (speakerName == null)
                    break;

                speaker = new Speaker();
                speaker.Name = speakerName;
                speaker.Topic = Request.Form["speakerTopic" + x];
                speaker.SacramentPlannerId = SacramentPlan.SacramentPlannerId;
                _context.Speaker.Add(speaker);
                await _context.SaveChangesAsync();
                x++;
            }
           
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
