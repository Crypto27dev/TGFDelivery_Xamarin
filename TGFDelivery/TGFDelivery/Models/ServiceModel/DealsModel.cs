using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
