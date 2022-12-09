using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TGFDelivery.Interfaces;
using Xamarin.Forms;

namespace TGFDelivery.Models.PageModel
{
    public class CreditCardPageModel : ViewModelBase
    {
        
        public ICommand PayCommand { get; set; }
        private string _CardNumber { get; set; }
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; OnPropertyChanged(); }
        }
        public int SelectedIndex
        {
            get { return 0; }
        }
        private string _Month { get; set; }
        public string Month
        {
            get { return _Month; }
            set { _Month = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _MonthList { get; set; }
        public ObservableCollection<string> MonthList
        {
            get
            {
                _MonthList = new ObservableCollection<string>();
                for (int i = 1; i < 13; i++)
                {
                    if (i < 10)
                    {
                        _MonthList.Add(string.Format("{0}{1}", 0, i));
                        continue;
                    }
                    _MonthList.Add(i.ToString());
                }
                return _MonthList;
            }
        }
        private string _Year { get; set; }
        public string Year
        {
            get { return _Year; }
            set { _Year = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _YearList { get; set; }
        public ObservableCollection<string> YearList
        {
            get
            {
                _YearList = new ObservableCollection<string>();
                DateTime a = DateTime.Now;
                for (int i = 1999; i < 2100; i++)
                {
                    if (i > a.Year)
                    {
                        break;
                    }
                    _YearList.Add(i.ToString());
                }
                _YearList = new ObservableCollection<string>(_YearList.Reverse());
                return _YearList;
            }
        }

        private string _Cvv { get; set; }
        public string Cvv
        {
            get { return _Cvv; }
            set { _Cvv = value; OnPropertyChanged(); }
        }

        public ICommand Pay_Clicked { get; private set; }
        IPayService _payService;
         
        string paymentClientToken = "<Payment token returned by the API HERE>";

        public CreditCardPageModel()
        {
            _payService = Xamarin.Forms.DependencyService.Get<IPayService>();
            PayCommand = new Command(async () => await CreatePayment());
            Pay_Clicked = new Command(com_Pay_Clicked);
            //GetPaymentConfig();
        }
        private void com_Pay_Clicked()
        {

        }
        async Task GetPaymentConfig()
        {
            await _payService.InitializeAsync(paymentClientToken);
        }
        async Task CreatePayment()
        {
            UserDialogs.Instance.ShowLoading("Loading");

            if (_payService.CanPay)
            {
                try
                {
                    _payService.OnTokenizationSuccessful += OnTokenizationSuccessful;
                    _payService.OnTokenizationError += OnTokenizationError;
                    await _payService.TokenizeCard(CardNumber.Replace(" ", string.Empty), Month, Year, Cvv);

                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");
                    System.Diagnostics.Debug.WriteLine(ex);
                }

            }
            else
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Error", "Payment not available", "Ok");
                });
            }
        }

        async void OnTokenizationSuccessful(object sender, string e)
        {
            _payService.OnTokenizationSuccessful -= OnTokenizationSuccessful;
            System.Diagnostics.Debug.WriteLine($"Payment Authorized - {e}");
            UserDialogs.Instance.HideLoading();
            await App.Current.MainPage.DisplayAlert("Success", $"Payment Authorized: the token is{e}", "Ok");

        }

        async void OnTokenizationError(object sender, string e)
        {
            _payService.OnTokenizationError -= OnTokenizationError;
            System.Diagnostics.Debug.WriteLine(e);
            UserDialogs.Instance.HideLoading();
            await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");

        }

    }
}
