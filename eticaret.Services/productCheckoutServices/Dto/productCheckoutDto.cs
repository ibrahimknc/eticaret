using System; 

namespace eticaret.Services.productCheckoutServices.Dto
{
    public class productCheckoutDto
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
    } 
}
