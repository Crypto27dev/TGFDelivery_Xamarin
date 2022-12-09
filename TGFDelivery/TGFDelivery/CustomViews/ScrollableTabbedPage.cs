using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace TGFDelivery.CustomViews
{
    public class ScrollableTabbedPage : FreshTabbedNavigationContainer
    {
        public ScrollableTabbedPage()
        {
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            On<Android>().SetIsSmoothScrollEnabled(false);
            On<Android>().SetOffscreenPageLimit(1);
        }

        
    }
}
