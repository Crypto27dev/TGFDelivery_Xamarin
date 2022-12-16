using System;
using TGFDelivery.Data;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Views.Menu;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
