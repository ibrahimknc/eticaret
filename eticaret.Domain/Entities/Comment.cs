using System; 

namespace eticaret.Domain.Entities
{
    public partial class Comment : BaseEntitiy
    {
        public Guid productID { get; set; }
        public Guid userID { get; set; }
        public string detail { get; set; }
        public int rating { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
       
    }
}
