using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticaret.Domain.Entities
{
    public partial class ProductCheckout : BaseEntitiy
    {
        public Guid userID { get; set; }
        #region billing (Fatura Adresi)
        public string billingCountry { get; set; }
        public string billingFirstName { get; set; }
        public string billingLastName { get; set; }
        public string billingCompanyName { get; set; }
        public string billingAddress { get; set; }
        public string billingCity { get; set; }
        #endregion
        #region shipping  (Nakliyat Adresi)
        public string shippingCountry { get; set; }
        public string shippingFirstName { get; set; }
        public string shippingLastName { get; set; }
        public string shippingTitle { get; set; }
        public string shippingAddress { get; set; }
        public string shippingCity { get; set; }
        #endregion
        public string token { get; set; } // İşlem Numarası
        public bool? isPayment { get; set; } // Ödeme Yapıldımı
        public decimal? totalPayment { get; set; } // Toplam Ödeme Miktarı 
        public decimal? totalshippingAmount { get; set; } // Toplam Kargo Ödeme Miktarı 
        public int totalQuantity { get; set; }
        public int status { get; set; } // 0=Ödeme Alındı, 1=Hazırlanıyor, 2=Kargoya Verildi, 3=Teslim Edildi
        public User User { get; set; } 
        public ICollection<ProductBasket> Products { get; set; } 
    }

     
}

