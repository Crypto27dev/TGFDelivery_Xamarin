using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WinPizzaData;

namespace TGFDelivery.Models.ServiceModel
{
    public enum PaymentStyle
    {
        Cash,
        Card
    }

    public class CheckOutModel
    {

        // Application name
        public string ApplicationName { get; set; }

        // StoreID
        public string StoreID { get; set; }

        public List<WinPizzaData.Address> AddressList { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "FirstName length can't be more than 50.")]
        public string FirstName { get; set; }

        [Required]
        [Phone(ErrorMessage = "Phone number has an invalid format.")]
        public string ContactNumber { get; set; }

        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [Required]
        [EmailAddress(ErrorMessage = "Email address has an invalid format.")]
        public string EmailAddress { get; set; }

        public string DriverInstructions { get; set; }

        public string PaymentMethod { get; set; }
        public string[] PaymentMethods = new[] { "Cash", "Card" };

        public string Address { get; set; }

        public string OrderType { get; set; }

        public string ErrorMessage { get; set; }

        public string AddressFormat { get; set; }

        public List<string> RoleList { get; set; }
    }
}
