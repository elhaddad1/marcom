using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcom.Models
{
    public class clsBrandMenu
    {
        public string ulid { get; set; }
        public string ulclass { get; set; }
        public string ultitle { get; set; }
        public string ulalt { get; set; }
        public string lirel { get; set; }
        public string ahref { get; set; }
        public string litext { get; set; }
        public bool isRoot { get; set; }
    }
}