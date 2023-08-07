using System;
using System.Collections.Generic;

namespace eticaret.Services.productsServices
{
    public interface IproductsService
    {
        public Dictionary<string, object> getProduct(Guid id, string ip);
        public Dictionary<string, object> updateComment(Guid userID, Guid productID, int rating, string detail);
       
    }
}
