using System;
using System.Collections.Generic;
using TGFDelivery.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using TGFDelivery.CustomViews;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrdersListPage : ContentPage
    {
       
        public MyOrdersListPage()
        {
            test testa = new test();
            InitializeComponent();
            testa.tests = getdata();
            BindingContext = testa;
        }


        public List<string> getdata()
        {
            List<string> temps = new List<string>();
            for(var i = 0; i < 3; i++)
            {
                var aa = "dessert" + (i + 1).ToString() + ".png";
                temps.Add(aa);
            }
            return temps;
        }

        public async void View_Details(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new OrderDetailView());
        }
    }
}