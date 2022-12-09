using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TGFDelivery.Data;
using TGFDelivery.Helpers.Data;
using TGFDelivery.Resources;
using TGFDelivery.Views;
using Xamarin.Forms;

namespace TGFDelivery.Models.PageModel
{
    public class BasketPageModel : ViewModelBase
    {
        #region "MultiLanguage"
        public string txt_youhave
        {
            get { return Resource.basket_Youhave; }
        }
        public string txt_items
        {
            get { return Resource.basket_items; }
        }
        public string txt_deliveryorder
        {
            get { return Resource.basket_onadeliveryorder; }
        }
        #endregion

        private string _Count { get; set; }
        public string Count
        {
            get { return _Count; }
            set { _Count = value; OnPropertyChanged(); }
        }
        private string _Btn_Name { get; set; }
        public string Btn_Name
        {
            get { return _Btn_Name; }
            set { _Btn_Name = value; OnPropertyChanged(); }
        }
        private string _Btn_BackColor { get; set; }
        public string Btn_BackColor
        {
            get { return _Btn_BackColor; }
            set { _Btn_BackColor = value; OnPropertyChanged(); }
        }
        private string _Btn_TextColor { get; set; }
        public string Btn_TextColor
        {
            get { return _Btn_TextColor; }
            set { _Btn_TextColor = value; OnPropertyChanged(); }
        }
        private string _Btn_BorderColor { get; set; }
        public string Btn_BorderColor
        {
            get { return _Btn_BorderColor; }
            set { _Btn_BorderColor = value; OnPropertyChanged(); }
        }
        private bool _Btn_IsEnabled { get; set; }
        public bool Btn_IsEnabled
        {
            get { return _Btn_IsEnabled; }
            set { _Btn_IsEnabled = value; OnPropertyChanged(); }
        }
        private string _TotalPrice { get; set; }
        public string TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; OnPropertyChanged(); }
        }
        private decimal _CheckOutPageParam { get; set; }
        public decimal CheckOutPageParam
        {
            get { return _CheckOutPageParam; }
            set { _CheckOutPageParam = value; OnPropertyChanged(); }
        }
        private ObservableCollection<BasketViewCellModel> _Datas = new ObservableCollection<BasketViewCellModel>();
        public ObservableCollection<BasketViewCellModel> Datas
        {
            get { return _Datas; }
            set { _Datas = value; OnPropertyChanged("Datas"); }
        }
        public ICommand Continue_Clicked { get; private set; }
        public BasketPageModel()
        {
            Continue_Clicked = new Command(com_Continue_Clicked);
        }

        private async void com_Continue_Clicked()
        {
            App.Loading(this);
            try
            {
                await App._NavigationPage.PushAsync(new MyDetailsPage());
                //App._NavigationPage.PushAsync(new CheckOutPage(this.CheckOutPageParam));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                App.Stop(this);
            }
        }

    }
    public class BasketViewCellModel : ViewModelBase
    {
        public event EventHandler<ResultAddToBasket> ButtonClicked;
        public event EventHandler<string> DeleteViewCell;
        //OrderLineID
        private string _Id { get; set; }
        public string Id
        {
            get { return _Id; }
            set { _Id = value; OnPropertyChanged(); }
        }
        private string _OfferId { get; set; }
        public string OfferId
        {
            get { return _OfferId; }
            set { _OfferId = value; OnPropertyChanged(); }
        }
        private WinPizzaData.WPBaseProduct _Product { get; set; }
        public WinPizzaData.WPBaseProduct Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged("Product"); }
        }
        private string _ImgUrl { get; set; }
        public string ImgUrl
        {
            get { return _ImgUrl; }
            set { _ImgUrl = value; OnPropertyChanged(); }
        }
        private string _Name { get; set; }
        public string Name 
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        private string _Qty { get; set; }
        public string Qty
        {
            get { return _Qty; }
            set { _Qty = value; OnPropertyChanged(); }
        }
        private string _Price { get; set; }
        public string Price
        {
            get { return _Price; }
            set { _Price = value; OnPropertyChanged(); }
        }
        private bool _IsCustomize { get; set; }
        public bool IsCustomize
        {
            get { return _IsCustomize; }
            set { _IsCustomize = value; OnPropertyChanged(); }
        }
        private bool _IsDeal { get; set; }
        public bool IsDeal
        {
            get { return _IsDeal; }
            set { _IsDeal = value; OnPropertyChanged(); }
        }
        public ICommand Plus { get; private set; }
        public ICommand Min { get; private set; }
        public ICommand ViewCellTapped { get; private set; }

        public BasketViewCellModel()
        {
            Plus = new Command(com_Plus);
            Min = new Command(com_Min);
            ViewCellTapped = new Command(com_ViewCellTapped);
        }
        private void com_Plus()
        {
            ResultAddToBasket res = DataManager.AddToBasketOnBasket(Int32.Parse(this.Id), 1);
            if (res.Message == "success")
            {
                this.Price = res.CurrentOrderLinePrice.ToString();
                this.Qty = (Int32.Parse(this.Qty) + 1).ToString();
                this.ButtonClicked?.Invoke(this, res);
            }
        }
        private void com_Min()
        {
            if ((Int32.Parse(this.Qty) - 1) == 0)
            {
                ResultAddToBasket result = DataManager.DeleteProductOnBasket(Int32.Parse(this.Id));
                if (result.Message == "success")
                {
                    this.Price = result.CurrentOrderLinePrice.ToString();
                    this.Qty = (Int32.Parse(this.Qty) - 1).ToString();
                    this.DeleteViewCell?.Invoke(this, this.Id);
                    this.ButtonClicked?.Invoke(this, result);
                    return;
                }
            }
            ResultAddToBasket res = DataManager.AddToBasketOnBasket(Int32.Parse(this.Id), 0);
            if (res.Message == "success")
            {
                this.Price = res.CurrentOrderLinePrice.ToString();
                this.Qty = (Int32.Parse(this.Qty) - 1).ToString();
                
                this.ButtonClicked?.Invoke(this, res);
            }
        }
        private void com_ViewCellTapped()
        {
            if (IsCustomize == true && IsDeal == false)
            {
                App._NavigationPage.PushAsync(new ProductDetailPage(this.Product.CatID, this.Product.GrpID, this.Product.ID, null, Int32.Parse(this.Id)));
                return;
            }
            if (IsCustomize == true && IsDeal == true)
            {
                App._NavigationPage.PushAsync(new DealPage(this.OfferId, this.Id));
                return;
            }
            if (IsCustomize == false)
            {
                return;
            }
            
        }
    }

}
