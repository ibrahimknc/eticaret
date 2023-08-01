using eticaret.Data;
using eticaret.Services.viewCategoryServices.Dto;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Services.viewCategoryServices
{
    public class viewCategoryService : IviewCategoryService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public viewCategoryService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        public List<viewCategoryDto> geCategories()
        {
            var result = _dbeticaretContext.categories
                       .Where(c => c.isActive == true)
                       .Join(_dbeticaretContext.products.Where(p => p.isActive == true),
                             c => c.id,
                             p => p.categoriID,
                             (c, p) => new { Category = c, Product = p })
                       .GroupBy(cp => new { cp.Category.id, cp.Category.name })
                       .Select(g => new viewCategoryDto // viewCategoryDto türüne dönüştürüyoruz
                       {
                           productCount = g.Count(),
                           id = g.Key.id,
                           name = g.Key.name
                       })
                       .OrderByDescending(g => g.productCount)
                       .ToList();

            return result;
        }
    }
}
