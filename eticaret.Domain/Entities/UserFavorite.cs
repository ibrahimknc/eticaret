 
using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class UserFavorite : BaseEntitiy
	{ 
        public int userID { get; set; }
        public int productID { get; set; } 
    }
}
