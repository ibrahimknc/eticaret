using System;
using System.Collections.Generic; 

namespace eticaret.Services.categoriesServices
{
    public interface IcategoriesService
    {
        public Dictionary<string, object> getCategoriList(Guid id, int page, int itemsPerPage, string search, string price, int listSorting, int rating,int isStock);
    }
}
