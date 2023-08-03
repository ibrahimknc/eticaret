using eticaret.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Services.productsServices
{
    public class productsService : IproductsService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public productsService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null},
                { "productImageList", null},
                { "comments", null},
                { "categoryName", null },
                { "categoryID", null },
                { "title", null }
            };

        public Dictionary<string, object> getProduct(Guid id)
        {
            try
            {
                var responseProduct = _dbeticaretContext.products.AsQueryable().FirstOrDefault(x => x.id == id);
                var responseCategory = _dbeticaretContext.categories.AsQueryable().FirstOrDefault(x => x.id == responseProduct.categoriID);
                var responseComments = _dbeticaretContext.comments.Include(c => c.User).AsQueryable().Where(x => x.productID == id & x.isActive == true).ToList();
                var responsePIL = _dbeticaretContext.productsIMGs.AsQueryable().Where(x => x.productID == id & x.isActive == true).ToList();

                if (responseProduct.isActive == true)
                {  
                    response["type"] = "success"; 
                    response["data"] = responseProduct; 
                    response["productImageList"] = responsePIL; 
                    response["comments"] = responseComments;
                    response["categoryName"] = responseCategory.name; 
                    response["title"] = responseProduct.name; 
                    response["categoryID"] = responseCategory.id;
                }
                else
                {
                    response["type"] = "inActive"; response["message"] = "";
                }

            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }

            return response;
        }
    }
}
