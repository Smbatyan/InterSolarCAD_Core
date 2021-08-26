using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Web
{
    public class ViewModel
    {
        public AboutUs AboutUs { get; set; }

        public PageContents PageContents { get; set; }

        public List<TeamMember> TeamMembers { get; set; }

        public ContactUs ContactUs { get; set; }

        public List<Project> Projects { get; set; }

        public List<Job> Jobs{ get; set; }

        public List<Testimonials> Testimonials { get; set; }

        public List<Clients> Clients { get; set; }

        public List<MapStates> MapStates { get; set; }

        public List<WorkProcess> WorkProcess { get; set; }
        
        public List<News> News { get; set; }

        private ApplicationDbContext db;

        public ViewModel(ApplicationDbContext _db)
        {
            db = _db;

            AboutUs = db.AboutUs.FirstOrDefault();

            ContactUs = db.ContactUs.FirstOrDefault();

            PageContents = db.PageContents.FirstOrDefault();

            TeamMembers = db.TeamMember.ToList();

            Jobs = db.Job.ToList();

            Projects = db.Project.Where(x=> x.MainPage).ToList();

            Testimonials = db.Testimonials.ToList();

            Clients = db.Clients.ToList();

            MapStates = db.MapStates.ToList();

            WorkProcess = db.WorkProcess.ToList();

            News = db.News.Where(x => x.MainPage).ToList();
        }
    }


}