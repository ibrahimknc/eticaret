using System; 

namespace eticaret.Services.viewsFavoriteServices.Dto
{
    public class viewsFavoriteDto
    {
        public int FavCount { get; set; }
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public DateTime? CreatingTime { get; set; }
        public decimal? Stock { get; set; }
        public string Details { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public double averageRating { get; set; }
        public int? commentCount { get; set; }
    }
}
