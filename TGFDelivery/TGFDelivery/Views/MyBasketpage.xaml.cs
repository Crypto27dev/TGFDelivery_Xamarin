using System.Collections.Generic;
using TGFDelivery.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBasketPage : ContentPage
    {
        test testa = new test();
        public MyBasketPage()
        {
            InitializeComponent();
            testa.tests = getdata();
            BindingContext = testa;
        }

        public List<string> getdata()
        {
            List<string> temps = new List<string>();
            for (var i = 0; i < 2; i++)
            {
                var aa = "pizza0" + (i + 1).ToString() + ".png";
                temps.Add(aa);
            }
            return temps;
        }
    }
}