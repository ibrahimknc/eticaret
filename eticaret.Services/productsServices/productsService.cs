using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.productsServices.Dto;
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
                { "title", null },
                { "productView", null },
                { "averageRating", null }
            };

        public Dictionary<string, object> getProduct(Guid id, string ip)
        {
            try
            {
                var responseProduct = _dbeticaretContext.products.Include(c => c.Category).AsQueryable().FirstOrDefault(x => x.id == id);

                if (responseProduct.isActive == true)
                {
                    var responseComments = _dbeticaretContext.comments.Include(c => c.User).AsQueryable().Where(x => x.productID == id & x.isActive == true).ToList();
                    var averageRating = _dbeticaretContext.comments.Where(c => c.productID == id).Average(c => (double?)c.rating) ?? 0;
                    var responsePIL = _dbeticaretContext.productsIMGs.AsQueryable().Where(x => x.productID == id & x.isActive == true).ToList();
                    var relatedProducts = _dbeticaretContext.products.AsQueryable().Where(x => x.id != id & x.isActive == true & x.categoriID == responseProduct.categoriID).OrderByDescending(x => x.creatingTime).Select(x => new
                    {
                        Product = x,
                        commentCount = _dbeticaretContext.comments.Count(c => c.productID == x.id),
                        averageRating = _dbeticaretContext.comments
                            .Where(c => c.productID == x.id)
                            .Average(c => (double?)c.rating) ?? 0
                    }).Take(5).ToList();

                    #region Add ProductViews 
                    var isProductView = _dbeticaretContext.productViews.Any(x => x.productID == id && x.ip == ip);
                    if (!isProductView)
                    {
                        ProductViews pv = new ProductViews()
                        {
                            ip = ip,
                            productID = id,
                            isActive = true,
                            creatingTime = DateTime.UtcNow,
                            updatedTime = DateTime.UtcNow
                        };
                        _dbeticaretContext.productViews.Add(pv);
                        _dbeticaretContext.SaveChanges();
                    }
                    var countProductView = _dbeticaretContext.productViews.Where(x => x.productID == id).Count();
                    #endregion

                    response["type"] = "success";
                    response["data"] = responseProduct;
                    response["productImageList"] = responsePIL;
                    response["relatedProducts"] = relatedProducts;
                    response["comments"] = responseComments;
                    response["categoryName"] = responseProduct.Category.name;
                    response["title"] = responseProduct.name;
                    response["categoryID"] = responseProduct.categoriID;
                    response["productView"] = countProductView;
                    response["averageRating"] = averageRating;
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

        public Dictionary<string, object> updateComment(Guid userID, Guid productID, int rating, string detail)
        {
            try
            {
                Comment comment = new Comment()
                {
                    userID = userID,
                    productID = productID,
                    rating = rating,
                    detail = detail,
                    isActive = true,
                    creatingTime = DateTime.UtcNow,
                    updatedTime = DateTime.UtcNow
                };
                _dbeticaretContext.comments.Add(comment);
                var isCompleted = _dbeticaretContext.SaveChanges();

                if (isCompleted > 0)
                {
                    response["type"] = "succcess"; response["message"] = "Yorum Başarıyla Eklendi.";
                }
                else
                {
                    response["type"] = "error"; response["message"] = "";
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
