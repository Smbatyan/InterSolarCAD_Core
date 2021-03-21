using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;

namespace InterSolarCAD_Core.Models.Web
{
    public class OurProjectsVM
    {
        public List<Project> Projects { get; set; }

        public PageContents PageContents { get; set; }

        public List<News> News { get; set; }

        public News NewsItem { get; set; }

        public OurProjectsVM(ApplicationDbContext db, int? newsId = null)
        {
            PageContents = db.PageContents.First();

            News = db.News.OrderByDescending(x => x.Id).Take(5).ToList();

            if (newsId.HasValue)
            {
                NewsItem = db.News.Find(newsId.Value);
            }
            else
            {
                Projects = db.Project.OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
