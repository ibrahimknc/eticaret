using eticaret.Domain.Entities;
using eticaret.Services.productCheckoutServices.Dto;
using System.Collections.Generic;

namespace eticaret.Services.productCheckoutServices
{
    public interface IproductCheckoutService
    {
        public Dictionary<string, object> updatePayment(productCheckoutDto productCheckoutDto, List<ProductBasket> ProductBasket);
    }
}
