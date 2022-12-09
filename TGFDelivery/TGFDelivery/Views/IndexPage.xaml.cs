using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.CustomViews;
using TGFDelivery.Data;
using TGFDelivery.Helpers;
using TGFDelivery.Models;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Resources;
using TGFDelivery.Views.Menu;
using TGFDelivery.Views.Tab;
using WPUtility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : ContentPage
    {
        private IndexPageModel _Model = new IndexPageModel();
        public IndexPage()
        {
            InitializeComponent();
            BindingContext = _Model;
        }


        private void onCollection(object sender, EventArgs e)
        {
            
        }
       
        private async void onDelivery(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_PostCode.Text))
            {
                await DisplayAlert("Warning", "Please add PostCode", "Cancel");
                return;
            }
            App.Loading(this);
            try
            {
                //   DataManager.MenuModel =    await DataManager.Index("DELIVERY", "EN88JQ", null, null);
                //  DataManager.ORDER_TYPE = "DELIVERY";
                //   DataManager.POSTCODE = "EN88JQ";
                await StoreDataSource.InitStoreSetting("DEVDATA");
                BasketDataSource.CreateOrder(_PostCode.Text, WinPizzaEnums.OrderType.DELIVERY.ToString());

                /*                var menuPage = await Task.Run(() => proc_onDelivery());
                                App.Current.MainPage = menuPage;*/
                await App._NavigationPage.PushAsync(new LandingPage());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                App.Stop(this);
            }
        }

        
        private MenuPage proc_onDelivery()
        {

            //ScrollableTabbedPage scrollableTabbedPage = new ScrollableTabbedPage() { BarBackgroundColor = Color.FromHex("696969") };
            //scrollableTabbedPage.AddTab<DealsPageModel>(Resource.deals_DEALS, null);
            //Menu
            CategoryListPage categoryListPage = new CategoryListPage();
            //foreach (var DeCat in DataManager.MenuModel.CategoryList)
            //{
            //scrollableTabbedPage.AddTab<ProductPageModel>(DeCat.Name, null, DeCat);
            //ProductPage productPage = (ProductPage)scrollableTabbedPage.TabbedPages.Last();
            //productPage.SetCategory(DeCat);
            //}


            MenuPage menuPage = new MenuPage()
            {
                //Detail = scrollableTabbedPage
                Detail = new NavigationPage(categoryListPage) { BarBackgroundColor = Color.Black, BarTextColor = Color.White }
            };

            return menuPage;

        }
    }
}