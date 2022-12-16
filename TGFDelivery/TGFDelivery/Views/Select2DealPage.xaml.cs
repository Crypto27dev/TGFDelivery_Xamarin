using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.Data;
using TGFDelivery.Models.ViewCellModel;
using TGFDelivery.Resources;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Select2DealPage : ContentPage
    {
        private ObservableCollection<WPBaseProduct> _Products = new ObservableCollection<WPBaseProduct>();
        private string _Index { get; set; }
        private bool _IsCustomize { get; set; }
        public Select2DealPage(ObservableCollection<WPBaseProduct> products, string Index, bool IsCustomize)
        {
            InitializeComponent();
            _Products = products;
            _Index = Index;
            _IsCustomize = IsCustomize;

        }
        protected override void OnAppearing()
        {
            Init();
        }
        private async void Init()
        {
            App.Loading(this);
            await Task.Delay(300);
            foreach (WPBaseProduct product in _Products)
            {
                Select2DealViewCellModel select2DealViewCellModel = new Select2DealViewCellModel(DataManager.StoreProfile.DeStoreLinks.Photo + product.ImgUrl, product.Name, Resource.deal_ADDTODEAL, product, this._Index, this._IsCustomize);
                Select2DealViewCell select2DealViewCell = new Select2DealViewCell() { BindingContext = select2DealViewCellModel };
                xName_List.Add(select2DealViewCell);
            }

            App.Stop(this);
        }

        protected override void OnDisappearing()
        {
            xName_List.Clear();
        }
    }
}