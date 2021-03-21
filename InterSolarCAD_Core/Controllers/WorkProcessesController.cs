using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;

namespace InterSolarCAD_Core.Controllers
{
    public class WorkProcessesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkProcessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkProcesses
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkProcess.ToListAsync());
        }

        // GET: WorkProcesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Desc,FontAwsome,Id,Order")] WorkProcess workProcess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workProcess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workProcess);
        }

        // GET: WorkProcesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workProcess = await _context.WorkProcess.FindAsync(id);
            if (workProcess == null)
            {
                return NotFound();
            }
            return View(workProcess);
        }

        // POST: WorkProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Desc,FontAwsome,Id,Order")] WorkProcess workProcess)
        {
            if (id != workProcess.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workProcess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkProcessExists(workProcess.Id))
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
            return View(workProcess);
        }

        // GET: WorkProcesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workProcess = await _context.WorkProcess
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workProcess == null)
            {
                return NotFound();
            }

            return View(workProcess);
        }

        // POST: WorkProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workProcess = await _context.WorkProcess.FindAsync(id);
            _context.WorkProcess.Remove(workProcess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkProcessExists(int id)
        {
            return _context.WorkProcess.Any(e => e.Id == id);
        }
    }
}
