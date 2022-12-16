using System;
using System.Globalization;
using TGFDelivery.Data;
using Xamarin.Forms;
namespace TGFDelivery.Helpers
{
    public class PlatformCulture
    {
        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocaleCode { get; private set; }

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", "platformCulturesString");
                //in c# 6 use name of(platformCultureString);
            }
            PlatformString = platformCultureString.Replace("_", "-");// .Net expects dash, not underscore
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }
        public override string ToString()
        {
            return PlatformString;
        }
    }
    public class ConvertToCurrency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dd = parameter != null ? string.Format((string)parameter, value) : ((decimal)value).ToString("C2", StoreDataSource.DeCultureInfo);
            return dd;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal ResOut = 0;
            if (decimal.TryParse((string)value, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint, StoreDataSource.DeCultureInfo, out ResOut))
                return ResOut;
            else
                return 0;
            //var Res = string.IsNullOrEmpty((string)value) ? 0 :
            //          decimal.TryParse((string)value,out ResOut);


        }
    }
}
