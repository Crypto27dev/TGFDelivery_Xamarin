using System.Collections.Generic;
using WinPizzaData;

namespace TGFDelivery.Models.ServiceModel
{
    public class DealsModel
    {
        public List<WPMealDeal> Products { get; set; }

        public List<string> RoleList { get; set; }

        public bool IsStoreClosed { get; set; }
    }
}
