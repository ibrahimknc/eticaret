using System; 

namespace eticaret.Services.userServices.Dto
{
	public class userDto
	{
		public Guid id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public bool isActive { get; set; }

        public string phone { get; set; }
        public string address { get; set; }
        public DateTime? lastLoginDate { get; set; }
        public DateTime creatingTime { get; set; }
        public DateTime? updatedTime { get; set; }
    }
}
