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
        }

        public IActionResult OnGet()
        {
            String line;
            String data = "{";
            List<string> Hymns = new List<string>();
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
                    //new ListItem(myHymn.Hymn((string)details[i.ToString()]["name"]), myHymn.HymnId.ToString()(i)
                    try
                    {
                        Hymns.Add((string)details[i.ToString()]["name"]);
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
