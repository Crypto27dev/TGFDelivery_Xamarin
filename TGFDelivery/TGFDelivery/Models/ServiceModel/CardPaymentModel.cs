namespace TGFDelivery.Models.ServiceModel
{
    public class CardPaymentModel
    {
        public string StoreID { get; set; }

        //[Required]
        //public string NameOnCard { get; set; }

        //[Required]
        //public string CardNumber { get; set; }

        //[RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$", ErrorMessage = "Expiry Date has an invalid format.")]
        //[Required]
        //public string ExpiryDate { get; set; }

        //[RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "CV2 has an invalid format.")]
        //[Required]
        //public string CV2 { get; set; }

        //public string Postcode { get; set; }

        public string ErrorMessage { get; set; }

        public string payment_method_nonce { get; set; }
        public static string ConvertSymbleToCurrency(string CurrencySymble)
        {
            string hh = "£";
            switch (CurrencySymble)
            {
                case "£":
                    hh = "GBP";
                    break;
                case "€":
                    hh = "EUR";
                    break;
            }
            return hh;
        }
    }
}
