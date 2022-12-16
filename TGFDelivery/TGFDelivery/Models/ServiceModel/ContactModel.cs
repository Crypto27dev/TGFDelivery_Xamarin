using System.ComponentModel.DataAnnotations;

namespace TGFDelivery.Models.ServiceModel
{
    public class ContactModel
    {
        [Required]
        public string CityPostcode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Observations { get; set; }
    }
}
