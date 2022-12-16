using System;
using TGFDelivery.Models.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCardPage : ContentPage
    {
        private CreditCardPageModel _Model = new CreditCardPageModel();
        public CreditCardPage()
        {
            InitializeComponent();
            BindingContext = _Model;
            Init();
        }
        private async void Init()
        {
            try
            {
                //CardPayment.CardPaymentClient client = new CardPayment.CardPaymentClient(CardPayment.CardPaymentClient.EndpointConfiguration.BasicHttpsBinding_ICardPayment);
                //var result =  client.GetClientBrainteeToken("");

                // HttpClient client = new HttpClient()
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}