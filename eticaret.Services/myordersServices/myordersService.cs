using System;
using eticaret.Data;
using eticaret.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Services.myordersServices
{
    public class myordersService : ImyordersService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public myordersService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null},
                { "c", null}
            };
        public Dictionary<string, object> getMyorders(Guid userID, int page, int itemsPerPage, string search, int listSorting)
        {
            try
            {
                var selProductCheckouts = _dbeticaretContext.productCheckouts
                    .Where(x =>
                    (listSorting == 0 || (listSorting > 0 && listSorting - 1 == x.status)) &&
                        x.isActive == true &&
                        x.isPayment == true &&
                        x.userID == userID &&
                        (string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(search)))
                    .ToList();

                var productCheckoutIds = selProductCheckouts.Select(y => y.id).ToList();

                var productBaskets = _dbeticaretContext.productBaskets.Include(x => x.Product).Include(x => x.Product.Shop)
                    .Where(basket => productCheckoutIds.Contains(basket.ProductCheckoutID))
                    .ToList();

                var productCheckoutsWithProducts = selProductCheckouts.Select(productCheckout => new ProductCheckout
                {
                    id = productCheckout.id,
                    userID = userID,
                    isActive = productCheckout.isActive,
                    creatingTime = productCheckout.creatingTime,
                    updatedTime = productCheckout.updatedTime,

                    billingCountry = productCheckout.billingCountry,
                    billingFirstName = productCheckout.billingFirstName,
                    billingLastName = productCheckout.billingLastName,
                    billingCompanyName = productCheckout.billingCompanyName,
                    billingCity = productCheckout.billingCity,
                    billingAddress = productCheckout.billingAddress,

                    shippingCountry = productCheckout.shippingCountry,
                    shippingFirstName = productCheckout.shippingFirstName,
                    shippingLastName = productCheckout.shippingLastName,
                    shippingTitle = productCheckout.shippingTitle,
                    shippingAddress = productCheckout.shippingAddress,
                    shippingCity = productCheckout.shippingCity,

                    token = productCheckout.token,
                    isPayment = productCheckout.isPayment,
                    totalPayment = productCheckout.totalPayment,
                    totalshippingAmount = productCheckout.totalshippingAmount,
                    totalQuantity = productCheckout.totalQuantity,
                    status = productCheckout.status,

                    ProductBasket = productBaskets
                        .Where(basket => basket.ProductCheckoutID == productCheckout.id)
                        .ToList()
                }).OrderByDescending(x => x.creatingTime);

                var count = productCheckoutsWithProducts.Count();
                var responseList = productCheckoutsWithProducts.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

                response["type"] = "success"; response["data"] = responseList;
                response["c"] = count;
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }

            return response;
        }
    }
}
