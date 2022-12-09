using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.Data;
using TGFDelivery.Helpers.Data;
using TGFDelivery.Models.ViewCellModel;
using TGFDelivery.Resources;
using WinPizzaData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideSelectPage2Toppings : ContentPage
    {
        private int _Start;
        public SideSelectPage2Toppings(int Start)
        {
            InitializeComponent();
            _Start = Start;
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
                await Task.Delay(200);
                if (DataManager.HALF_PRODUCT.ContainsKey(DataManager.GetSessionKeyFromSideIndex(_Start)))
                {
                    //Draw_HalfNHalfProduct(DataManager.HALF_PRODUCT[DataManager.GetSessionKeyFromSideIndex(_Start)]);
                    return;
                }
                xName_Vegetarian.Text = Resource.customize_Vegetarian;
                xName_GlutenFree.Text = Resource.customize_GlutenFree;
                xName_HotSpicy.Text = Resource.customize_HotSpicy;
                xName_List.Clear();
                List<SelectedSide> SideToppings = new List<SelectedSide>();
                if (_Start != 10)
                {
                    SideToppings = DataManager.SelectSide(_Start);
                }

                if (DataManager.CustomizeModel.Product.isHalfandHalf)
                {
                    //Half and Half
                    xName_TableView.RowHeight = 100;
                    for (int nProductIndex = 0; nProductIndex < DataManager.CustomizeModel.AllProducts.Count; nProductIndex++)
                    {
                        var productItem = DataManager.CustomizeModel.AllProducts[nProductIndex];
                        if (!productItem.IsSingelPrice() && productItem.CanHvItem == true)
                        {
                            ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                            productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + productItem.ImgUrl;
                            productViewCellModel.Name = productItem.Name;
                            productViewCellModel.CatID = productItem.CatID;
                            productViewCellModel.GroupID = productItem.GrpID;
                            productViewCellModel.ProductID = productItem.ID;
                            productViewCellModel.BtnIsVisible = false;
                            productViewCellModel.Price = productItem.DeGroupedPrices.DePrices.FirstOrDefault().DeMixOption.Name.Replace(",", " ") + productItem.DeGroupedPrices.DePrices.FirstOrDefault().Amount.ToString();
                            ProductViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel }; 
                            productViewCell.Tapped += ProductViewCell_Tapped;
                            xName_List.Add(productViewCell);
                        }
                        else
                        {
                            ProductViewCellModel productViewCellModel = new ProductViewCellModel();
                            productViewCellModel.ImgUrl = DataManager.StoreProfile.DeStoreLinks.Photo + productItem.ImgUrl;
                            productViewCellModel.Name = productItem.Name;
                            productViewCellModel.CatID = productItem.CatID;
                            productViewCellModel.GroupID = productItem.GrpID;
                            productViewCellModel.ProductID = productItem.ID;
                            productViewCellModel.BtnIsVisible = false;
                            ProductViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                            xName_List.Add(productViewCell);
                        }
                    }
                }
                else
                {
                    xName_TableView.RowHeight = 60;
                    List<WinPizzaData.ItemOnProduct> ToppingLayout = WinPizzaData.ItemOnProduct.ToppingLayout(DataManager.CustomizeModel.AllToppings, DataManager.CustomizeModel.TheOrderLine.Toppings);
                    foreach (WinPizzaData.ItemOnProduct topping in ToppingLayout)
                    {
                        SideViewCell sideViewCell = null;
                        foreach (SelectedSide seletedtopping in SideToppings)
                        {
                            if (topping.ID == seletedtopping.ToppingID)
                            {
                                SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "#4a4b5d", seletedtopping.ToppingPortion);
                                sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                                //sideViewCell.Tapped += SideViewCell_Tapped;
                                sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                                xName_List.Add(sideViewCell);
                            }
                        }
                        if (sideViewCell == null)
                        {
                            SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "", 0);
                            sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                            //sideViewCell.Tapped += SideViewCell_Tapped;
                            sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                            xName_List.Add(sideViewCell);
                        }

                        //int portion = 0;
                        //if (topping.Sides.Count != 0 && ((DataManager.CustomizeModel.CurrentSideIndex == 0 && topping.Sides.Count == DataManager.CustomizeModel.SideNumber && topping.Portion > 0) || !WinPizzaData.ToppingSide.IsNUll(topping.Sides.FirstOrDefault(sid => sid.SideID == DataManager.CustomizeModel.CurrentSideIndex && sid.Portion > 0))))
                        //{
                        //    if (DataManager.CustomizeModel.CurrentSideIndex == 0 && topping.Sides.Count == DataManager.CustomizeModel.SideNumber)
                        //    {
                        //        portion = topping.Portion;
                        //    }
                        //    else if (!WinPizzaData.ToppingSide.IsNUll(topping.Sides.FirstOrDefault(sid => sid.SideID == DataManager.CustomizeModel.CurrentSideIndex)) && topping.Sides.FirstOrDefault(sid => sid.SideID == DataManager.CustomizeModel.CurrentSideIndex).SideID == DataManager.CustomizeModel.CurrentSideIndex)
                        //    {
                        //        portion = topping.Sides.FirstOrDefault(sid => sid.SideID == DataManager.CustomizeModel.CurrentSideIndex).Portion;
                        //    }
                        //    SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "#4a4b5d", portion);
                        //    sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                        //    sideViewCell.Tapped += SideViewCell_Tapped;
                        //    sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                        //    xName_List.Add(sideViewCell);
                        //}
                        //else
                        //{
                        //    SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "", portion);
                        //    sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                        //    sideViewCell.Tapped += SideViewCell_Tapped;
                        //    sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                        //    xName_List.Add(sideViewCell);
                        //}
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

        private async void ProductViewCell_Tapped(object sender, EventArgs e)
        {
            App.Loading(this);
            await Task.Delay(50);
            ViewCell vc = (ViewCell)sender;
            ProductViewCellModel productViewCellModel = (ProductViewCellModel)vc.BindingContext;
            this.Title = productViewCellModel.Name;
            xName_List.Clear();

            xName_TableView.RowHeight = 60;
            List<SelectedSide> SideToppings = DataManager.ChooseProduct4Half(productViewCellModel.CatID, productViewCellModel.GroupID, productViewCellModel.ProductID).ToppingList;
            if (Convert.ToDecimal(DataManager.ChooseProduct4Half(productViewCellModel.CatID, productViewCellModel.GroupID, productViewCellModel.ProductID).Price) > 0)
            {
                DataManager.HalfNHalf_Price = DataManager.ChooseProduct4Half(productViewCellModel.CatID, productViewCellModel.GroupID, productViewCellModel.ProductID).Price;
            }
            List<WinPizzaData.ItemOnProduct> ToppingLayout = WinPizzaData.ItemOnProduct.ToppingLayout(DataManager.CustomizeModel.AllToppings, DataManager.CustomizeModel.TheOrderLine.Toppings);
            foreach (WinPizzaData.ItemOnProduct topping in ToppingLayout)
            {
                SideViewCell sideViewCell = null;
                foreach (SelectedSide seletedtopping in SideToppings)
                {
                    if (topping.ID == seletedtopping.ToppingID)
                    {
                        SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "#4a4b5d", seletedtopping.ToppingPortion);
                        sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                        //sideViewCell.Tapped += SideViewCell_Tapped;
                        sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                        xName_List.Add(sideViewCell);
                    }
                }
                if (sideViewCell == null)
                {
                    SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "", 0);
                    sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                    //sideViewCell.Tapped += SideViewCell_Tapped;
                    sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                    xName_List.Add(sideViewCell);
                }
            }
            App.Stop(this);
        }

        private void SideViewCell_ButtonClicked(object sender, string e)
        {
            DisplayAlert("Error", Resource.customize_warningmsg, "Cancel");
        }

        private void Draw_HalfNHalfProduct(WPProduct HalfNHalfProduct)
        {
            List<SelectedSide> SideToppings = DataManager.ChooseProduct4Half(HalfNHalfProduct.CatID, HalfNHalfProduct.GrpID, HalfNHalfProduct.ID).ToppingList;
            List<WinPizzaData.ItemOnProduct> ToppingLayout = WinPizzaData.ItemOnProduct.ToppingLayout(DataManager.CustomizeModel.AllToppings, DataManager.CustomizeModel.TheOrderLine.Toppings);
            foreach (WinPizzaData.ItemOnProduct topping in ToppingLayout)
            {
                SideViewCell sideViewCell = null;
                foreach (SelectedSide seletedtopping in SideToppings)
                {
                    if (topping.ID == seletedtopping.ToppingID)
                    {
                        SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "#4a4b5d", seletedtopping.ToppingPortion);
                        sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                        //sideViewCell.Tapped += SideViewCell_Tapped;
                        sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                        xName_List.Add(sideViewCell);
                    }
                }
                if (sideViewCell == null)
                {
                    SideViewCellModel sideViewCellModel = new SideViewCellModel(topping, "", 0);
                    sideViewCell = new SideViewCell(sideViewCellModel) { BindingContext = sideViewCellModel };
                    //sideViewCell.Tapped += SideViewCell_Tapped;
                    sideViewCell.ButtonClicked += SideViewCell_ButtonClicked;
                    xName_List.Add(sideViewCell);
                }
            }
        }

        protected override void OnDisappearing()
        {
            
        }   
    }
}