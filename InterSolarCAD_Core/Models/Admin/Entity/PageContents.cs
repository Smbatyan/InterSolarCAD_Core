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
    public class PageContents : BaseEntity
    {
        [Required(ErrorMessage = "Site title is Required")]
        [DisplayName("Site title")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Short description is Required")]
        [DisplayName("Short description")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string ShortDesc { get; set; }

        //[Required(ErrorMessage = "Long description is Required")]
        [DisplayName("Long description")]
        [StringLength(10000)]
        public string LongDesc { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Slider Photo/Video")]
        [StringLength(5000)]
        public string SliderVideoURL { get; set; }

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
                    var fileName = "background" + Path.GetExtension(value.FileName);
                    var filePath = Path.Combine(Directory.GetParent("wwwroot").FullName, @"wwwroot\Images", fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        value.CopyTo(fileStream);
                    }

                    SliderVideoURL = "/Images/" + fileName;
                }
            }
        }

        [ScaffoldColumn(false)]
        [DisplayName("Team image")]
        [StringLength(5000)]
        public string OurTeamImage { get; set; }

        [NotMapped]
        public IFormFile OurTeamImageFile
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {

                    var fileName = "ourTeam" + Path.GetExtension(value.FileName);
                    var filePath = Path.Combine(Directory.GetParent("wwwroot").FullName, @"wwwroot\Images", fileName);
                    
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        value.CopyTo(fileStream);
                    }

                    OurTeamImage = "/Images/" + fileName;
                }
            }
        }

        [Required(ErrorMessage = "Footer text is Required")]
        [DisplayName("Footer text")]
        [StringLength(500)]
        public string FooterText { get; set; }

        [StringLength(500)]
        [DisplayName("Facebook URL")]
        public string Facebook { get; set; }

        [StringLength(500)]
        [DisplayName("Linkedin Url")]
        public string Linkedin { get; set; }
    }
}