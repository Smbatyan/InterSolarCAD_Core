using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class WorkProcess : BaseEntity
    {
        [Required(ErrorMessage = "Work process title is Required")]
        [DisplayName("Title")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [DisplayName("Description")]
        [StringLength(300)]
        public string Desc { get; set; }

        [Required(ErrorMessage = "Symbol is Required")]
        [DisplayName("Symbol")]
        [StringLength(20)]
        public string FontAwsome { get; set; }

        [Required(ErrorMessage = "Order is Required")]
        public int Order { get; set; }
    }
}