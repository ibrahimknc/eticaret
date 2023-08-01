using eticaret.Services.viewCategoryServices.Dto; 
using System.Collections.Generic; 

namespace eticaret.Services.viewCategoryServices
{
    public interface IviewCategoryService
    {
        public List<viewCategoryDto> geCategories();
    }
}
