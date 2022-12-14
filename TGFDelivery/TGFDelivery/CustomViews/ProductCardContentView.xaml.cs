using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WinPizzaData;
using TGFDelivery.Models;
using TGFDelivery.Helpers;
using System.Windows.Input;
using TGFDelivery.Views;

namespace TGFDelivery.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCardContentView : ContentView
    {
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create(
               "Products",        // the name of the bindable property
               typeof(ObservableCollection<ProductsModel>),     // the bindable property type
               typeof(ProductCardContentView),   // the parent object type
               null, propertyChanged: OnEventNameChanged);

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Property changed implementation goes here
            ((ProductCardContentView)bindable).Products = (ObservableCollection<ProductsModel>)newValue;
        }

        public ObservableCollection<ProductsModel> Products
        {
            get { return (ObservableCollection<ProductsModel>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        private async void onCustomize(object sender, EventArgs e)
        {
            Button BtnCustomize = sender as Button;
            if (BtnCustomize != null)
            {
                await App._NavigationPage.PushAsync(new SideTabbedPage2Toppings());
            }
        }
        public ProductCardContentView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}