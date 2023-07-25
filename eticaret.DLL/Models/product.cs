using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class product
    {
        public int id { get; set; }
        public int categoriID { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal? salePrice { get; set; }
        public decimal? basePrice { get; set; }
        public string details { get; set; }
        public decimal? stock { get; set; }
        public DateTime? creatingDate { get; set; }
        public string tags { get; set; }
        public int? popularity { get; set; }
        public bool isActive { get; set; }
    }
}
