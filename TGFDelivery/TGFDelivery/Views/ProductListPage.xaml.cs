using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Views.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TGFDelivery.Data;
using WinPizzaData;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        ProductListPageModel _viewModel;

        public ProductListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ProductListPageModel();
        }

        private async void go_Category(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MenuPage;
            await mainPage.Detail.Navigation.PopToRootAsync();
            
        }

        private async void go_Group(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MenuPage;
            await mainPage.Detail.Navigation.PopAsync();
        }

        private void ProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BasketDataSource.DoNoCustomize(((WPBaseProduct)sender));
        }
    }
}
