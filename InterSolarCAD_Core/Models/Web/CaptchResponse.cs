using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterSolarCAD_Core.Models.Web
{
    public class CaptchResponse
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string apk_package_name { get; set; }
    }
}
