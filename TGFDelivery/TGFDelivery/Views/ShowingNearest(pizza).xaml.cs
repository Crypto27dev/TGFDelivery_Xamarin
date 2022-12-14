using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TGFDelivery.Helpers;
using TGFDelivery.Models;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Views.Menu;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TGFDelivery.Data;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowingNearest_pizza_ : ContentPage
    {
        CategoryListPageModel _viewModel;

        public ShowingNearest_pizza_(CategoryListPageModel viewModel,CategoryModel categoryModel, String currentCatName)
        {
            _viewModel = viewModel;
            InitializeComponent();
            CategoryList_ItemSelected(categoryModel);
            /*_viewModel.GroupList = new GroupListPageModel();*/
          
            Title = currentCatName;
                     
            BindingContext = _viewModel;

            StoreDataSource.DeCultureInfo = System.Globalization.CultureInfo.CurrentCulture;
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        /*    private async void _viewModel_OnLoadGroups(object sender, EventArgs e)
            {
                var mainPage = Application.Current.MainPage as MenuPage;
                if (_viewModel.MyCategorySelected.DeGroup.Count != 1)
                    await mainPage.Detail.Navigation.PushAsync(new GroupListPage());
                else
                    await mainPage.Detail.Navigation.PushAsync(new ProductListPage());
            }
    */

        private void CategoryList_ItemSelected(CategoryModel currentCat)
        {
            _viewModel.MyCategorySelected = currentCat;
            _viewModel.hasMultipleGroup = currentCat.MyGrp.Count == 1 ? false : true;
            var Current_True = _viewModel.CategoryList.Where(p => p.IsSelected == true).FirstOrDefault();
            if (Current_True == null) { 
                _viewModel.CategoryList.Where(p => p == currentCat).FirstOrDefault().IsSelected = true;
            }
            else
            {
                Current_True.IsSelected = false;
                _viewModel.CategoryList.Where(p => p == currentCat).FirstOrDefault().IsSelected = true;
            }
            ProsView.Products = new System.Collections.ObjectModel.ObservableCollection<ProductsModel>(_viewModel.MyCategorySelected.MyGrp[0].MyPros);
        }

        public List<string> getdata()
        { 
            List<string> temps = new List<string>();
            for (var i = 0; i < 4; i++){
                var aa = "pizza0" + (i + 1).ToString();
                temps.Add(aa);
            }
            return temps;
        }

/*        public void SelectionCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null) return;

            _viewModel.MyCategorySelected = (Category)e.CurrentSelection.FirstOrDefault();
        }*/

        public void GroupCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null) return;


            _viewModel.MyGroupSelected = (GroupModel)e.CurrentSelection[0];
            var Current_True = _viewModel.MyCategorySelected.MyGrp.Where(p => p.IsSelected == true).FirstOrDefault();
            if (Current_True == null)
            {                           
                _viewModel.MyCategorySelected.MyGrp.Where(p => p == e.CurrentSelection[0]).FirstOrDefault().IsSelected = true;
            }
            else
            {
                Current_True.IsSelected = false;
                _viewModel.MyCategorySelected.MyGrp.Where(p => p == e.CurrentSelection[0]).FirstOrDefault().IsSelected = true;
            }

            ProsView.Products = new System.Collections.ObjectModel.ObservableCollection<ProductsModel>(_viewModel.MyGroupSelected.MyPros);
            // _viewModel.MyGroupSelected = new GroupModel();
        }

    }
}