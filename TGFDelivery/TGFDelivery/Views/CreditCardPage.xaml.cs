﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.Models.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
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