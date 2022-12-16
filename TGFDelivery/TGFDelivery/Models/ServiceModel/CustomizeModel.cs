using System;
using System.Collections.Generic;
using System.Globalization;
using WinPizzaData;

namespace TGFDelivery.Models.ServiceModel
{
    public class CustomizeModel
    {
        // All toppings for product
        public List<WPBaseProduct> AllToppings { get; set; }

        // Product to customize
        public WPProduct Product { get; set; }

        // Option list
        public Dictionary<string, List<Option>> GroupedLinq { get; set; }

        // OrderLine
        public OrderLine TheOrderLine { get; set; }

        // Current side index selected
        public int? CurrentSideIndex { get; set; }

        public int SideNumber { get; set; }

        // Products of group for selected product
        public List<WPBaseProduct> AllProducts { get; set; }

        // HalfNHalf Product list
        public Dictionary<int, WPProduct> HalfProducts { get; set; }

        public string ToppingGroupName { get; set; }

        public bool IsExistSide { get; set; }

        public List<String> ListGroupKey { get; set; }

        public int MaxToppingCount { get; set; }

        public List<string> RoleList { get; set; }

        public string PriceFormatting(decimal Price)
        {
            return Price.ToString((CultureInfo)CultureInfo.CurrentCulture) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
        }
    }
}
