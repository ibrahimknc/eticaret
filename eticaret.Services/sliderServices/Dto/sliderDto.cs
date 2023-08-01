using System; 

namespace eticaret.Services.sliderServices.Dto
{
    public class sliderDto
    {
        public Guid id { get; set; } 
        public bool isActive { get; set; }
        public DateTime creatingTime { get; set; }
        public DateTime? updatedTime { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int? rank { get; set; }
    }
}
