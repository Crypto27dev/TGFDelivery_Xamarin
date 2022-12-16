using System.ComponentModel.DataAnnotations;

namespace TGFDelivery.Models.ServiceModel
{
    public class UserInfoModel
    {
        public bool IsTelLogin { get; set; }

        public bool IsEmailLogin { get; set; }

        [Display(Name = "Telephone Number or User Name")]
        [Phone]
        public string TelOrUserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool IsAgree { get; set; }
    }
}
