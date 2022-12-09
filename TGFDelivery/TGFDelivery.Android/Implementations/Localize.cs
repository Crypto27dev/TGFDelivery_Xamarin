[assembly: Xamarin.Forms.Dependency(typeof(TGFDelivery.Droid.Implementations.Localize))]

namespace TGFDelivery.Droid.Implementations
{
    using System.Globalization;
    using System.Threading;
    using Helpers;
    using Interfaces;
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));

            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new System.Globalization.CultureInfo("en");
                }
            }
            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "ms-BN": //"Malaysian (Brunei)" not supported .Net culture
                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;
                case "in-ID":
                    netLanguage = "id-ID";
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;
            }
            return netLanguage;
        }
        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLangauge = platCulture.LanguageCode;
            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLangauge = "de-CH";
                    break;
            }
            return netLangauge;
        }
    }
}