using eticaret.Data;
using eticaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace eticaret.Services.categoriesServices
{
    public class categoriesService : IcategoriesService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public categoriesService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }

        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null},
                { "c", null },
                { "name", null },
                { "tags", null }
            };

        public Dictionary<string, object> getCategoriList(Guid id, int page, int itemsPerPage, string search, string price, int listSorting, int rating, int isStock)
        {
            try
            {
                int startingPrice = 0;
                int endPrice = 0;
                bool filterPrice = false;
                if (!string.IsNullOrEmpty(price) & price != "0;10000")
                {
                    filterPrice = true;
                    startingPrice = Convert.ToInt32(price.Split(";")[0]);
                    endPrice = Convert.ToInt32(price.Split(";")[1]);
                }
                var pattern = "\\b" + (string.IsNullOrEmpty(search) ? null : Regex.Escape(search)) + "\\b";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                var products = _dbeticaretContext.products.ToList();

                var query = products.Where(x =>
                (isStock == 0 || (isStock == 1 && x.stock <= 0) || (isStock == 2 && x.stock > 0)) &
                (filterPrice == false | (filterPrice == true & x.salePrice >= startingPrice & x.salePrice <= endPrice)) &
                x.isActive == true &
                x.categoriID == id &
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
                    }).Where(x => rating == 0 || (rating > 0 && x.averageRating >= rating));

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
                var categoryName = _dbeticaretContext.categories.FirstOrDefault(x => x.id == id)?.name ?? "";
                var tags = _dbeticaretContext.products.Where(x => x.isActive == true && x.categoriID == id).Select(x => x.tags).ToList();

                response["type"] = "success"; response["data"] = responseList;
                response["c"] = count; response["name"] = categoryName; response["tags"] = tags;

            }
             catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
