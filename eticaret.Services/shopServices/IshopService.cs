using System;
using System.Collections.Generic; 

namespace eticaret.Services.shopServices
{
    public interface IshopService
    {
        public Dictionary<string, object> getProduct(Guid shopID, int page, int itemsPerPage, string search, int listSorting);
    }
}
