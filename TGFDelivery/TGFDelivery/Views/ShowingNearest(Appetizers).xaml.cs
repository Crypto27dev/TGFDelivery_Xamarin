using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowingNearest_Appetizers_ : ContentPage
    {
        public ShowingNearest_Appetizers_()
        {
            test testa = new test();
            InitializeComponent();
            testa.tests = getdata();
            BindingContext = testa;
        }

        public List<string> getdata()
        {
            List<string> temps = new List<string>();
            for (var i = 0; i < 5; i++)
            {
                var aa = "dessert" + (i + 1).ToString();
                temps.Add(aa);
            }
            return temps;
        }



    }
}