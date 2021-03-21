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
    public class PageContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PageContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PageContents
        public async Task<IActionResult> Index()
        {
            var retval = await _context.PageContents.FirstOrDefaultAsync();
            return View(retval);
        }

        // GET: PageContents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ShortDesc,LongDesc,FooterText,Facebook,Linkedin,Id,File,OurTeamImageFile")] PageContents pageContents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageContents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageContents);
        }

        // GET: PageContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageContents = await _context.PageContents.FindAsync(id);
            if (pageContents == null)
            {
                return NotFound();
            }
            return View(pageContents);
        }

        // POST: PageContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ShortDesc,LongDesc,FooterText,Facebook,Linkedin,Id,File,OurTeamImageFile")] PageContents pageContents)
        {
            if (id != pageContents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pc = _context.PageContents.AsNoTracking().First();
                    if (pageContents.SliderVideoURL != null)
                    {
                        string img = pc.SliderVideoURL;
                        if (img != null && System.IO.File.Exists(img))
                        {
                            System.IO.File.Delete(img);
                        }
                    }
                    else
                    {
                        pageContents.SliderVideoURL = pc.SliderVideoURL;
                    }

                    if (pageContents.OurTeamImage != null)
                    {
                        string img = pc.OurTeamImage;
                        if (img != null && System.IO.File.Exists(img))
                        {
                            System.IO.File.Delete(img);
                        }
                    }
                    else
                    {
                        pageContents.OurTeamImage = pc.OurTeamImage;
                    }

                    _context.Update(pageContents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageContentsExists(pageContents.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pageContents);
        }

        private bool PageContentsExists(int id)
        {
            return _context.PageContents.Any(e => e.Id == id);
        }
    }
}
