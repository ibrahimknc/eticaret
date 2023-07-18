using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class log
    {
        public int id { get; set; }
        public int? userID { get; set; }
        public byte? type { get; set; }
        public DateTime? date { get; set; }
        public string ip { get; set; }
        public string note { get; set; }
    }
}
