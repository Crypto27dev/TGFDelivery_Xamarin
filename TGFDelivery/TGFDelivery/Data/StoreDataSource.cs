using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Services;
using WinPizzaData;
using WPUtility;

namespace TGFDelivery.Data
{

    public class StoreData : BindableBase
    {
        public StoreData()
        {
            _Store = new Store();

        }
        public StoreData(Store DeStore)
        {
            _Store = DeStore;
            DispayableGrp = new ObservableCollection<Group>(DeStore.DispayableGrp);
            DeStore.DeCats.ForEach(p =>
            {
                var DCat = p;
                DeCats.Add(DCat);
                if (DCat.CatType == WinPizzaEnums.ItemType.TOPPING)
                    ToppingGrp = new ObservableCollection<Group>(DCat.DeGroup);
            });
        }

        public WPProduct Product4CreateYourOwn { get; set; }

        // Product for HalfNHalf
        public WPProduct Product4Half { get; set; }

        // Product for legend
        public WPProduct Product4Legend { get; set; }

        // Product for sale
        public WPProduct Product4Sale { get; set; }

        private Store _Store;
        public Store Store
        {
            get { return _Store; }
            set
            {

                { _Store = value; OnPropertyChanged(); }
            }

        }
        public ObservableCollection<Group> _DispayableGrp = new ObservableCollection<Group>();
        public ObservableCollection<Group> DispayableGrp
        {
            get { return _DispayableGrp; }
            set
            {
                if (_DispayableGrp != value)
                { _DispayableGrp = value; OnPropertyChanged(); }
            }

        }
        ObservableCollection<Group> _ToppingGrp = new ObservableCollection<Group>();
        public ObservableCollection<Group> ToppingGrp
        {
            get { return _ToppingGrp; }
            set
            {
                if (_ToppingGrp != value)
                { _ToppingGrp = value; OnPropertyChanged(); }
            }

        }

        ObservableCollection<Category> _DeCats = new ObservableCollection<Category>();
        public ObservableCollection<Category> DeCats
        {
            get { return _DeCats; }
            set
            {
                if (_DeCats != value)
                { _DeCats = value; OnPropertyChanged(); }
            }
        }
        DateTime _OpeningTime;
        public DateTime OpeningTime
        {
            get { return _OpeningTime; }
            set
            {
                if (_OpeningTime != value)
                { _OpeningTime = value; OnPropertyChanged(); }
            }
        }
        public PrinterData ReciptPrinter
        {
            get { return _Store.ReciptPrinter; }
            set
            {
                if (_Store.ReciptPrinter != value)
                { _Store.ReciptPrinter = value; OnPropertyChanged(); }
            }
        }
        public PrinterData ReportPrinter
        {
            get { return _Store.ReportPrinter; }
            set
            {
                if (_Store.ReportPrinter != value)
                { _Store.ReportPrinter = value; OnPropertyChanged(); }
            }
        }
        public PrinterData TillPrinter
        {
            get { return _Store.TillPrinter; }
            set
            {
                if (_Store.TillPrinter != value)
                { _Store.TillPrinter = value; OnPropertyChanged(); }
            }
        }
        public bool OnlinePayment
        {
            get { return _Store.OnlinePayment; }
            set
            {
                if (_Store.OnlinePayment != value)
                { _Store.OnlinePayment = value; OnPropertyChanged(); }
            }
        }
        public string PaymentUrl
        {
            get { return _Store.PaymentUrl; }
            set
            {
                if (_Store.PaymentUrl != value)
                { _Store.PaymentUrl = value; OnPropertyChanged(); }
            }
        }
        public List<PrinterData> PrepPrinter
        {
            get { return _Store.DePrepPrinters; }
            set
            {
                if (_Store.DePrepPrinters != value)
                { _Store.DePrepPrinters = value; OnPropertyChanged(); }
            }
        }


        public WPCustomerMessages DeCustomerMsg
        {
            get { return _Store.CustomerMsgs; }
            set
            {
                if (_Store.CustomerMsgs != value)
                { _Store.CustomerMsgs = value; OnPropertyChanged(); }
            }
        }
        public string CurrentMenuID
        {
            get { return _Store.CurrentMenuID; }
            set
            {
                if (_Store.CurrentMenuID != value)
                { _Store.CurrentMenuID = value; OnPropertyChanged(); }
            }
        }
    }
    public class FoodStoreProfileData : BindableBase
    {
        public FoodStoreProfileData()
        {
            _StoreProfile = new FoodStoreProfile();
        }
        public FoodStoreProfileData(FoodStoreProfile DeStoreProfile)
        {
            _StoreProfile = DeStoreProfile;
        }
        FoodStoreProfile _StoreProfile;
        public FoodStoreProfile AStoreProfile
        {
            get { return _StoreProfile; }
            set
            {
                if (_StoreProfile != value)
                { _StoreProfile = value; OnPropertyChanged(); }
            }
        }
        bool _IsStoreValid;
        public bool IsStoreValid
        {
            get { return (AStoreProfile != null && !WinPizzaEnums.IsNUll(StoreID)); }

        }
        public string StoreID
        {
            get { return _StoreProfile.StoreID; }
            set
            {
                if (_StoreProfile.StoreID != value)
                { _StoreProfile.StoreID = value; OnPropertyChanged(); }
            }
        }
        public string Logo
        {
            get { return _StoreProfile.DeStoreLinks.Logo; }
            set
            {
                if (_StoreProfile.DeStoreLinks.Logo != value)
                { _StoreProfile.DeStoreLinks.Logo = value; OnPropertyChanged(); }
            }
        }

        public int CurrentDLVTime
        {
            get { return _StoreProfile.CurrentDLVTime; }
            set
            {
                if (_StoreProfile.CurrentDLVTime != value)
                { _StoreProfile.CurrentDLVTime = value; OnPropertyChanged(); }
            }
        }
    }
    public sealed class StoreDataSource
    {
        public static StoreDataSource _StoreDataSource = new StoreDataSource();

        #region Property
        public static CultureInfo DeCultureInfo;
        public static bool EposMode { set; get; }
        public static FoodStoreProfile DeStoreProfile { set; get; }
        public static ServersUrl DeSeversUrl { set; get; }
        public static StoreData DeStore { set; get; }
        public static bool IsStoreClosed { set; get; }
        // closing and opening times
        public static List<string> RoleList { get; set; }
        #endregion

        #region StoreSetting
        public static async Task<bool> GetStoreProfile(string DeStoreID)
        {
            if (DeStore == null || DeStoreProfile == null || DeSeversUrl == null)
            {
                DeStoreProfile = await CoreServices.GetStoreProfile(DeStoreID);
                // strStoreID);
                if (DeStoreProfile == null)
                {
                    return false;
                }
            }
            return true;
        }

        public static async Task SetStoreStatus()
        {
            IsStoreClosed = await CoreServices.GetStoreClosed(DeStoreProfile.DeDataSourceName, DeSeversUrl, DeStore.Store);
        }

        public static async Task InitStoreSetting(string DeStoreID)
        {
            bool IsGetStoreProfile = await StoreDataSource.GetStoreProfile(DeStoreID);
            if (!IsGetStoreProfile)
            {
                // If don't get store profile, redirect to share site
                await UserDialogs.Instance.AlertAsync("Server Error", "GetStoreProfile", "Cancel");
                return;
            }
            DeSeversUrl = DataManager.AdjustServcePoints(await CoreServices.GetWebServicesEndPoint(DeStoreProfile.StoreID), DeStoreProfile);
            DeStore = new StoreData(await CoreServices.LoadMainMenu(DeStoreProfile.StoreID, DeStoreProfile.DeDataSourceName, DeSeversUrl, DeStoreProfile.DeStoreLinks.Photo, AppSettings.MenuID));
            if (!DeStore.Store.IsValid)
            {
                await UserDialogs.Instance.AlertAsync("Server Error", "LoadMenu", "Cancel");
                return;
            }
            BasketDataSource.BasketData = new Orderdata();
        }
        #endregion
    }
}
