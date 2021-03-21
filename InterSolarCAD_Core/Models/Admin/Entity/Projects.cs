using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class Project : BaseInfo
    {
        [ScaffoldColumn(false)]
        [DisplayName("Images")]
        public string Image { get; set; }

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

                    Image = "/Images/" + fileName;
                }
            }
        }

        [DisplayName("Show in main page")]
        public bool MainPage { get; set; }
    }

}