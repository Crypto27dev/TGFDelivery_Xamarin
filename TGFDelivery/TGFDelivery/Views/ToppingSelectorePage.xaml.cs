using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TGFDelivery.Data;
using TGFDelivery.Models;
using System.Collections.ObjectModel;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToppingSelectorePage : ContentPage
    {
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create(
            "Products",        // the name of the bindable property
            typeof(ObservableCollection<ToppingsModel>),     // the bindable property type
            typeof(ToppingsPage),   // the parent object type
            null, propertyChanged: OnEventNameChanged);

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
            // Property changed implementation goes here
            ((ToppingsPage)bindable).Products = (ObservableCollection<ToppingsModel>)newValue;
        }

        public ObservableCollection<ToppingsModel> Products
        {
            get { return (ObservableCollection<ToppingsModel>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public ToppingsModel PreviousItem { set; get; }
        public ToppingSelectorePage()
        {
            InitializeComponent();
        }

        private void ToppingList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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

        private void BtnReset_Clicked(object sender, EventArgs e)
        {

        }
        private void BtnSave_Clicked(object sender, EventArgs e)
        {

        }

       
    }
}