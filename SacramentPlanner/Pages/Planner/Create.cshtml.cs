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

        public CreateModel(SacramentPlanner.Data.SacramentPlannerContext context)
        {
            _context = context;
            Speakers = new List<Speaker>();
        }

        public IActionResult OnGet()
        {
            String line;
            String data = "{";
            List<Hymn> HymnList = new List<Hymn>();
            List<SelectListItem> Hymns = new List<SelectListItem>();
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("./JSON/Hymns.json");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    
                    //Read the next line
                    line = sr.ReadLine();
                    data += line;
                }
                //close the file
                sr.Close();

                var details = JObject.Parse(data);



                for (int i = 0; i < 341; i++)
                {
                    
                    try
                    {
                        //Hymn Hymn = new Hymn((string)details[i.ToString()]["name"]);
                        //Hymn.HymnId = i;
                        //HymnList.Add(Hymn);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            SelectList HymnSelectList = new SelectList(HymnList, "HymnId", "Name");
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
