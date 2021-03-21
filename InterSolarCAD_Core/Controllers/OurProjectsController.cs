using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Web;
using Microsoft.AspNetCore.Mvc;

namespace InterSolarCAD_Core.Controllers
{
    public class OurProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OurProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}