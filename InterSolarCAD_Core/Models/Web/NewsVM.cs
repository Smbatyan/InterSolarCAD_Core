using InterSolarCAD_Core.Data;
using InterSolarCAD_Core.Models.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterSolarCAD_Core.Models.Web
{
    public class NewsVM
    {
        public PagedInfo PagedInfo { get; set; }

        public List<Project> Projects { get; set; }

        public PageContents PageContents { get; set; }

        public List<News> News { get; set; }

        public News NewsItem { get; set; }

        public NewsVM(ApplicationDbContext db, int? newsId = null, int? pageNumber = null)
        {
            PageContents = db.PageContents.First();

            if (newsId.HasValue)
            {
                News = db.News.OrderByDescending(x => x.Id).Take(5).ToList();
                NewsItem = db.News.Find(newsId);
                if (NewsItem is null)
                {
                    throw new KeyNotFoundException();
                }
            }
            else
            {
                PagedInfo = new PagedInfo();
                if (pageNumber.HasValue)
                {
                    PagedInfo.PageNumber = pageNumber.Value;
                }

                News = db.News.OrderByDescending(x => x.Id).Skip(PagedInfo.PageNumber * 6).Take(6).ToList();

                int totalCount = db.News.Count();

                PagedInfo.HasNextPage = totalCount - ((PagedInfo.PageNumber + 1) * 6) > 0;
            }
            
            Projects = db.Project.OrderByDescending(x => x.Id).Take(6).ToList();

        }
    }
}
