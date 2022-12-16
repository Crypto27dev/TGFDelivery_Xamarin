using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TGFDelivery.Data;
using TGFDelivery.Views;
using WinPizzaData;
using Xamarin.Forms;

namespace TGFDelivery.Models.PageModel
{
    public class DealPageModel : ViewModelBase
    {
        private string _txt_AddBasket { get; set; }
        public string txt_AddBasket
        {
            get { return _txt_AddBasket; }
            set { _txt_AddBasket = value; OnPropertyChanged(); }
        }
        private string _PageTitleName;
        public string PageTitleName
        {
            get { return _PageTitleName; }
            set { _PageTitleName = value; OnPropertyChanged(); }
        }
        private string _DealName;
        public string DealName
        {
            get { return _DealName; }
            set { _DealName = value; OnPropertyChanged(); }
        }
        private string _Price;
        public string Price
        {
            get { return _Price; }
            set { _Price = value; OnPropertyChanged(); }
        }
        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; OnPropertyChanged(); }
        }
        private ObservableCollection<CollectionData> _Datas = new ObservableCollection<CollectionData>();
        public ObservableCollection<CollectionData> Datas
        {
            get { return _Datas; }
            set { _Datas = value; OnPropertyChanged("Datas"); }
        }
        public ICommand AddBasket_Clicked { private set; get; }

        public DealPageModel()
        {
            AddBasket_Clicked = new Command(com_AddBasket_Clicked);
        }
        private void com_AddBasket_Clicked()
        {
            try
            {
                DataManager.AddDealToBasket();
                //App._NavigationPage.Navigation.RemovePage(App._NavigationPage.Navigation.NavigationStack[App._NavigationPage.Navigation.NavigationStack.Count - 2]);
                App._NavigationPage.PopAsync();
                MessagingCenter.Send<object>(this, AppSettings.STATUS_PRICE);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class CollectionData : ViewModelBase
    {
        private string _Index;
        public string Index
        {
            get { return _Index; }
            set { _Index = value; OnPropertyChanged(); }
        }
        private string _ImgUrl;
        public string ImgUrl
        {
            get { return _ImgUrl; }
            set { _ImgUrl = value; OnPropertyChanged(); }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        private string _Desc;
        public string Desc
        {
            get { return _Desc; }
            set { _Desc = value; OnPropertyChanged(); }
        }
        private bool _IsCustomize;
        public bool IsCustomize
        {
            get { return _IsCustomize; }
            set { _IsCustomize = value; OnPropertyChanged(); }
        }
        private ObservableCollection<WPBaseProduct> _Products = new ObservableCollection<WPBaseProduct>();
        public ObservableCollection<WPBaseProduct> Products
        {
            get { return _Products; }
            set { _Products = value; OnPropertyChanged("Products"); }
        }
        public ICommand Product_Clicked { private set; get; }

        public CollectionData()
        {
            Product_Clicked = new Command<string>(com_Product_Clicked);
        }
        private void com_Product_Clicked(string index)
        {
            App.Loading(this);
            if (this.Products.Count > 0)
            {
                App._NavigationPage.PushAsync(new Select2DealPage(this.Products, this.Index, this.IsCustomize));
            }
            App.Stop(this);
        }
    }
}
