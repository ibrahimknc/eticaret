using System;

namespace eticaret.Domain.Entities
{
    public partial class ProductIMG : BaseEntitiy
	{ 
        public Guid productID { get; set; }
        public string url { get; set; }

        public Product Product { get; set; }
    }
}
