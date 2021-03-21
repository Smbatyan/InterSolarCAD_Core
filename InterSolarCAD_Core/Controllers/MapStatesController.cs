using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;
using Microsoft.AspNetCore.Authorization;

namespace InterSolarCAD_Core.Controllers
{
    [Authorize]
    public class MapStatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MapStatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MapStates
        public async Task<IActionResult> Index()
        {
            Dictionary<string, string> dictionaryCountry = new Dictionary<string, string>()
            {
      {"AL", "Alabama"},
      {"AK", "Alaska"},
      {"AZ", "Arizona"},
      {"AR", "Arkansas"},
      {"CA", "California"},
      {"CT", "Connecticut"},
      {"DE", "Delaware"},
      {"FL", "Florida"},
      {"GA", "Georgia"},
      {"HI", "Hawaii"},
      {"ID", "Idaho"},
      {"IL", "Illinois"},
      {"IN", "Indiana"},
      {"IA", "Iowa"},
      {"KS", "Kansas"},
      {"KY", "Kentucky"},
      {"LA", "Louisiana"},

      {"ME", "Maine"},
      {"MD", "Maryland"},
      {"MA", "Massachusetts"},
      {"MI", "Michigan"},
      {"MN", "Minnesota"},
      {"MS", "Mississippi"},
      {"MO", "Missouri"},
      {"MT", "Montana"},
      {"NE", "Nebraska"},
      {"NV", "Nevada"},
      {"NH", "New Hampshire"},
      {"NJ", "New Jersey"},
      {"NM", "New Mexico"},
      {"NY", "New York"},
      {"NC", "North Carolina"},
      {"ND", "North Dakota"},
      {"OK", "Oklahoma"},
      {"OR", "Oregon"},
      {"PA", "Pennsylvania"},
      {"RI", "Rhode Island"},
      {"SC", "South Carolina"},
      {"SD", "South Dakota"},
      {"TN", "Tennessee"},
      {"TX", "Texas"},
      {"UT", "Utah"},
      {"VT", "Vermont"},
      {"VA", "Virginia"},
      {"WA", "Washington"},
      {"WV", "West Virginia"},
      {"WI", "Wisconsin"},
      {"WY", "Wyoming"},
    };
            var existing = await _context.MapStates.ToListAsync();

            foreach (var item in existing)
            {
                item.StateName = dictionaryCountry[item.StateCode];
            }

            return View(existing);
        }

        // GET: MapStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MapStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateCode,Id")] MapStates mapStates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mapStates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mapStates);
        }

        // GET: MapStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapStates = await _context.MapStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mapStates == null)
            {
                return NotFound();
            }

            return View(mapStates);
        }

        // POST: MapStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mapStates = await _context.MapStates.FindAsync(id);
            _context.MapStates.Remove(mapStates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MapStatesExists(int id)
        {
            return _context.MapStates.Any(e => e.Id == id);
        }
    }
}
