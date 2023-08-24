using eticaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace eticaret.Services.shopServices
{
    public class shopService : IshopService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public shopService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }

        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null},
                { "title", null },
                { "titleIMG", null },
                { "c", null },
            };
        public Dictionary<string, object> getProduct(Guid shopID, int page, int itemsPerPage, string search, int listSorting)
        {
            try
            {
                var pattern = "\\b" + (string.IsNullOrEmpty(search) ? null : Regex.Escape(search)) + "\\b";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                var products = _dbeticaretContext.products.ToList();
                var query = products.Where(x =>
                (listSorting <= 4 | (listSorting == 5 && x.stock > 0)) &
                x.isActive == true &
                x.shopID == shopID &
                (string.IsNullOrEmpty(search) | (!string.IsNullOrEmpty(search) &
                ((regex.IsMatch(x.name)) |
                (regex.IsMatch(x.tags)) |
                (regex.IsMatch(x.details)))
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
                var shop = _dbeticaretContext.shops.FirstOrDefault(x => x.id == shopID);

                response["type"] = "success"; response["data"] = responseList;
                response["c"] = count; response["title"] = shop?.name ?? ""; response["titleIMG"] = shop?.image ?? "";

            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
