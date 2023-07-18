using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class category
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool? isActive { get; set; }
    }
}
