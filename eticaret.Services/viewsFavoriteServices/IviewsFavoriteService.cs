using eticaret.Services.viewsFavoriteServices.Dto; 
using System.Collections.Generic; 

namespace eticaret.Services.viewsFavoriteServices
{
    public interface IviewsFavoriteService
    {
        public List<viewsFavoriteDto> GetFavoriteProducts(string whichDay);
    }
}
