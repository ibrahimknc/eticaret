using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class viewsFavorite
    {
        public int? favCount { get; set; }
        public int productID { get; set; }
        public string name { get; set; }
        public DateTime? creatingDate { get; set; }
        public string tags { get; set; }
        public decimal? stock { get; set; }
        public string details { get; set; }
        public decimal? basePrice { get; set; }
        public decimal? salePrice { get; set; }
        public int? categoriID { get; set; }
        public string categoriName { get; set; }
        public string image { get; set; }
    }
}
