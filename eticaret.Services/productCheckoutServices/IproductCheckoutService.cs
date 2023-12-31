﻿using eticaret.Domain.Entities;
using eticaret.Services.productCheckoutServices.Dto;
using System;
using System.Collections.Generic;

namespace eticaret.Services.productCheckoutServices
{
    public interface IproductCheckoutService
    {
        public Dictionary<string, object> updatePayment(productCheckoutDto productCheckoutDto, List<ProductBasket> ProductBasket, string IP);
        public Dictionary<string, object> responseCheck(string token);
        public Dictionary<string, object> updateAddress(UserAddress userAddress); 
        public Dictionary<string, object> getUserAddress(Guid userID); 
    }
} 