using System; 

namespace eticaret.Domain.Entities
{
    public partial class ProductViews : BaseEntitiy
    {
        public Guid productID { get; set; }
        public string ip { get; set; }

        public Product Product { get; set; }
    }
}
