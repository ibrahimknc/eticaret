using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class userFavorite
    {
        public int id { get; set; }
        public int? userID { get; set; }
        public int? productID { get; set; }
        public DateTime? creatingDate { get; set; }
    }
}
