using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.productCheckoutServices.Dto;
using Microsoft.EntityFrameworkCore;
using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace eticaret.Services.productCheckoutServices
{
    public class productCheckoutService : IproductCheckoutService
    {
        readonly dbeticaretContext _dbeticaretContext;

        public productCheckoutService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null}
            };

        public Dictionary<string, object> updatePayment(productCheckoutDto productCheckoutDto, List<ProductBasket> basket)
        {
            try
            {
                int totalQuantity = 0;
                decimal? totalshippingAmount = 0;
                decimal? totalPayment = 0;
                foreach (var item in basket)
                {
                    var product = _dbeticaretContext.products.Include(c => c.Category).Include(c => c.Shop).AsQueryable().FirstOrDefault(x => x.id == item.productID);

                    if (product.isActive == false || product.Category.isActive == false || product.Shop.isActive == false)
                    {
                        response["type"] = "error"; response["message"] = "<b>" + product.name + "</b> Ürününü Satıştan Kalkmıştır. Lütfen Sepetten Çıkarınız.";
                        return response;
                    }
                    if (product.stock < Convert.ToDecimal(item.quantity))
                    {
                        response["type"] = "error"; response["message"] = "<b>" + product.name + "</b> Ürününün Stoğu [ <b>" + (product.stock % 1 == 0 ? Convert.ToInt32(product.stock) : product.stock) + "</b> ] Kalmıştır. Lütfen Sepeti Güncelleyiniz.";
                        return response;
                    }
                    if (product.salePrice != item.price)
                    {
                        response["type"] = "error"; response["message"] = "<b>" + product.name + "</b> Ürününün Fiyatı Değişmiştir. Güncel Fiyat [ <b>" + (product.salePrice % 1 == 0 ? Convert.ToInt32(product.salePrice) : product.stock) + "</b> ] Lütfen Sepetten Çıkarıp Geri Ekleyiniz.";
                        return response;
                    }

                    totalQuantity += item.quantity;
                    totalshippingAmount += item.shippingAmount;
                    totalPayment += item.price;
                }
                ProductCheckout data = new ProductCheckout()
                {
                    userID = productCheckoutDto.userID,
                    billingCountry = productCheckoutDto.billingCountry,
                    billingFirstName = productCheckoutDto.billingFirstName,
                    billingLastName = productCheckoutDto.billingLastName,
                    billingCompanyName = productCheckoutDto.billingCompanyName,
                    billingAddress = productCheckoutDto.billingAddress,
                    billingCity = productCheckoutDto.billingCity,

                    shippingCountry = productCheckoutDto.shippingCountry,
                    shippingFirstName = productCheckoutDto.shippingFirstName,
                    shippingLastName = productCheckoutDto.shippingLastName,
                    shippingTitle = productCheckoutDto.shippingTitle,
                    shippingAddress = productCheckoutDto.shippingAddress,
                    shippingCity = productCheckoutDto.shippingCity,

                    totalQuantity = totalQuantity,
                    totalshippingAmount = totalshippingAmount,
                    isPayment = true,
                    totalPayment = totalPayment,
                    status = 0
                };
                _dbeticaretContext.productCheckouts.Add(data);
                _dbeticaretContext.SaveChanges();

                List<ProductBasket> ProductBasket = new List<ProductBasket>();

                foreach (var item in basket)
                {
                    ProductBasket pb = new ProductBasket()
                    {
                        ProductCheckoutID = data.id,
                        productID = item.productID,
                        name = item.name,
                        image = item.image,
                        price = item.price,
                        stock = item.stock,
                        shippingAmount = item.shippingAmount,
                        quantity = item.quantity,
                        ProductCheckout = data,
                        isActive = true,
                        creatingTime = DateTime.UtcNow
                    }; 
                    _dbeticaretContext.productBaskets.Add(pb); 

                    var clProduct = _dbeticaretContext.products.AsQueryable().FirstOrDefault(x => x.id == item.productID);
                    clProduct.stock -= item.quantity;
                } 
                _dbeticaretContext.SaveChanges();
                response["type"] = "success"; response["message"] = "✔Sipariş Başarılı.";
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
