using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterSolarCAD_Core.Models.Admin.Entity
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        [EmailAddress]
        [DisplayName("Email that receive messages from website users")]
        public string AdminEmail { get; set; }
    }
}