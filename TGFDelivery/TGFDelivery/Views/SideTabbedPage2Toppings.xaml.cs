using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TGFDelivery.Models;
using WinPizzaData;
using System.Collections.ObjectModel;
using TGFDelivery.Data;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
namespace TGFDelivery.Views
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ToppingData
    {
        public string Name { private set; get; }
        public List<ToppingsModel> Toppings { private set; get; }

        public ToppingData(string _Name, List<ToppingsModel> _controlli)
        {
            this.Name = _Name;
            this.Toppings = _controlli;
        }
    }
    public class ToppingDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ToppingTemplate { get; set; }
        public DataTemplate ToppingSelctionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((ToppingData)item).Toppings.FirstOrDefault().OneChoice == false)
                return ToppingTemplate;
            else
                return ToppingSelctionTemplate;
        }
    }
        
    public partial class SideTabbedPage2Toppings : Xamarin.Forms.TabbedPage
    {       

        List<ToppingData> _TabList;
        public List<ToppingData> TabList
        {
            get { return _TabList; }
            set { _TabList = value; OnPropertyChanged(); }
        }

        
        public SideTabbedPage2Toppings(WPBaseProduct Pro)
        {     
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            On<Android>().SetIsSmoothScrollEnabled(false);
            On<Android>().SetOffscreenPageLimit(1);
            InitializeComponent();
            var Temp = new List<ToppingsModel>();
            TabList = new List<ToppingData>();
            Group Grp = new Group();
            if (Pro.Modifiable)
            {
                Grp = StoreDataSource.DeStore.Store.GetCurrentMenuTopping(((WPProduct)Pro).ToppingGrpID);
            }
            var dd = Grp.DeProducts.GroupBy(p => p.DisplayGrp);

            foreach (var Item in dd)
            {
                Temp = new List<ToppingsModel>(); 
                Item.All(p => { 
                    Temp.Add(new ToppingsModel(p));
                    return true; 
                });
                TabList.Add(new ToppingData(Item.Key, Temp));
            }
            ItemsSource = TabList;
            this.CurrentPageChanged += SideTabbedPage2Toppings_CurrentPageChanged;
        }

        private void SideTabbedPage2Toppings_CurrentPageChanged(object sender, System.EventArgs e)
        {
            var CurrentTab = ((SideTabbedPage2Toppings)sender).CurrentPage;
            var DD = CurrentTab.Title;
        }
    }
}