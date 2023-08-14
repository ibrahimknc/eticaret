using System; 

namespace eticaret.Domain.Entities
{
    public partial class ProductBasket : BaseEntitiy
    {
        public Guid ProductCheckoutID { get; set; }
        public Guid productID { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal price { get; set; }
        public decimal? stock { get; set; }
        public decimal? shippingAmount { get; set; }
        public int quantity { get; set; }

        public ProductCheckout ProductCheckout { get; set; }
}
}
