using System;
using System.Net.Http;
using TGFDelivery.Data;
using TGFDelivery.Views;
using WinPizzaData;
using Xamarin.Forms;
namespace TGFDelivery
{
    public partial class App : Application
    {
        public static NavigationPage _NavigationPage { get; set; }

        public App()
        {
            InitializeComponent();
            _NavigationPage = new NavigationPage(new IndexPage());
            /*_NavigationPage = new NavigationPage(new MyOrdersListPage());*/
            MainPage = _NavigationPage;
        }

        protected override async void OnStart()
        {
            HttpClient client = new HttpClient();
            try
            {
                // client.DefaultRequestHeaders.Add("SOAPAction", "TheSameAsIn_WcfTestClient");
                // var response = await client.GetStringAsync("https://korush.eu/ThirdPartyServices/StoreServices.svc/GetPaymentToken?StoreID=65");
                var dd = await LoadStoreHelperFunctoin.DoWebJsonServices("http://korush.eu/ThirdPartyServices/StoreServices.svc/GetPaymentToken?StoreID=DEVDATA");
            }
            catch (Exception gg)
            {

            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static void Loading(object obj)
        {
            MessagingCenter.Send(obj, AppSettings.STATUS_LOADING);
        }

        public static void Stop(object obj)
        {
            MessagingCenter.Send(obj, AppSettings.STATUS_DONE);
        }

    }
}
