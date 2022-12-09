using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Views.Menu;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupListPage : ContentPage
    {
        GroupListPageModel _viewModel;

        public GroupListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GroupListPageModel();
            _viewModel.OnLoadProducts += _viewModel_OnLoadProducts;
        }


        private async void _viewModel_OnLoadProducts(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MenuPage;
            await mainPage.Detail.Navigation.PushAsync(new ProductListPage());
        }

       

        private void GroupList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null) return;

           /* _viewModel.MyGroupSelected = (Group)e.SelectedItem; // service
            _viewModel.OnGroupClickedCommand.Execute(null);
            GroupList.SelectedItem = null;*/
        }

        private async void go_Category(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MenuPage;
            await mainPage.Detail.Navigation.PopAsync();
        }
    }
}
