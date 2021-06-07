using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using InterSolarCAD_Core.Models.Web;

namespace InterSolarCAD_Core.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? pageNumber = 0)
        {
            NewsVM vm = new NewsVM(_context, pageNumber: pageNumber);

            return View(vm);
        }

        
        [AllowAnonymous]
        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsVM vm = default;

            try
            {
                vm = new NewsVM(_context, id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            
            return View(vm);
        }

        // GET: News
        public async Task<IActionResult> List()
        {
            return View(await _context.News.ToListAsync());
        }


        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainPage,Title,ShortDesc,LongDesc,Id,File")] News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MainPage,Title,ShortDesc,LongDesc,Id,File")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string img = _context.News.Where(x => x.Id == news.Id).AsNoTracking().FirstOrDefault()?.MainImage;
                    var filePath = Path.Combine(Directory.GetParent("wwwroot").FullName, @"wwwroot", img);

                    if (news.MainImage != null)
                    {
                        if (img != null && System.IO.File.Exists(img))
                        {
                            System.IO.File.Delete(img);
                        }
                    }
                    else
                    {
                        news.MainImage = img;
                    }

                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news.MainImage != null && System.IO.File.Exists(Directory.GetParent("wwwroot").FullName + "/wwwroot" + news.MainImage))
            {
                System.IO.File.Delete(Directory.GetParent("wwwroot").FullName + "/wwwroot" + news.MainImage);
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
