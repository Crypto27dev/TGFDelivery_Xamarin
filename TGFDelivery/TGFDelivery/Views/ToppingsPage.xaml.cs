﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Products = temps;
            BindingContext = this;
        }

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