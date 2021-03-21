using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class BaseInfo : BaseEntity
    {
        [Required(ErrorMessage = "Title is Required")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [StringLength(500)]
        [DisplayName("Short description")]
        public string ShortDesc { get; set; }

        [StringLength(5000)]
        [DisplayName("Long description")]
        public string LongDesc { get; set; }
    }
}