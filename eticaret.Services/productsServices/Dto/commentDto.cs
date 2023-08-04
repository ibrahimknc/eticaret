using System; 

namespace eticaret.Services.productsServices.Dto
{
    public class commentDto
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public bool isActive { get; set; }
        public DateTime creatingTime { get; set; } = DateTime.UtcNow;
        public DateTime? updatedTime { get; set; }
        public Guid productID { get; set; }
        public Guid userID { get; set; }
        public string detail { get; set; }
        public int rating { get; set; }
    }
}
