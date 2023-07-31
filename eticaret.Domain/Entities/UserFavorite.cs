using System;

namespace eticaret.Domain.Entities
{
    public partial class UserFavorite : BaseEntitiy
	{ 
        public Guid userID { get; set; }
        public Guid productID { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
