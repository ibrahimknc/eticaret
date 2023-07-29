using System;

namespace eticaret.Services.logServices.Dto
{
	public class logDto
	{
		public Guid id { get; set; } 
		public bool isActive { get; set; }
		public DateTime creatingTime { get; set; } 
		public DateTime? updatedTime { get; set; }
		public string userID { get; set; }
		public int type { get; set; }
		public string ip { get; set; }
		public string note { get; set; }
	}
}
