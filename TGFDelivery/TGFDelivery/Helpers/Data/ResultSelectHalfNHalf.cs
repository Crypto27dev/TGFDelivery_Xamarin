using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGFDelivery.Helpers.Data
{
    public class ResultSelectHalfNHalf
    {
        public string ProductName { get; set; }
        
        public string Price { get; set; }

        public string Message { get; set; }

        public string Toppings { get; set; }

        public string ProductImgUrl { get; set; }

        public List<SelectedSide> ToppingList { get; set; }

        public ResultSelectHalfNHalf()
        {
            ProductName = string.Empty;
            ProductImgUrl = string.Empty;
            Price = "0.00";
            Message = "unsuccess";
            ToppingList = new List<SelectedSide>();
        }
    }
}
