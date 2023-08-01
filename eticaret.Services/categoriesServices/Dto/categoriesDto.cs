using System; 

namespace eticaret.Services.categoriesServices.Dto
{
    public class categoriesDto
    {
        public Guid id { get; set; }
        public bool isActive { get; set; }
        public DateTime creatingTime { get; set; }
        public DateTime? updatedTime { get; set; }
        public string name { get; set; }
    }
}
