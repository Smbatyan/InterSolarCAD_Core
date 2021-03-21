using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;
using InterSolarCAD_Core.Helper;
using Microsoft.AspNetCore.Authorization;

namespace InterSolarCAD_Core.Controllers
{
    [Authorize]
    public class ContactFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactForm.ToListAsync());
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PhoneNumber,Country,City,ZIP,Message,Id")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                string title = contactForm.FirstName + " " + contactForm.LastName;

                string message = $"<p><b>FirstName</b>: {contactForm.FirstName}</p>" +
                                 $"<p><b>LastName</b>: {contactForm.LastName}</p>" +
                                 $"<p><b>Email</b>: {contactForm.Email}</p>" +
                                 $"<p><b>PhoneNumber</b>: {contactForm.PhoneNumber}</p>" +
                                 $"<p><b>Country</b>: {contactForm.Country}</p>" +
                                 $"<p><b>City</b>: {contactForm.City}</p>" +
                                 $"<p><b>ZIP</b>: {contactForm.ZIP}</p>" +
                                 $"<p><b>Date</b>: {DateTime.Now.ToLongDateString()}</p>" +
                                 $"<p><b>Message</b></p> <p>{contactForm.Message}</p>";

                await EmailService.SendEmailAsync(_context.ContactUs.First().AdminEmail, "Message from website user", message);
                _context.Add(contactForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactForm);
        }

        // GET: ContactForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactForm = await _context.ContactForm.FindAsync(id);
            _context.ContactForm.Remove(contactForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
            return _context.ContactForm.Any(e => e.Id == id);
        }
    }
}
