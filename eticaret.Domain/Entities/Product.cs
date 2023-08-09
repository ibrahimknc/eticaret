 
using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class Product : BaseEntitiy
	{ 
        public Guid categoriID { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal salePrice { get; set; }
        public decimal? basePrice { get; set; }
        public decimal? shippingAmount { get; set; } = 0; 
        public string details { get; set; }
        public decimal? stock { get; set; } 
        public string tags { get; set; }
        public int? popularity { get; set; }
        public Guid shopID { get; set; }

        public Category Category { get; set; }
        public Shop Shop { get; set; } 
    }
}
