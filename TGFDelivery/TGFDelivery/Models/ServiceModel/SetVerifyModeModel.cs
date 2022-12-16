using System.ComponentModel.DataAnnotations;

namespace TGFDelivery.Models.ServiceModel
{
    public enum VerifyMethod
    {
        Phone,
        Email
    }

    public class SetVerifyModeModel
    {

        // Application name
        public string ApplicationName { get; set; }

        // StoreID
        public string StoreID { get; set; }

        [Required]
        [Phone(ErrorMessage = "Phone number has an invalid format.")]
        public string ContactNumber { get; set; }

        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [Required]
        [EmailAddress(ErrorMessage = "Email address has an invalid format.")]
        public string EmailAddress { get; set; }

        [Required]
        public VerifyMethod UserVerifyMethod { get; set; }

        public string ErrorMessage { get; set; }
    }
}
