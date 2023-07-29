 
using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class ProductIMG : BaseEntitiy
	{ 
        public int productID { get; set; }
        public string url { get; set; }
    }
}
