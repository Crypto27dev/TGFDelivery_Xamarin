using System;
using System.Collections.Generic;
using System.Text;

namespace TGFDelivery.Models.ViewCellModel
{
    public class SideViewCellModel
    {
        public SideViewCellModel(WinPizzaData.ItemOnProduct topping, string backColor, int portion)
        {
            Topping = topping;
            BackColor = backColor;
            Portion = portion;
        }

        public WinPizzaData.ItemOnProduct Topping { get; set; }
        public string BackColor { get; set; }
        public int Portion { get; set; }
    }
}
