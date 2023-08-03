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
                { "relatedProducts", null}, //ilgili ürünler
                { "comments", null},
                { "categoryName", null },
                { "categoryID", null },
                { "title", null }
            };

        public Dictionary<string, object> getProduct(Guid id)
        {
            try
            {
                var responseProduct = _dbeticaretContext.products.Include(c => c.Category).AsQueryable().FirstOrDefault(x => x.id == id); 
                var responseComments = _dbeticaretContext.comments.Include(c => c.User).AsQueryable().Where(x => x.productID == id & x.isActive == true).ToList();
                var responsePIL = _dbeticaretContext.productsIMGs.AsQueryable().Where(x => x.productID == id & x.isActive == true).ToList();
                var relatedProducts = _dbeticaretContext.products.AsQueryable().Where(x => x.id != id & x.isActive == true & x.categoriID == responseProduct.categoriID).OrderByDescending(x => x.creatingTime).Take(5).ToList();

                if (responseProduct.isActive == true)
                {  
                    response["type"] = "success"; 
                    response["data"] = responseProduct; 
                    response["productImageList"] = responsePIL; 
                    response["relatedProducts"] = relatedProducts;
                    response["comments"] = responseComments;
                    response["categoryName"] = responseProduct.Category.name; 
                    response["title"] = responseProduct.name; 
                    response["categoryID"] = responseProduct.categoriID;
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
