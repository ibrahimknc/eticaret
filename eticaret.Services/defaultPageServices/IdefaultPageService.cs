using eticaret.Services.defaultPageServices.Dto; 
using System.Collections.Generic; 

namespace eticaret.Services.defaultPageServices
{
    public interface IdefaultPageService
    {
        public List<lastCommentsDto> getLastComments(string number);
        public List<lastProductsDto> getLastProducts(string number);
    }
}
