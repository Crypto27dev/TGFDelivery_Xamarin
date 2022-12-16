using Acr.UserDialogs;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TGFDelivery.Data;
using TGFDelivery.Views;
using Xamarin.Forms;

namespace TGFDelivery.Models.PageModel
{
    public class MyDetailPageModel : ViewModelBase
    {

        public string PaymentMethod { get; set; }
        public string[] PaymentMethods = new[] { "Cash", "Card" };

        public string PostCode
        {
            get { return DataManager.POSTCODE; }
        }
        private ObservableCollection<string> _AddressList = new ObservableCollection<string>();
        public ObservableCollection<string> AddressList
        {
            get { return _AddressList; }
            set { _AddressList = value; OnPropertyChanged("AddressList"); }
        }
        private string _SelectedAddress { get; set; }
        public string SelectedAddress
        {
            get { return _SelectedAddress; }
            set { _SelectedAddress = value; OnPropertyChanged(); }
        }
        private int _SelectedAddressIndex { get; set; }
        public int SelectedAddressIndex
        {
            get { return _SelectedAddressIndex; }
            set { _SelectedAddressIndex = value; OnPropertyChanged(); }
        }
        private string _First_Name { get; set; }
        public string First_Name
        {
            get { return _First_Name; }
            set { _First_Name = value; OnPropertyChanged(); }
        }
        private string _Contact_Number { get; set; }
        public string Contact_Number
        {
            get { return _Contact_Number; }
            set { _Contact_Number = value; OnPropertyChanged(); }
        }
        private string _Email { get; set; }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }
        public string BasketPrice
        {
            get { return string.Format("{0:0.00}", DataManager.BASKET.DeOrderHeader.Total) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol; }
        }
        private Color _Card_BackGround { get; set; }
        public Color Card_BackGround
        {
            get { return _Card_BackGround; }
            set { _Card_BackGround = value; OnPropertyChanged(); }
        }
        private Color _Card_FrontGround { get; set; }
        public Color Card_FrontGround
        {
            get { return _Card_FrontGround; }
            set { _Card_FrontGround = value; OnPropertyChanged(); }
        }
        private Color _Paypal_BackGround { get; set; }
        public Color Paypal_BackGround
        {
            get { return _Paypal_BackGround; }
            set { _Paypal_BackGround = value; OnPropertyChanged(); }
        }
        private Color _Paypal_FrontGround { get; set; }
        public Color Paypal_FrontGround
        {
            get { return _Paypal_FrontGround; }
            set { _Paypal_FrontGround = value; OnPropertyChanged(); }
        }
        public ICommand Card_Selected { get; private set; }
        public ICommand Paypal_Selected { get; private set; }
        public ICommand Pay_Clicked { get; private set; }
        public MyDetailPageModel()
        {
            com_Card_Selected();
            Card_Selected = new Command(com_Card_Selected);
            Paypal_Selected = new Command(com_Paypal_Selected);
            Pay_Clicked = new Command(com_Pay_Clicked);
        }
        private async void com_Pay_Clicked()
        {
            if (string.IsNullOrEmpty(this.SelectedAddress))
            {
                UserDialogs.Instance.Alert("Please Select Address", "Warning", "Cancel");
                return;
            }
            if (string.IsNullOrEmpty(this.First_Name))
            {
                UserDialogs.Instance.Alert("Please Input Name", "Warning", "Cancel");
                return;
            }
            if (string.IsNullOrEmpty(this.Contact_Number))
            {
                UserDialogs.Instance.Alert("Please Input Contact Number", "Warning", "Cancel");
                return;
            }
            if (string.IsNullOrEmpty(this.Email))
            {
                UserDialogs.Instance.Alert("Please Input Email", "Warning", "Cancel");
                return;
            }
            if (this.PaymentMethod == "CARD")
            {
                App.Loading(this);
                try
                {
                    var res = await DataManager.PlaceOrderAndPay(this);
                    if (res == "card")
                    {
                        await App._NavigationPage.PushAsync(new CreditCardPage());
                        return;
                    }
                    await UserDialogs.Instance.AlertAsync(res, "Warning", "Cancel");
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
        private void com_Paypal_Selected()
        {
            this.PaymentMethod = "CASH";
            this.Card_BackGround = Color.Transparent;
            this.Card_FrontGround = Color.Transparent;
            this.Paypal_BackGround = Color.FromHex("#696969");
            this.Paypal_FrontGround = Color.FromHex("#DBE0E3");
        }
        private void com_Card_Selected()
        {
            this.PaymentMethod = "CARD";
            this.Paypal_BackGround = Color.Transparent;
            this.Paypal_FrontGround = Color.Transparent;
            this.Card_BackGround = Color.FromHex("#696969");
            this.Card_FrontGround = Color.FromHex("#DBE0E3");
        }
    }
}
