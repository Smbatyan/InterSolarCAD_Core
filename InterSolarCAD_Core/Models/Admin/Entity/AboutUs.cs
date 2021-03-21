using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class AboutUs
    {
        [Key]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(1250)]
        [DisplayName("Logo")]
        public string ImageURL { get; set; }

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
                    string guid = Guid.NewGuid().ToString();
                    var fileName = guid + Path.GetExtension(value.FileName);
                    var filePath = Path.Combine(Directory.GetParent("wwwroot").FullName, @"wwwroot\Images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        value.CopyTo(fileStream);
                    }

                    ImageURL = "/Images/" + fileName;
                }
            }
        }

        [DisplayName("Description")]
        public string LongDesc { get; set; }
    }
}