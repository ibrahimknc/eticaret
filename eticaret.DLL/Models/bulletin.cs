using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class bulletin
    {
        public int id { get; set; }
        public string email { get; set; }
        public DateTime creatingDate { get; set; }
    }
}
