using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TGFDelivery.Models;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Views.Menu;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryListPage : ContentPage
    {
        CategoryListPageModel _viewModel;
        public CategoryListPage()
        {
            _viewModel = new CategoryListPageModel();
            /*_viewModel.GroupList = new GroupListPageModel();*/
            InitializeComponent();

            BindingContext = _viewModel;
        }

        private async void CategoryList_ItemSelected(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var currentCat = e.CurrentSelection.FirstOrDefault() as CategoryModel;
            var currentCatName = currentCat.MYCat.Name;
            await App._NavigationPage.PushAsync(new ShowingNearest_pizza_(_viewModel, currentCat, currentCatName));
        }
    }
}
