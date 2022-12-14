using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGFDelivery.Models;
using WinPizzaData;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToppingsPage : ContentPage
    {
        public ToppingsPage()
        {
            InitializeComponent();

            System.Collections.ObjectModel.ObservableCollection<ToppingsModel> temps = new System.Collections.ObjectModel.ObservableCollection<ToppingsModel>();
            for (int i = 0; i < 10; i++)
            {
                ToppingsModel model = new ToppingsModel()
                {
                    MyPro = new WinPizzaData.WPBaseProduct()
                    {
                        Name = "TestFoot" + i,
                        ImgUrl = "test" + (i + 1) + ".png",

                    },
                    Calorie = 20 + i * 2,
                    IsVegan = i % 2 == 0 ? true : false,
                    Order = 0
                };
                temps.Add(model);
            }
            ProCustomView.Products = temps;
            BindingContext = ProCustomView;
        }
    }
}