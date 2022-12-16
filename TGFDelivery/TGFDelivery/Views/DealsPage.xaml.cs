using System.Collections.Generic;
using TGFDelivery.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealsPage : ContentPage
    {
        test _test = new test();
        public DealsPage()
        {

            InitializeComponent();
            _test.tests = getdata();
            BindingContext = _test;
        }

        public List<string> getdata()
        {
            List<string> temps = new List<string>();
            for (var i = 0; i < 4; i++)
            {
                var aa = "deal0" + (i + 1).ToString();
                temps.Add(aa);
            }
            return temps;
        }
    }
}