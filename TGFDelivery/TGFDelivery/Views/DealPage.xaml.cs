using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.Data;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Resources;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealPage : ContentPage
    {
        private DealPageModel _Model;
        private string _Id = string.Empty;
        private string _OrderLineId = string.Empty;
        public DealPage(string Id, string OrderLineID)
        {
            InitializeComponent();
            _Id = Id;
            _OrderLineId = OrderLineID;
            
            
            //Init();
        }
        protected override void OnAppearing()
        {
            BindingContext = _Model = new DealPageModel();
            Init();
        }
        private async void Init()
        {
            App.Loading(this);
            try
            {
                DealModel deModel = new DealModel();
                await Task.Run( () => 
                {
                    deModel = DataManager.Deal(_Id, _OrderLineId);
                    
                });
                _Model.PageTitleName = "Deal";
                _Model.DealName = deModel.OfferOrderLine.Name;
                _Model.Price = deModel.PriceFormatting(deModel.OfferOrderLine.Price);
                if (deModel.IsAllItemPicked)
                {
                    _Model.IsVisible = true;
                    _Model.txt_AddBasket = Resource.deal_ADDTOBASKET;
                }
                else
                {
                    _Model.IsVisible = false;
                }
                int index = 1;
                //Product list for deal
                foreach (OrderLine TheOrderLine in deModel.OfferOrderLine.DeOrderLines)
                {
                    CollectionData collectionData = new CollectionData();

                    // Get category name for each offeritem
                    var TheName = TheOrderLine.DeOfferItem.Name;
                    if (TheOrderLine.DeOfferItem.DeGroup != null)
                    {
                        TheName = TheOrderLine.DeOfferItem.DeGroup.Name;
                    }
                    else if (TheOrderLine.DeOfferItem.DeCat != null)
                    {
                        TheName = TheOrderLine.DeOfferItem.DeCat.Name;
                    }
                    collectionData.Index = index.ToString();
                    if (string.IsNullOrEmpty(TheOrderLine.ID) || (TheOrderLine.ID == "0"))
                    {
                        collectionData.Name = string.Format("{0}.{1}", index.ToString(), TheOrderLine.Name);
                        collectionData.Desc = string.Format("{0} {1}", Resource.deal_Chooseyour, TheName);
                    }
                    else
                    {
                        collectionData.Name = TheOrderLine.Name4Binding;
                        collectionData.Desc = Resource.deal_Selected;
                    }

                    // Product list for deal
                    if (string.IsNullOrEmpty(TheOrderLine.DeOfferItem.ProductID))
                    {
                        if (TheOrderLine.DeOfferItem.DeCat != null)
                        {
                            var productItem = TheOrderLine.DeOfferItem.DeCat.DeGroup.FirstOrDefault().DeProducts.FirstOrDefault();
                            if (!productItem.IsSingelPrice() && productItem.CanHvItem == true)
                            {
                                collectionData.IsCustomize = true;
                                for (int nGroupIndex = 0; nGroupIndex < TheOrderLine.DeOfferItem.DeCat.DeGroup.Count; nGroupIndex++)
                                {
                                    for (int i = 0; i < TheOrderLine.DeOfferItem.DeCat.DeGroup[nGroupIndex].DeProducts.Count; i++)
                                    {
                                        var CustomizeIndex = index * 100 + i;
                                        var product = TheOrderLine.DeOfferItem.DeCat.DeGroup[nGroupIndex].DeProducts[i];
                                        if (!product.SoloInStore && product.IsActive && !product.IsFreeChoice && !product.isHalfandHalf && !product.CreateYourOwn && !product.PushSale)
                                        {
                                            collectionData.Products.Add(product);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                collectionData.IsCustomize = false;
                                for (int i = 0; i < TheOrderLine.DeOfferItem.DeCat.DeGroup.FirstOrDefault(p => p.ID == TheOrderLine.DeOfferItem.GroupID).DeProducts.Count; i++)
                                {
                                    var product = TheOrderLine.DeOfferItem.DeCat.DeGroup.FirstOrDefault(p => p.ID == TheOrderLine.DeOfferItem.GroupID).DeProducts[i];
                                    collectionData.Products.Add(product);
                                }
                            }
                        }
                        else
                        {
                            collectionData.IsCustomize = false;
                            for (int i = 0; i < TheOrderLine.DeOfferItem.DeOptionOfferItem.Count; i++)
                            {
                                for (int idxProduct = 0; idxProduct < TheOrderLine.DeOfferItem.DeOptionOfferItem[i].DeGroup.DeProducts.Count; idxProduct++)
                                {
                                    var product = TheOrderLine.DeOfferItem.DeOptionOfferItem[i].DeGroup.DeProducts[idxProduct];
                                    collectionData.Products.Add(product);
                                }
                            }
                        }
                    }
                    _Model.Datas.Add(collectionData);
                    index++;
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
        protected override void OnDisappearing()
        {
            
        }
    }
}