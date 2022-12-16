using System.Windows.Input;
using TGFDelivery.Data;
using TGFDelivery.Views;
using WinPizzaData;
using Xamarin.Forms;

namespace TGFDelivery.Models.ViewCellModel
{
    public class Select2DealViewCellModel
    {
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string BtnName { get; set; }
        public WPBaseProduct Product { get; set; }
        public ICommand onAdd { get; private set; }
        public ICommand ViewCellTapped { get; private set; }
        public string OfferIndex { get; set; }
        public bool isCustomize { get; set; }

        public Select2DealViewCellModel(string ImgUrl, string Name, string BtnName, WPBaseProduct Product, string OfferIndex, bool isCustomize)
        {
            this.ImgUrl = ImgUrl;
            this.Name = Name;
            this.BtnName = BtnName;
            this.Product = Product;
            this.OfferIndex = OfferIndex;
            this.isCustomize = isCustomize;
            onAdd = new Command(com_onAdd);
            ViewCellTapped = new Command(com_ViewCellTapped);
        }
        private void com_onAdd()
        {
            var test = DataManager.AddToDeal(this.Product.CatID, this.Product.GrpID, this.Product.ID, this.OfferIndex);
            App._NavigationPage.PopAsync();
        }
        private void com_ViewCellTapped()
        {
            if (this.isCustomize)
            {
                App._NavigationPage.PushAsync(new ProductDetailPage(this.Product.CatID, this.Product.GrpID, this.Product.ID, this.OfferIndex, -1));
            }
            else
            {
                return;
            }
        }
    }
}
