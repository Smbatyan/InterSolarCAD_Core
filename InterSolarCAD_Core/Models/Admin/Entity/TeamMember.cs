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
    public class TeamMember : BaseEntity
    {
        [Required(ErrorMessage = "First name is Required")]
        [DisplayName("First name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is Required")]
        [DisplayName("Last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Position")]
        [StringLength(50)]
        public string Position { get; set; }

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

                    ProfilePicture = "/Images/" + fileName;
                }
            }
        }

        [ScaffoldColumn(false)]
        [DisplayName("Profile picture")]
        public string ProfilePicture { get; set; }
    }
}