using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.Data;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Models.ViewCellModel;
using TGFDelivery.Resources;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views.Tab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealsPage : ContentPage
    {
        public DealsPage()
        {
            InitializeComponent();
            
            //Subscribe();
        }
        protected override void OnAppearing()
        {
            Init();
        }
        private async void Init()
        {
            App.Loading(this);
            try
            {
                xName_BasketPrice.Text = DataManager.BASKET.DeOrderHeader.Total.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;

                DealsModel deModel = await DataManager.Deals();
                string btnName = string.Empty;
                if (deModel.IsStoreClosed)
                {
                    btnName = Resource.deals_Closed;
                }
                else
                {
                    btnName = Resource.deals_CHOOSE;
                }
                if (deModel.Products != null)
                {
                    foreach (WPMealDeal product in deModel.Products)
                    {
                        if (product.IsActive)
                        {
                            DealsViewCellModel dealsViewCellModel = new DealsViewCellModel(product.ID, product.ImgUrl, product.Name, product.Desc, btnName, "#00ed00");
                            DealsViewCell dealsViewCell = new DealsViewCell() { BindingContext = dealsViewCellModel };
                            xName_Product_List.Add(dealsViewCell);
                        }
                    }
                }
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

        private void onPrice(object sender, EventArgs e)
        {
            App._NavigationPage.PushAsync(new BasketPage());
        }
        protected override void OnDisappearing()
        {
            xName_Product_List.Clear();
        }
    }
}