using System.Collections.Generic;
using System.Globalization;
using WinPizzaData;

namespace TGFDelivery.Models.ServiceModel
{
    public class MenuModel
    {
        public string ShowGroup { get; set; }
        public string SelectedCategoryID { get; set; }

        public List<Category> CategoryList { get; set; }

        public Category ShowCategory { get; set; }

        // Product for CreateYourOwn
        public WPProduct Product4CreateYourOwn { get; set; }

        // Product for HalfNHalf
        public WPProduct Product4Half { get; set; }

        // Product for legend
        public WPProduct Product4Legend { get; set; }

        // Product for sale
        public WPProduct Product4Sale { get; set; }

        public Basket UserBasket { get; set; }

        // true: Call from first page, false: not
        public bool IsStart { get; set; }

        public bool IsStoreClosed { get; set; }

        public List<string> RoleList { get; set; }

        public string PriceFormatting(decimal Price)
        {
            return Price.ToString((CultureInfo)CultureInfo.CurrentCulture) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
        }
    }
}
