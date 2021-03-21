using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class Testimonials : BaseEntity
    {
        [StringLength(100)]
        [DisplayName("Company/Person name")]
        public string Company { get; set; }

        [StringLength(350)]
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string ShortDesc { get; set; }
    }
}