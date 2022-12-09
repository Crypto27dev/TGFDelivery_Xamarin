using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WinPizzaData;

namespace TGFDelivery.Models.ServiceModel
{
    public class DealModel 
    {
        // OfferOrderLine
        public OfferOrderLine OfferOrderLine { get; set; }

        // Is all offer item picked
        public bool IsAllItemPicked { get; set; }

        public List<string> RoleList { get; set; }

        public string PriceFormatting(decimal Price)
        {
            return Price.ToString((CultureInfo)CultureInfo.CurrentCulture) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
        }
    }
}
