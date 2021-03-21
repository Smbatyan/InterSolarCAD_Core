using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class MapStates : BaseEntity
    {
        public string StateCode { get; set; }

        [NotMapped]
        public string StateName { get; set; }
    }
}
