using System; 

namespace eticaret.Services.settingsServices.Dto
{
	public class settingsDto
	{
		public Guid id { get; set; }
		public bool isActive { get; set; }
		public DateTime creatingTime { get; set; }
		public DateTime? updatedTime { get; set; }
		public string type { get; set; }
		public string title { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string address { get; set; }
		public string keywords { get; set; }
		public string description { get; set; }
	}
}
