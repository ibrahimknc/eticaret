using System; 

namespace eticaret.Domain.Entities
{ 
    public partial class UserAddress : BaseEntitiy
    {
        public Guid userID { get; set; }
        public string title { get; set; } 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string city { get; set; } 

        public User User { get; set; }
    }

}
