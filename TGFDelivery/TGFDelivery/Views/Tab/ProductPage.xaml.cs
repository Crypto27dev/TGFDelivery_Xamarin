using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.CustomViews;
using TGFDelivery.Data;
using TGFDelivery.Helpers;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Models.ViewCellModel;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views.Tab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        private ProductPageModel _Model = new ProductPageModel();
        private Category _Category;
        List<BoxView> _listGroupBox = new List<BoxView>();
        public ProductPage()
        {
            _Category = null;
            InitializeComponent();
            Subscribe();
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<object>(this, AppSettings.STATUS_PRICE, (sender) =>
            {
                xName_BasketPrice.Text = DataManager.BASKET.DeOrderHeader.Total.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
            });
        }
        protected override void OnAppearing()
        {
            Init();
        }
        protected override void OnDisappearing()
        {
            xName_FlexLayout.Children.Clear();
            xName_Product_List.Clear();
        }
        public void Init()
        {
            if (_Category == null) return;

            Device.BeginInvokeOnMainThread(async () => {
                App.Loading(this);
                try
                {
                    xName_BasketPrice.Text = DataManager.BASKET.DeOrderHeader.Total.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;

                    //_Category = this.BindingContext as Category;
                    if (_Category.DeGroup.Count == 1)
                    {
                        MenuModel deModel = await DataManager.Index(null, null, _Category.ID, _Category.DeGroup[0].Name);
                        Draw(deModel);
                    }
                    else
                    {
                        foreach (var deGroup in _Category.DeGroup)
                        {
                            // Draw Group Button
                            StackLayout stackLayout = new StackLayout() { Orientation = StackOrientation.Vertical, Spacing = 0 };
                            Button btn = new Button()
                            {
                                Text = deGroup.Name,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.White,
                                Padding = new Thickness(20, 0), 
                                FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                                Command = new Command(() =>
                                {
                                    GroupButton_Clicked(_Category.ID, deGroup.Name);
                                })
                            };
                            BoxView boxView = new BoxView() { HeightRequest = 5, StyleId = deGroup.Name };
                            stackLayout.Children.Add(btn);
                            stackLayout.Children.Add(boxView);
                            _listGroupBox.Add(boxView);
                            xName_FlexLayout.Children.Add(stackLayout);
                        }
                        //if (DataManager.SelectedProduct.ContainsKey(_Category.ID))
                        //{
                        //    foreach (KeyValuePair<string, string> item in DataManager.SelectedProduct)
                        //    {
                        //        if (item.Key == _Category.ID)
                        //        {
                        //            await GroupButton_Clicked(item.Key, item.Value);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        await GroupButton_Clicked(_Category.ID, _Category.DeGroup[0].Name);
                        //}

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
            });
        }
        private async Task GroupButton_Clicked(string CategoryId, string GroupName)
        {
            App.Loading(this);
            try
            {
                foreach (BoxView box in _listGroupBox)
                {
                    box.BackgroundColor = Color.Transparent;
                    if (box.StyleId == GroupName)
                    {
                        box.BackgroundColor = Color.White;
                    }
                }
                xName_Product_List.Clear();
                MenuModel deModel = await DataManager.Index(null, null, CategoryId, GroupName);
                Draw(deModel);

                if (DataManager.SelectedProduct.ContainsKey(CategoryId))
                {
                    DataManager.SelectedProduct[CategoryId] = GroupName;
                }
                else
                {
                    DataManager.SelectedProduct.Add(CategoryId, GroupName);
                }
                
            }
            catch (Exception ex)
            {

                
            }
            finally
            {
                App.Stop(this);
            }
        }
        private void Draw(MenuModel deModel)
        {
            //Banner
            if(deModel.Product4Sale != null && !deModel.IsStoreClosed)
            {
                ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                productViewCellModel.Name = "Banner";
                productViewCellModel.CatID = deModel.Product4Sale.CatID;
                productViewCellModel.GroupID = deModel.Product4Sale.GrpID;
                productViewCellModel.ProductID = deModel.Product4Sale.ID;
                productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + deModel.Product4Sale.ImgUrl;

                SetCreateButton(productViewCellModel);
                ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                productViewCell.Tapped += ProductViewCell_Tapped;
                xName_Product_List.Add(productViewCell);
            }
            if (deModel.CategoryList.FirstOrDefault().ID == deModel.SelectedCategoryID)
            {
                //Create your own
                if (deModel.Product4CreateYourOwn != null && !deModel.IsStoreClosed)
                {
                    ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                    productViewCellModel.Name = "Create Your OWN";
                    productViewCellModel.CatID = deModel.Product4CreateYourOwn.CatID;
                    productViewCellModel.GroupID = deModel.Product4CreateYourOwn.GrpID;
                    productViewCellModel.ProductID = deModel.Product4CreateYourOwn.ID;
                    productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + deModel.Product4CreateYourOwn.ImgUrl;

                    SetCreateButton(productViewCellModel);

                    ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                    productViewCell.Tapped += ProductViewCell_Tapped;
                    xName_Product_List.Add(productViewCell);
                }
                else
                {

                }

                //Pizza legend
                if (deModel.Product4Legend != null && !deModel.IsStoreClosed)
                {
                    ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                    productViewCellModel.Name = "Pizza legend";
                    productViewCellModel.CatID = deModel.Product4Legend.CatID;
                    productViewCellModel.GroupID = deModel.Product4Legend.GrpID;
                    productViewCellModel.ProductID = deModel.Product4Legend.ID;
                    productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + deModel.Product4Legend.ImgUrl;

                    SetCreateButton(productViewCellModel);

                    ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                    productViewCell.Tapped += ProductViewCell_Tapped;
                    xName_Product_List.Add(productViewCell);
                }
                else
                {

                }

                //Half and Half
                if (deModel.Product4Half != null && !deModel.IsStoreClosed)
                {
                    ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                    productViewCellModel.Name = "Half and Half";
                    productViewCellModel.CatID = deModel.Product4Half.CatID;
                    productViewCellModel.GroupID = deModel.Product4Half.GrpID;
                    productViewCellModel.ProductID = deModel.Product4Half.ID;
                    productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + deModel.Product4Half.ImgUrl;

                    SetCreateButton(productViewCellModel);

                    ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                    productViewCell.Tapped += ProductViewCell_Tapped;
                    xName_Product_List.Add(productViewCell);
                }
                else
                {

                }
            }
            //Product list
            foreach (WinPizzaData.Group group in deModel.ShowCategory.DeGroup)
            {
                if (group.Name == deModel.ShowGroup)
                {
                    foreach (WinPizzaData.WPBaseProduct product in group.DeProducts)
                    {
                        if (!product.SoloInStore && product.IsActive && !product.CreateYourOwn && !product.isHalfandHalf && !product.IsFreeChoice && !product.PushSale)
                        {
                            ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                            productViewCellModel.Name = product.Name;
                            productViewCellModel.BtnName = "ADD";
                            productViewCellModel.BtnBackgroundColor = "#00ed00";
                            productViewCellModel.BtnBorderColor = "#00ed00";
                            productViewCellModel.BtnBorderWidth = "0";
                            productViewCellModel.BtnParam = "ADD";
                            productViewCellModel.BtnIsVisible = true;
                            productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + product.ImgUrl;

                            //productViewCellModel.wPBaseProduct = product;
                            productViewCellModel.CatID = product.CatID;
                            productViewCellModel.GroupID = product.GrpID;
                            productViewCellModel.ProductID = product.ID;
                            productViewCellModel.AddBtn_Clicked += ProductViewCellModel_AddBtn_Clicked;
                            if (!product.IsSingelPrice() && product.CanHvItem == true && !DataManager.MenuModel.IsStoreClosed)
                            {
                                productViewCellModel.Price = product.DeGroupedPrices.DePrices.FirstOrDefault().DeMixOption.Name.Replace(",", " ") + product.DeGroupedPrices.DePrices.FirstOrDefault().Amount.ToString();

                                ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                                productViewCell.Tapped += ProductViewCell_Tapped;
                                xName_Product_List.Add(productViewCell);
                            }
                            else if (!DataManager.MenuModel.IsStoreClosed)
                            {
                                productViewCellModel.Price = product.DePrice.ToString((CultureInfo)CultureInfo.CurrentCulture) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
                                ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                                xName_Product_List.Add(productViewCell);
                            }
                        }
                    }
                }
            }
        }

        private void ProductViewCellModel_AddBtn_Clicked(object sender, string e)
        {
            
        }

        public void SetCreateButton(ProductViewCellModel productViewCellModel)
        {
            productViewCellModel.BtnName = "CREATE";
            productViewCellModel.BtnBackgroundColor = "transparent";
            productViewCellModel.BtnBorderColor = "White";
            productViewCellModel.BtnBorderWidth = "2";
            productViewCellModel.BtnParam = "CREATE";
            productViewCellModel.BtnIsVisible = true;
        }
        private void ProductViewCell_Tapped(object sender, EventArgs e)
        {
            App.Loading(this);
            try
            {
                ViewCell vc = (ViewCell)sender;
                ProductViewCellModel productViewCellModel = (ProductViewCellModel)vc.BindingContext;
                if (DataManager.CUSTOM_PRODUCT != null)
                {
                    DataManager.CUSTOM_ORDERLINE = null;
                    DataManager.CUSTOM_SIDE_INDEX = null;
                    DataManager.CUSTOM_PRODUCT = null;
                }
                if (DataManager.CustomizeModel != null)
                {
                    DataManager.CustomizeModel = null;
                }
                App._NavigationPage.PushAsync(new ProductDetailPage(productViewCellModel.CatID, productViewCellModel.GroupID, productViewCellModel.ProductID, null, -1));
            }
            catch (Exception)
            {

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

        public void SetCategory(Category cate)
        {
            this._Category = cate;
        }
    }
}