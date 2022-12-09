using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.Data;
using TGFDelivery.Helpers.Data;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : ContentPage
    {
        private BasketPageModel _Model;
        public BasketPage()
        {
            InitializeComponent();
            this.Title = "My Order";
        }
        protected override void OnAppearing()
        {
            BindingContext = _Model = new BasketPageModel();
            Init();
        }
        private async void Init()
        {
            App.Loading(this);
            try
            {
                await Task.Delay(200);
                BasketModel deModel = DataManager.Basket();
                if (deModel.BasketInfo.DeOrderLines.Count == 0)
                {
                    await App._NavigationPage.PopAsync();
                    return;
                }
                
                    _Model.Count = deModel.BasketInfo.DeOrderLines.Count.ToString();
                    _Model.TotalPrice = string.Format("{0:0.00}", DataManager.BASKET.DeOrderHeader.Total) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
                    if (deModel.BasketInfo.DeOrderLines.Count > 0 && deModel.BasketInfo.DeOrderHeader.Total >= deModel.BasketInfo.GetMin4DlvOrder())
                    {
                        _Model.Btn_Name = Resource.basket_CheckOut;
                        _Model.Btn_BackColor = "#5be5b6";
                        _Model.Btn_TextColor = "#FFFFFF";//white
                        _Model.Btn_BorderColor = "#5be5b6";
                        _Model.Btn_IsEnabled = true;
                        _Model.CheckOutPageParam = deModel.BasketInfo.GetMin4DlvOrder();
                    }
                    else
                    {
                        _Model.Btn_Name = string.Format("{0} {1}", Resource.basket_MinmumOrdervalueis, deModel.BasketInfo.GetMin4DlvOrder());
                        _Model.Btn_BackColor = "#2d2d2d";
                        _Model.Btn_TextColor = "#FF0000";//red
                        _Model.Btn_BorderColor = "#FF0000";
                        _Model.Btn_IsEnabled = false;
                    }
                    foreach (WinPizzaData.OrderLine DeOrderLine in deModel.BasketInfo.DeOrderLines)
                    {
                        BasketViewCellModel basketViewCellModel = new BasketViewCellModel();
                        if (!DeOrderLine.IsDeal)
                        {
                            WinPizzaData.WPBaseProduct DeProduct = deModel.Store.DeCats.FirstOrDefault(cat => cat.ID == DeOrderLine.CatID).DeGroup.FirstOrDefault(grp => grp.ID == DeOrderLine.GrpID).DeProducts.FirstOrDefault(prd => prd.ID == DeOrderLine.ProID);
                            basketViewCellModel.Id = DeOrderLine.ID;
                            basketViewCellModel.Product = DeProduct;
                            basketViewCellModel.IsDeal = false;
                            basketViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + DeProduct.ImgUrl;
                            basketViewCellModel.Qty = DeOrderLine.Qty.ToString();
                            basketViewCellModel.Name = DeOrderLine.Name4Binding;
                            basketViewCellModel.Price = deModel.PriceFormatting(DeOrderLine.Price);
                            if (DeOrderLine.Toppings != null && DeOrderLine.Toppings.Count > 0)
                            {
                                basketViewCellModel.IsCustomize = true;
                            }
                            else
                            {
                                basketViewCellModel.IsCustomize = false;
                            }
                        }
                        else
                        {
                            WinPizzaData.WPBaseProduct DeProduct = deModel.Store.DeCats.FirstOrDefault(cat => cat.ID == DeOrderLine.CatID).DeGroup.FirstOrDefault(grp => grp.ID == DeOrderLine.GrpID).DeProducts.FirstOrDefault(prd => prd.ID == DeOrderLine.ProID);
                            basketViewCellModel.Id = DeOrderLine.ID;
                            basketViewCellModel.OfferId = DeOrderLine.OfferID;
                            basketViewCellModel.Product = DeProduct;
                            basketViewCellModel.IsDeal = true;
                            basketViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + DeProduct.ImgUrl;
                            basketViewCellModel.Qty = DeOrderLine.Qty.ToString();
                            basketViewCellModel.Name = DeOrderLine.Name4Binding;
                            basketViewCellModel.Price = deModel.PriceFormatting(DeOrderLine.Price);
                            basketViewCellModel.IsCustomize = true;
                        }
                        basketViewCellModel.ButtonClicked += BasketViewCellModel_ButtonClicked;
                        basketViewCellModel.DeleteViewCell += BasketViewCellModel_DeleteViewCell;
                        BasketViewCell basketViewCell = new BasketViewCell() { BindingContext = basketViewCellModel };
                        xName_List.Add(basketViewCell);
                        _Model.Datas.Add(basketViewCellModel);
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

        private void BasketViewCellModel_DeleteViewCell(object sender, string e)
        {
            _Model.Datas.Remove(_Model.Datas.Where(i => i.Id == e).Single());
            xName_List.Clear();
            foreach (var data in _Model.Datas)
            {
                BasketViewCell basketViewCell = new BasketViewCell() { BindingContext = data };
                xName_List.Add(basketViewCell);
            }
        }

        private void BasketViewCellModel_ButtonClicked(object sender, ResultAddToBasket e)
        {
            _Model.TotalPrice = e.TotalPrice;
            _Model.Count = e.TotalItemCount.ToString();
        }
       
        protected override void OnDisappearing()
        {
            xName_List.Clear();
        }
    }
}