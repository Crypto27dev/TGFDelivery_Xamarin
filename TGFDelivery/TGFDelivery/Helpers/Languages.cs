using System;
using System.Collections.Generic;
using System.Text;
using TGFDelivery.Interfaces;
using Xamarin.Forms;
using TGFDelivery.Resources;

namespace TGFDelivery.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        #region "IndexPage"
        public static string PostCode
        {
            get { return Resource.PostCode; }
        }
        public static string Collection
        {
            get { return Resource.Collection; }
        }
        public static string Delivery
        {
            get { return Resource.Delivery; }
        }
        public static string Login
        {
            get { return Resource.Login; }
        }
        #endregion

    }
}
