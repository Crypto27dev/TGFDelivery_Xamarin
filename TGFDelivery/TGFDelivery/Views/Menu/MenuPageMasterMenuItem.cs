using System;

namespace TGFDelivery.Views.Menu
{

    public class MenuPageMasterMenuItem
    {
        public MenuPageMasterMenuItem()
        {
            TargetType = typeof(MenuPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}