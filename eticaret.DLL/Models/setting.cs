using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class setting
    {
        public int id { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
    }
}
