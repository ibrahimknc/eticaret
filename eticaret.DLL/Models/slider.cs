using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class slider
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int? rank { get; set; }
        public bool? isActive { get; set; }
    }
}
