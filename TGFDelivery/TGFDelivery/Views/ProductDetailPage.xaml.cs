using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGFDelivery.Data;
using TGFDelivery.Helpers;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Models.ViewCellModel;
using TGFDelivery.Resources;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private enum CustomizeData
        {
            Size,
            Crust,
            Topping
        }
        private string CatID { get; set; }
        private string GroupID { get; set; }
        private string ProductID { get; set; }
        private string OfferIndex { get; set; }
        private int OrderLindID { get; set; }
        public ProductDetailPage(string CatID, string GroupID, string ProductID, string OfferIndex, int OrderLindID)
        {
            InitializeComponent();
            this.CatID = CatID;
            this.GroupID = GroupID;
            this.ProductID = ProductID;
            this.OfferIndex = OfferIndex;
            this.OrderLindID = OrderLindID;
        }
        protected override void OnAppearing()
        {
            Init();
        }
        private void Init()
        {
            App.Loading(this);
            try
            {
                xName_Layout.Children.Clear();

                CustomizeModel DeModel = DataManager.Customize(this.CatID , this.GroupID, this.ProductID, this.OfferIndex, this.OrderLindID);
                if (DeModel.Product.isHalfandHalf)
                {
                    if (0 < DeModel.TheOrderLine.Sides.Count && DeModel.TheOrderLine.Sides.Count < DeModel.SideNumber)
                    {
                        DeModel.TheOrderLine.Sides.Clear();
                    }
                }

                DataManager.CustomizeModel = DeModel;

                xName_ImgUrl.Source = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + DeModel.Product.ImgUrl;
                xName_Name.Text = DeModel.TheOrderLine.SelectedProduct.Name;
                //if (!string.IsNullOrEmpty(DataManager.HalfNHalf_Price))
                //{
                //    xName_Price.Text = DataManager.HalfNHalf_Price;
                //}
                //else
                //{
                //    xName_Price.Text = DeModel.PriceFormatting(DeModel.TheOrderLine.Price);
                //}

                //Size
                if (DeModel.GroupedLinq != null && DeModel.GroupedLinq.ContainsKey(DeModel.ListGroupKey[0]))
                {
                    Draw(DeModel, CustomizeData.Size);
                }
                //Crust
                if (DeModel.GroupedLinq != null && DeModel.ListGroupKey.Count > 1 && DeModel.GroupedLinq.ContainsKey(DeModel.ListGroupKey[1]))
                {
                    Draw(DeModel, CustomizeData.Crust);
                }
                //Topping
                if (DeModel.AllToppings != null)
                {
                    Draw(DeModel, CustomizeData.Topping);
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
        private void Draw(CustomizeModel DeModel, CustomizeData index)
        {
            StackLayout stackLayout = new StackLayout() { BackgroundColor = Color.FromHex("#2d2d2d"), HeightRequest = 50};
            
            if (index == CustomizeData.Size)
            {
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        Size_Tapped();
                    })
                };
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                Grid grid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) });

                Label title = new Label()
                {
                    Text = DeModel.ListGroupKey[0],
                    FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                    TextColor = Color.White,
                    Margin = new Thickness(15, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center
                };
                grid.Children.Add(title, 0, 0);
                
                foreach (WinPizzaData.Option size in DeModel.GroupedLinq[DeModel.ListGroupKey[0]])
                {
                    if (DeModel.TheOrderLine.DeMixedOption.OptionList.FirstOrDefault(o => o.Value.Name == size.Name).Value != null)
                    {
                        Label desc = new Label()
                        {
                            Text = size.Name,
                            FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                            TextColor = Color.White,
                            FontSize = 20
                        };
                        grid.Children.Add(desc, 1, 0);
                    }
                }
                stackLayout.Children.Add(grid);
            }
            else if (index == CustomizeData.Crust)
            {
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        Crust_Tapped();
                    })
                };
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                Grid grid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) });

                Label title = new Label()
                {
                    Text = DeModel.ListGroupKey[1],
                    FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                    TextColor = Color.White,
                    Margin = new Thickness(15, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center
                };
                grid.Children.Add(title, 0, 0);
                foreach (WinPizzaData.Option crust in DeModel.GroupedLinq[DeModel.ListGroupKey[1]])
                {
                    if (DeModel.TheOrderLine.DeMixedOption.OptionList.FirstOrDefault(o => o.Value.Name == crust.Name).Value != null)
                    {
                        Label desc = new Label()
                        {
                            Text = crust.Name,
                            FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                            TextColor = Color.White,
                            FontSize = 20
                        };
                        grid.Children.Add(desc, 1, 0);
                    }
                }
                stackLayout.Children.Add(grid);
            }
            else if (index == CustomizeData.Topping)
            {
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        Toppings_Tapped();
                    })
                };
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                var TitleName4Topping = string.Empty;
                if (DeModel.Product.isHalfandHalf)
                {
                    xName_Price.Text = DataManager.HalfNHalf_Price;
                    TitleName4Topping = "HALF AND HALF";
                }
                else
                {
                    xName_Price.Text = DeModel.PriceFormatting(DeModel.TheOrderLine.Price);
                    TitleName4Topping = DeModel.ToppingGroupName;
                }
                Grid grid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) });

                Label title = new Label()
                {
                    Text = TitleName4Topping,
                    FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                    TextColor = Color.White,
                    Margin = new Thickness(15, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center
                };
                grid.Children.Add(title, 0, 0);
                Label desc = new Label()
                {
                    Text = "Toppings",
                    FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                    TextColor = Color.White,
                    FontSize = 20
                };
                grid.Children.Add(desc, 1, 0);

                #region "If user don't choose all sides then back"
                ////if (!string.IsNullOrEmpty(DeModel.TheOrderLine.DeMixedOption.DeSideDef?.ID))
                ////{
                ////    int Start = 0;
                ////    if (DeModel.Product.isHalfandHalf)
                ////    {
                ////        Start = 1;
                ////    }
                ////    while (DeModel.SideNumber >= Start)
                ////    {
                ////        string SidName = string.Empty;
                ////        if (DeModel.TheOrderLine.IsThisHnH())
                ////        {
                ////            if (Start != 0)
                ////            {
                ////                if (DeModel.TheOrderLine.Sides.Count() >= Start && DeModel.TheOrderLine.Sides[Start - 1].DeProduct != null)
                ////                {
                ////                    SidName = DeModel.TheOrderLine.Sides[Start - 1].ProductName;
                ////                }
                ////                else
                ////                {
                ////                    SidName = "Side " + Start.ToString();
                ////                }
                ////            }
                ////            else
                ////            {
                ////                SidName = "";
                ////            }
                ////        }
                ////        else
                ////        {
                ////            SidName = Start == 0 ? DeModel.TheOrderLine.Name : Resource.customize_Side + Start.ToString();
                ////        }
                ////        if (DeModel.Product.isHalfandHalf)
                ////        {
                ////            if (DeModel.TheOrderLine.Sides.Count != 0 && DeModel.TheOrderLine.Sides[Start - 1].DeProduct != null)
                ////            {
                ////                SidName = DeModel.TheOrderLine.Sides[Start - 1].DeProduct.Name;
                ////            }
                ////        }
                ////        if (Start == DeModel.CurrentSideIndex && (!DeModel.Product.isHalfandHalf || DeModel.TheOrderLine.Sides.Count > 0))
                ////        {
                ////            Label desc = new Label()
                ////            {
                ////                Text = SidName,
                ////                FontFamily = Device.RuntimePlatform == Device.iOS ? "Oswald" : Device.RuntimePlatform == Device.Android ? "Oswald[wght].ttf#Oswald[wght]" : "Assets/Oswald[wght].ttf",
                ////                TextColor = Color.White,
                ////                FontSize = 20
                ////            };
                ////            grid.Children.Add(desc, 1, 0);
                ////        }
                ////        Start++;
                ////    }
                ////}
                #endregion
                stackLayout.Children.Add(grid);
            }
            xName_Layout.Children.Add(stackLayout);
        }
        private void Size_Tapped()
        {
            App.Loading(this);
            try
            {
                App._NavigationPage.PushAsync(new SizePage() { Title = DataManager.CustomizeModel.ListGroupKey[0] });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //App.Stop(this);
            }
            
        }
        private void Crust_Tapped()
        {
            App.Loading(this);
            try
            {
                App._NavigationPage.PushAsync(new CrustPage() { Title = DataManager.CustomizeModel.ListGroupKey[1] });
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //App.Stop(this);
            }
            
        }
        private async void Toppings_Tapped()
        {
            App.Loading(this);
            DataManager.HALF_PRODUCT.Clear();
            DataManager.HalfNHalf_Price = "";
            var TitleName4Topping = string.Empty;
            if (DataManager.CustomizeModel.Product.isHalfandHalf)
            {
                TitleName4Topping = "HALF AND HALF";
            }
            else
            {
                TitleName4Topping = DataManager.CustomizeModel.ToppingGroupName;
            }

            if (!string.IsNullOrEmpty(DataManager.CustomizeModel.TheOrderLine.DeMixedOption.DeSideDef?.ID))
            {
                var res = await Task.Run(() => proc_Toppings_Tapped(TitleName4Topping));
                await App._NavigationPage.PushAsync(res);
            }
            else
            {
                await App._NavigationPage.PushAsync(new SideSelectPage2Toppings(10) { Title = TitleName4Topping, BackgroundColor = Color.Black });
            }
            
            App.Stop(this);
        }
        private SideTabbedPage2Toppings proc_Toppings_Tapped(string TitleName4Topping)
        {
            try
            {
                SideTabbedPage2Toppings sideTabbedPage = new SideTabbedPage2Toppings() { Title = TitleName4Topping };
                int Start = 0;
                if (DataManager.CustomizeModel.Product.isHalfandHalf)
                {
                    Start = 1;
                }
                while (DataManager.CustomizeModel.SideNumber >= Start)
                {
                    string SidName = string.Empty;
                    if (DataManager.CustomizeModel.TheOrderLine.IsThisHnH())
                    {
                        if (Start != 0)
                        {
                            if (DataManager.CustomizeModel.TheOrderLine.Sides.Count() >= Start && DataManager.CustomizeModel.TheOrderLine.Sides[Start - 1].DeProduct != null)
                            {
                                SidName = DataManager.CustomizeModel.TheOrderLine.Sides[Start - 1].ProductName;
                            }
                            else
                            {
                                SidName = "Side " + Start.ToString();
                            }
                        }
                        else
                        {
                            SidName = "";
                        }
                    }
                    else
                    {
                        SidName = Start == 0 ? DataManager.CustomizeModel.TheOrderLine.Name : Resource.customize_Side + Start.ToString();
                    }
                    if (DataManager.CustomizeModel.Product.isHalfandHalf)
                    {
                        if (DataManager.CustomizeModel.TheOrderLine.Sides.Count != 0 && DataManager.CustomizeModel.TheOrderLine.Sides[Start - 1].DeProduct != null)
                        {
                            SidName = DataManager.CustomizeModel.TheOrderLine.Sides[Start - 1].DeProduct.Name;
                        }
                    }
                    if (Start == DataManager.CustomizeModel.CurrentSideIndex && (!DataManager.CustomizeModel.Product.isHalfandHalf || DataManager.CustomizeModel.TheOrderLine.Sides.Count > 0))
                    {
                        sideTabbedPage.Children.Add(new SideSelectPage2Toppings(Start) { Title = SidName, BackgroundColor = Color.Black });
                    }
                    else
                    {
                        sideTabbedPage.Children.Add(new SideSelectPage2Toppings(Start) { Title = SidName, BackgroundColor = Color.Black });
                    }
                    Start++;

                }
                return sideTabbedPage;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        private void Back_Tapped(object sender, EventArgs e)
        {
            App._NavigationPage.PopAsync();
        }

        private async void Add2Order_Clicked(object sender, EventArgs e)
        {
            App.Loading(this);
            var result = await Task.Run(() => proc_Add2Order());
            
            if (result == "unpicked")
            {
                await DisplayAlert("Warnning", "Please select pizza for Half and Half", "Cancel");
                App.Stop(this);
                return;
            }
            if (!string.IsNullOrEmpty(this.OfferIndex))
            {
                //To Deal
                App._NavigationPage.Navigation.RemovePage(App._NavigationPage.Navigation.NavigationStack[App._NavigationPage.Navigation.NavigationStack.Count - 2]);
                await App._NavigationPage.PopAsync();
                return;
            }
            if (this.OrderLindID > 0)
            {
                //To Basket
                await App._NavigationPage.PopAsync();
                return;
            }
            await App._NavigationPage.PopAsync();
            App.Stop(this);
        }
        private string proc_Add2Order() 
        {
            try
            {
                string result = DataManager.AddToOrder(Int32.Parse(xName_Qty.Text));
                return result;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {

            }
        }

        private void Plus_Clicked(object sender, EventArgs e)
        {
            if((Int32.Parse(xName_Qty.Text) + 1) == 11)
            {
                return;
            }
            xName_Qty.Text = (Int32.Parse(xName_Qty.Text) + 1).ToString();
        }

        private void Minus_Clicked(object sender, EventArgs e)
        {
            if ((Int32.Parse(xName_Qty.Text) - 1) == 0)
            {
                return;
            }
            xName_Qty.Text = (Int32.Parse(xName_Qty.Text) - 1).ToString();
        }
    }
}