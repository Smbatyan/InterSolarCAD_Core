using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;
using JW;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InterSolarCAD_Core.Models.Web
{
    public class OurProjectsVM
    {             
        public PagedInfo PagedInfo { get; set; }

        public List<Project> Projects { get; set; }

        public PageContents PageContents { get; set; }

        public List<News> News { get; set; }

        public News NewsItem { get; set; }

        public OurProjectsVM(ApplicationDbContext db, int? newsId = null, int? pageNumber = null)
        {
            PageContents = db.PageContents.First();

            News = db.News.OrderByDescending(x => x.Id).Take(5).ToList();

            if (newsId.HasValue)
            {
                NewsItem = db.News.Find(newsId.Value);
            }
            else
            {
                PagedInfo = new PagedInfo();

                if (pageNumber.HasValue)
                {
                    PagedInfo.PageNumber = pageNumber.Value;
                }

                int totalCount = db.Project.Count();

                Projects = db.Project.OrderByDescending(x => x.Id).Skip(PagedInfo.PageNumber * 6).Take(6).ToList();

                

                PagedInfo.HasNextPage = totalCount - ((PagedInfo.PageNumber+1) * 6) > 0;
            }
        }
    }
}
