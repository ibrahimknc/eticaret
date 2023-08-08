using eticaret.Data;
using eticaret.Services.searchService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Services.searchServices
{
    public class searchService : IsearchService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public searchService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null}, 
                { "c", null },
            };

        public Dictionary<string, object> getSearchProduct(int page, int itemsPerPage, string search, int listSorting)
        {
            try
            {

                var query = _dbeticaretContext.products.Where(x =>
                (listSorting <= 4 | (listSorting == 5 && x.stock > 0)) &
                x.isActive == true & 
                (string.IsNullOrEmpty(search) | (!string.IsNullOrEmpty(search) &
                ((x.name.Contains(search)) |
                (x.tags.Contains(search)) |
                (x.details.Contains(search)))
                )))
                    .Select(x => new
                    {
                        Product = x,
                        commentCount = _dbeticaretContext.comments.Count(c => c.productID == x.id),
                        averageRating = _dbeticaretContext.comments
                            .Where(c => c.productID == x.id)
                            .Average(c => (double?)c.rating) ?? 0
                    });

                switch (listSorting)
                {
                    case 1:
                        query = query.OrderBy(x => x.Product.salePrice);
                        break;
                    case 2:
                        query = query.OrderByDescending(x => x.Product.salePrice);
                        break;
                    case 3:
                        query = query.OrderBy(x => x.Product.name);
                        break;
                    case 4:
                        query = query.OrderByDescending(x => x.Product.name);
                        break;
                    default:
                        query = query.OrderByDescending(x => x.Product.id);
                        break;
                }

                var count = query.Count();
                var responseList = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(); 

                response["type"] = "success"; response["data"] = responseList;
                response["c"] = count; 

            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
