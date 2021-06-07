using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class News : BaseEntity
    {
        public string Title { get; set; }

        public string ShortDesc { get; set; }

        public string LongDesc { get; set; }

        public string MainImage { get; set; }

        public bool MainPage { get; set; }

        //public DateTime CreateDate { get; set; }

        [NotMapped]
        public IFormFile File
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(value.FileName);
                    var filePath = Path.Combine(Directory.GetParent("wwwroot").FullName, @"wwwroot\Images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        value.CopyTo(fileStream);
                    }

                    MainImage = "/Images/" + fileName;
                }
            }
        }
    }
}
