using System.Collections.Generic; 

namespace eticaret.Services.searchService
{
    public interface IsearchService
    {
        public Dictionary<string, object> getSearchProduct(int page, int itemsPerPage, string search, int listSorting);
    }
}
