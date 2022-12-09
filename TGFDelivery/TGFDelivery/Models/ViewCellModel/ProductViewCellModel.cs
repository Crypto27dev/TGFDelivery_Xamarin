using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TGFDelivery.Helpers;
using TGFDelivery.Helpers.Data;
using TGFDelivery.Data;
using WinPizzaData;
using WPUtility;
using Xamarin.Forms;
using TGFDelivery.Views;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace TGFDelivery.Models.ViewCellModel
{
    public class ProductViewCellModel : ViewModelBase
    {
        public event EventHandler<string> AddBtn_Clicked;
        public string BtnName                   { get; set; }
        public string BtnBorderColor            { get; set; }
        public string BtnBorderWidth            { get; set; }
        public string BtnBackgroundColor        { get; set; }
        public string BtnParam                  { get; set; }
        public bool BtnIsVisible                { get; set; }

        public string Name                      { get; set; }
        public string ImgUrl                    { get; set; }
        public string Price                     { get; set; }
        public int Qty                          { get; set; }

        public string CatID                     { get; set; }
        public string GroupID                   { get; set; }
        public string ProductID                 { get; set; }
        public ICommand onAdd                   { get; private set; }

        public ProductViewCellModel()
        {
            onAdd = new Command<string>(proc_onAdd);
        }
        public async void proc_onAdd(string index)
        {
            App.Loading(this);
            try
            {
                if (index == "ADD")
                {
                    await Task.Run(() => DataManager.AddToBasket(CatID, GroupID, ProductID, Qty));
                }
                else if (index == "CREATE")
                {
                    await App._NavigationPage.PushAsync(new ProductDetailPage(this.CatID, this.GroupID, this.ProductID, null, -1));
                    //DataManager.AddToBasket(wPProduct.CatID, wPProduct.GrpID, wPProduct.ID, Qty);

                }
                MessagingCenter.Send<object>(this, AppSettings.STATUS_PRICE);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                App.Stop(this);
            }
        }

    }
}
