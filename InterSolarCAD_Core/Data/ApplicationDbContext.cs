using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InterSolarCAD_Core.Models.Admin.Entity;

namespace InterSolarCAD_Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        } 
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.PageContents> PageContents { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.Job> Job { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.ContactUs> ContactUs { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.ContactForm> ContactForm { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.MapStates> MapStates { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.AboutUs> AboutUs { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.TeamMember> TeamMember { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.Project> Project { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.Testimonials> Testimonials { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.Clients> Clients { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.WorkProcess> WorkProcess { get; set; }
        public DbSet<InterSolarCAD_Core.Models.Admin.Entity.News> News { get; set; }
    }
}
