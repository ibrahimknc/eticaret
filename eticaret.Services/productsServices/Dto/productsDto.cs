﻿using System; 

namespace eticaret.Services.productsServices.Dto
{
    public class productsDto
    {
        public Guid id { get; set; }  
        public bool isActive { get; set; }
        public DateTime creatingTime { get; set; } 
        public DateTime? updatedTime { get; set; }
        public Guid categoriID { get; set; }
        public decimal? shippingAmount { get; set; } = 0;

        public string name { get; set; }
        public string image { get; set; }
        public decimal salePrice { get; set; }
        public decimal? basePrice { get; set; }
        public string details { get; set; }
        public decimal? stock { get; set; }
        public string tags { get; set; }
        public int? popularity { get; set; }
    }
}
