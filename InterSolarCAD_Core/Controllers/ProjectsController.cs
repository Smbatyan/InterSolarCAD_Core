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
using System.IO;
using InterSolarCAD_Core.Models.Web;
using System.ComponentModel.DataAnnotations;

namespace InterSolarCAD_Core.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index(int pageNumber = 0)
        {
            OurProjectsVM vm = new OurProjectsVM(_context, pageNumber: pageNumber);

            return View(vm);
        }

        // GET: Projects
        public async Task<IActionResult> List()
        {
            return View(await _context.Project.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainPage,Title,ShortDesc,LongDesc,Id,File")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MainPage,Title,ShortDesc,LongDesc,Id,File")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string img = _context.Project.Where(x => x.Id == project.Id).AsNoTracking().FirstOrDefault()?.Image;
                    var filePath = Path.Combine(Directory.GetParent("wwwroot").FullName, @"wwwroot", img);

                    if (project.Image != null)
                    {
                        if (img != null && System.IO.File.Exists(img))
                        {
                            System.IO.File.Delete(img);
                        }
                    }
                    else
                    {
                        project.Image = img;
                    }

                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project.Image != null && System.IO.File.Exists(Directory.GetParent("wwwroot").FullName + "/wwwroot" + project.Image))
            {
                System.IO.File.Delete(Directory.GetParent("wwwroot").FullName + "/wwwroot" + project.Image);
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
