using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TGFDelivery.Views;
using TGFDelivery.Models;

namespace TGFDelivery.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCardCustomView : ContentView
    {
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create(
               "Products",        // the name of the bindable property
               typeof(ObservableCollection<ToppingsModel>),     // the bindable property type
               typeof(ProductCardCustomView),   // the parent object type
               null, propertyChanged: OnEventNameChanged);

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Property changed implementation goes here
            ((ProductCardCustomView)bindable).Products = (ObservableCollection<ToppingsModel>)newValue;
        }

        public ObservableCollection<ToppingsModel> Products
        {
            get { return (ObservableCollection<ToppingsModel>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public ProductCardCustomView()
        {
            InitializeComponent();
        }

        public ToppingsModel PreviousItem { set; get; }

        private void ItemSelectionEvent(object sender, SelectedItemChangedEventArgs e)
        {   
            var currentItem = e.SelectedItem as ToppingsModel;
            if (PreviousItem != null)
            {
                PreviousItem.IsSelected = false;
            }
            if (currentItem != null)
            {
                currentItem.IsSelected = true;
                PreviousItem = currentItem;
            }
        }
    }
}