using eticaret.Data; 
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Dictionary<string, object> getCategoriList(Guid id, int page, int itemsPerPage, string search, string price, int listSorting)
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

                var query = _dbeticaretContext.products.Where(x =>
                (listSorting <= 4 | (listSorting == 5 && x.stock > 0)) &
                (filterPrice == false | (filterPrice == true & x.salePrice >= startingPrice & x.salePrice <= endPrice)) &
                x.isActive == true &
                x.categoriID == id &
                (string.IsNullOrEmpty(search) | (!string.IsNullOrEmpty(search) &
                ((x.name.Contains(search)) |
                (x.tags.Contains(search)) |
                (x.details.Contains(search)))
                )));

                switch (listSorting)
                {
                    case 1:
                        query = query.OrderBy(x => x.salePrice);
                        break;
                    case 2:
                        query = query.OrderByDescending(x => x.salePrice);
                        break;
                    case 3:
                        query = query.OrderBy(x => x.name);
                        break;
                    case 4:
                        query = query.OrderByDescending(x => x.name);
                        break;
                    default:
                        query = query.OrderByDescending(x => x.id);
                        break;
                }

                var count = query.Count();
                var responseList = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
                var categoryName = _dbeticaretContext.categories.FirstOrDefault(x => x.id == id)?.name ?? "";
                var tags = _dbeticaretContext.products.Where(x => x.isActive == true && x.categoriID == id).Select(x => x.tags).ToList();

                response["type"] = "success"; response["data"] = responseList;
                response["c"] = count; response["name"] = categoryName; response["tags"] = tags;
 
            }
            catch {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
