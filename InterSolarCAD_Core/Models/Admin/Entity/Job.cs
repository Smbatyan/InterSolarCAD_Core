using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class Job : BaseEntity
    {
        [StringLength(15000)]
        [DisplayName("Description")]
        public string Desc { get; set; }

        [StringLength(50)]
        public string Possition { get; set; }
    }
}