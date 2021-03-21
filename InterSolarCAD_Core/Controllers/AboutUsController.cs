﻿using System;
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
    public class AboutUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AboutUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AboutUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutUs.ToListAsync());
        }

        // GET: AboutUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LongDesc,File")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LongDesc,File")] AboutUs aboutUs)
        {
            if (id != aboutUs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    aboutUs.LongDesc.Trim();

                    string img = _context.AboutUs.AsNoTracking().FirstOrDefault()?.ImageURL;
                    if (aboutUs.ImageURL != null)
                    {
                        if (img != null && System.IO.File.Exists(img))
                        {
                            System.IO.File.Delete(img);
                        }
                    }
                    else
                    {
                        aboutUs.ImageURL = img;
                    }

                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(aboutUs.Id))
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
            return View(aboutUs);
        }

        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutUs = await _context.AboutUs.FindAsync(id);
            _context.AboutUs.Remove(aboutUs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(int id)
        {
            return _context.AboutUs.Any(e => e.Id == id);
        }
    }
}
