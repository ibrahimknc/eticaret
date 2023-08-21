using System;
using System.Collections.Generic; 

namespace eticaret.Services.myordersServices
{
    public interface ImyordersService
    {
        public Dictionary<string, object> getMyorders(Guid userID, int page, int itemsPerPage, string search, int listSorting);

    }
}
