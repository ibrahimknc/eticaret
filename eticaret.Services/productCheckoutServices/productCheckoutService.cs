using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.productCheckoutServices.Dto;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public Dictionary<string, object> updatePayment(productCheckoutDto productCheckoutDto, List<ProductBasket> basket, string IP)
        {
            try
            {
                var selUser = _dbeticaretContext.users.AsSingleQuery().FirstOrDefault(x => x.id == productCheckoutDto.userID);
                int totalQuantity = 0;
                decimal? totalshippingAmount = 0;
                decimal? totalPayment = 0;
                foreach (var item in basket)
                {
                    if (selUser.isActive == false)
                    {
                        response["type"] = "error"; response["message"] = "Hesabının Banlanmıştır Alışveriş Yapamazsınız.";
                        return response;
                    }

                    var product = _dbeticaretContext.products.Include(c => c.Category).Include(c => c.Shop).AsQueryable().FirstOrDefault(x => x.id == item.productID);

                    if (product.isActive == false || product.Category.isActive == false || product.Shop.isActive == false)
                    {
                        response["type"] = "error"; response["message"] = "<b>" + product.name + "</b> Ürünü Satıştan Kalkmıştır. Lütfen Sepetten Çıkarınız.";
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
                    totalPayment += (item.price * (item.quantity));
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
                    isPayment = false,
                    totalPayment = totalPayment,
                    status = 0
                };

                #region IyziPay 
                CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
                request.Locale = Locale.EN.ToString();
                request.ConversationId = data.id.ToString();
                request.Price = (data.totalPayment).ToString().Replace(",", ".");
                request.PaidPrice = (data.totalPayment + data.totalshippingAmount).ToString().Replace(",", "."); //İndirim, vergi gibi değerlerin dahil edildiği, vade farkı öncesi tutar değeri.
                request.Currency = Currency.TRY.ToString();
                request.BasketId = data.id.ToString();
                request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
                request.CallbackUrl = veriyoneticisi.projectSettings["siteUrl"] + "/response";


                #region Taksit işlemleri
                //List<int> enabledInstallments = new List<int>();
                //enabledInstallments.Add(2);
                //enabledInstallments.Add(3);
                //enabledInstallments.Add(6);
                //enabledInstallments.Add(9);
                //request.EnabledInstallments = enabledInstallments;
                #endregion

                #region Müşteri Bilgisi 
                Buyer buyer = new Buyer();
                buyer.Id = selUser.id.ToString();
                buyer.Name = selUser.firstName;
                buyer.Surname = selUser.lastName;
                buyer.GsmNumber = selUser.phone;
                buyer.Email = selUser.email;
                buyer.IdentityNumber = "00000000000";
                buyer.LastLoginDate = selUser.lastLoginDate?.ToString("yyyy-MM-dd HH:mm:ss");
                buyer.RegistrationDate = selUser.creatingTime.ToString("yyyy-MM-dd HH:mm:ss");
                buyer.RegistrationAddress = selUser.address == null ? "Null" : selUser.address;
                buyer.Ip = IP;
                buyer.City = "Null";
                buyer.Country = "Null";
                buyer.ZipCode = "Null";
                request.Buyer = buyer;
                #endregion

                #region Nakliye-Gönderi Adresi
                Address shippingAddress = new Address();
                shippingAddress.ContactName = data.shippingLastName + " " + data.shippingFirstName;
                shippingAddress.City = data.shippingCity;
                shippingAddress.Country = data.shippingCountry;
                shippingAddress.Description = data.shippingAddress;
                shippingAddress.ZipCode = "00000";
                request.ShippingAddress = shippingAddress;
                #endregion

                #region Fatura Adresi
                Address billingAddress = new Address();
                billingAddress.ContactName = data.billingFirstName + " " + data.billingLastName;
                billingAddress.City = data.billingCity;
                billingAddress.Country = data.billingCountry;
                billingAddress.Description = data.billingAddress;
                billingAddress.ZipCode = "00000";
                request.BillingAddress = billingAddress;
                #endregion

                #region Sepetteki ürün Listesi
                List<BasketItem> basketItems = new List<BasketItem>();

                foreach (var item in basket)
                {
                    var product = _dbeticaretContext.products.Include(c => c.Category).Include(s => s.Shop).AsQueryable().FirstOrDefault(x => x.id == item.productID);
                    BasketItem basketItem = new BasketItem();
                    basketItem.Id = product.id.ToString();
                    basketItem.Name = item.name + "( " + item.quantity + " )";
                    basketItem.Category1 = product.Shop.name;
                    basketItem.Category2 = product.Category.name;
                    basketItem.ItemType = BasketItemType.PHYSICAL.ToString(); //BasketItemType.VIRTUAL.ToString(sanal ürün)
                    basketItem.Price = (item.price * item.quantity).ToString();
                    basketItems.Add(basketItem);
                }
                request.BasketItems = basketItems;
                #endregion

                CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, veriyoneticisi.GetOptionsForPaymentMethod("iyzipay"));
                string respStr = checkoutFormInitialize.CheckoutFormContent;
                #endregion

                if (respStr != null)
                {
                    string pattern = @"token:""([^""]+)"""; // Token değerini yakalayacak desen
                    Match match = Regex.Match(respStr, pattern);

                    if (match.Success)
                    {
                        data.token = match.Groups[1].Value;
                    }
                    _dbeticaretContext.productCheckouts.Add(data);
                    _dbeticaretContext.SaveChanges();

                    response["type"] = "success"; response["data"] = respStr;
                }
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }

        public Dictionary<string, object> responseCheck(string token)
        {
            try
            {
                RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
                request.Token = token;
                CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, veriyoneticisi.GetOptionsForPaymentMethod("iyzipay"));
                if (checkoutForm.PaymentStatus == "SUCCESS")
                {

                    //    foreach (var item in basket)
                    //    {
                    //        ProductBasket pb = new ProductBasket()
                    //        {
                    //            ProductCheckoutID = data.id,
                    //            productID = item.productID,
                    //            name = item.name,
                    //            image = item.image,
                    //            price = item.price,
                    //            stock = item.stock,
                    //            shippingAmount = item.shippingAmount,
                    //            quantity = item.quantity,
                    //            ProductCheckout = data,
                    //            isActive = true,
                    //            creatingTime = DateTime.UtcNow
                    //        };
                    //        _dbeticaretContext.productBaskets.Add(pb);

                    //        var clProduct = _dbeticaretContext.products.AsQueryable().FirstOrDefault(x => x.id == item.productID);
                    //        clProduct.stock -= item.quantity;
                    //    }
                    //    _dbeticaretContext.SaveChanges();
                      response["type"] = "success"; response["message"] = "✔ Ödeme Başarılı.";

                }
                else
                {
                    response["type"] = "error"; response["message"] = "✘ Ödeme Başarısız.";
                } 
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
