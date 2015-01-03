using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcom.Models
{
    public class clsAddToCard
    {
        public string UserNamer { get; set; }
        public string UserEmail { get; set; }
        public string Telephonenumber { get; set; }
        public string CustomerComment { get; set; }
        public string Password { get; set; }
        public List<CardData> card { get; set; }
    }

    public class CardData
    {
        public string ProductNamer { get; set; }
        public string Amount { get; set; }
    }
}