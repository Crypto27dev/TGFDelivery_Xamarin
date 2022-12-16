using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TGFDelivery.Helpers.Data;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Services;
using WinPizzaData;
using WPUtility;

namespace TGFDelivery.Data
{
    public static class DataManager
    {

        public static ServersUrl AdjustServcePoints(ServersUrl DeServersUrl, FoodStoreProfile storeProfile)
        {
            storeProfile.DeStoreLinks.Logo = storeProfile.DeStoreLinks.Logo.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            storeProfile.DeStoreLinks.Photo = storeProfile.DeStoreLinks.Photo.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            // Get menu


            // Replace external IP
            DeServersUrl.AddressSRV = DeServersUrl.AddressSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.CallerIDSrv = DeServersUrl.CallerIDSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.MasterStoreSrv = DeServersUrl.MasterStoreSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.MenuSRV = DeServersUrl.MenuSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.MsgSRV = DeServersUrl.MsgSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.OrderProcessingSRV = DeServersUrl.OrderProcessingSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.OutLookSRV = DeServersUrl.OutLookSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.PeopleSrv = DeServersUrl.PeopleSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.PrintingEndPoint = DeServersUrl.PrintingEndPoint.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.StoreSrv = DeServersUrl.StoreSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            DeServersUrl.TillSRV = DeServersUrl.TillSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
            return DeServersUrl;
        }

        private static async Task<bool> GetStoreProfile(string DeStoreID)
        {
            if (Store == null || StoreProfile == null || SeversUrl == null)
            {
                FoodStoreProfile storeProfile = await CoreServices.GetStoreProfile(DeStoreID);
                // strStoreID);
                if (storeProfile == null)
                {
                    return false;
                }

                ServersUrl DeServersUrl = await CoreServices.GetWebServicesEndPoint(DeStoreID);

                //if (!_hostingEnvironment.IsProduction())
                //{
                storeProfile.DeStoreLinks.Logo = storeProfile.DeStoreLinks.Logo.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                storeProfile.DeStoreLinks.Photo = storeProfile.DeStoreLinks.Photo.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                // Get menu


                // Replace external IP
                DeServersUrl.AddressSRV = DeServersUrl.AddressSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.CallerIDSrv = DeServersUrl.CallerIDSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.MasterStoreSrv = DeServersUrl.MasterStoreSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.MenuSRV = DeServersUrl.MenuSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.MsgSRV = DeServersUrl.MsgSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.OrderProcessingSRV = DeServersUrl.OrderProcessingSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.OutLookSRV = DeServersUrl.OutLookSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.PeopleSrv = DeServersUrl.PeopleSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.PrintingEndPoint = DeServersUrl.PrintingEndPoint.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.StoreSrv = DeServersUrl.StoreSrv.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                DeServersUrl.TillSRV = DeServersUrl.TillSRV.Replace(AppSettings.InternalIP, AppSettings.ExternalIP);
                //}
                //else
                //{// 93.219.32.165
                //    storeProfile.DeStoreLinks.Logo = storeProfile.DeStoreLinks.Logo.Replace(InternalIP, _config.GetSection("ExternalIP").Value);
                //    storeProfile.DeStoreLinks.Photo = storeProfile.DeStoreLinks.Photo.Replace(InternalIP, _config.GetSection("ExternalIP").Value);
                //}


                Store store = await CoreServices.LoadMainMenu(storeProfile.StoreID, storeProfile.DeDataSourceName, DeServersUrl, AppSettings.MenuID);
                if (!store.IsValid)
                {
                    return false;
                }

                // Save store info
                Store = store;
                StoreProfile = storeProfile;
                SeversUrl = DeServersUrl;
            }
            return true;
        }
        public static async Task<MenuModel> Index(string OrderType, string Postcode, string CatID, string GroupName)
        {
            bool IsGetStoreProfile = await StoreDataSource.GetStoreProfile(AppSettings.StoreID);
            if (!IsGetStoreProfile)
            {
                // If don't get store profile, redirect to share site
                await UserDialogs.Instance.AlertAsync("Server Error", "Error", "Cancel");
            }
            // Basket process
            if (BASKET == null)
            {
                Basket basket = new Basket();
                BASKET = basket;
                IsStart = 1;
            }
            // Create StoreInfoModel
            MenuModel DeModel = new MenuModel();
            if (DeModel.SelectedCategoryID != CatID) DeModel.SelectedCategoryID = CatID;

            Store DeStore = DataManager.Store;
            if (DeStore.DeCats == null)
            {
                await UserDialogs.Instance.AlertAsync("Server Error", "Error", "Cancel");
                //// If don't get store profile, redirect to share site
                ///
            }
            FoodStoreProfile DeProfile = StoreProfile;
            ServersUrl DeServersUrl = SeversUrl;
            DeModel.IsStoreClosed = await CoreServices.GetStoreClosed(DeProfile.DeDataSourceName, DeServersUrl, DeStore);

            // Get categories to show from store info
            DeModel.CategoryList = new List<Category>();
            bool bFirstCat = false;
            foreach (Category cat in DeStore.DeCats)
            {
                if (cat.DisplyAble == true && cat.CatType == WPUtility.WinPizzaEnums.ItemType.PRODUCT)
                {
                    if (string.IsNullOrEmpty(CatID) && bFirstCat == false)
                    {
                        bFirstCat = true;
                        CatID = cat.ID;
                    }
                    DeModel.CategoryList.Add(cat);
                    if (cat.ID == CatID)
                    {
                        DeModel.ShowCategory = cat;
                        if (string.IsNullOrEmpty(GroupName))
                            GroupName = cat.DeGroup[0].Name;
                        DeModel.ShowGroup = GroupName;
                    }
                    if (DeModel.Product4CreateYourOwn == null || DeModel.Product4Half == null || DeModel.Product4Legend == null || DeModel.Product4Sale == null)
                    {
                        foreach (WinPizzaData.Group group in cat.DeGroup)
                        {
                            if (DeModel.Product4CreateYourOwn == null) DeModel.Product4CreateYourOwn = (WPProduct)group.DeProducts.FirstOrDefault(prd => prd.CreateYourOwn == true);
                            if (DeModel.Product4Half == null) DeModel.Product4Half = (WPProduct)group.DeProducts.FirstOrDefault(prd => prd.isHalfandHalf == true);
                            if (DeModel.Product4Legend == null) DeModel.Product4Legend = (WPProduct)group.DeProducts.FirstOrDefault(prd => prd.IsFreeChoice == true);
                            if (DeModel.Product4Sale == null) DeModel.Product4Sale = (WPProduct)group.DeProducts.FirstOrDefault(prd => prd.PushSale == true);
                        }
                    }
                }
            }

            DeModel.UserBasket = BASKET;

            if (IsStart != null)
            {
                DeModel.IsStart = true;
            }
            else
            {
                DeModel.IsStart = false;
            }

            if (DeModel.IsStoreClosed)
            {
                DateTime time = OpeningDate.GetNextOpening(DeStore.OnlineRoles.FirstOrDefault(p => p.DeDaysOfWeek == DateTime.Now.DayOfWeek.ToString()));
            }

            List<OpeningDate> roles = DeStore.OnlineRoles;
            List<string> TimeList = new List<string>();
            foreach (OpeningDate date in roles)
            {
                string StartTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeStartHh, date.DeOpeningTimes[0].DeStartMin);
                string EndTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeEndtHh, date.DeOpeningTimes[0].DeEndMin);
                TimeList.Add(string.Format("{0} - {1}", StartTime, EndTime));
            }

            DeModel.RoleList = TimeList;
            DataManager.IsStart = null;
            return DeModel;
        }
        public static CustomizeModel Customize(string CatID, string GroupID, string ProductID, string OfferIndex, int OrderLindID)
        {
            // Find product from CatID and GroupID and ProductID 

            WPProduct DeProduct = (WPProduct)Store.DeCats.FirstOrDefault
                                            (cat => cat.ID == CatID).DeGroup.FirstOrDefault
                                            (grp => grp.ID == GroupID).DeProducts.FirstOrDefault
                                            (prd => prd.ID == ProductID);
            if (DeProduct == null)
            {
                //
            }

            // Get Basket and Order line to customize on basket page
            OrderLine BasketOrderLine = null;


            // Changed by Hans 20200501 for fixing index error
            //if (OrderLindID > -1) BasketOrderLine = DeBasket.DeOrderLines[OrderLindID];
            if (OrderLindID > -1) BasketOrderLine = BASKET.DeOrderLines.FirstOrDefault(ordLine => ordLine.ID == OrderLindID.ToString());

            //if (CUSTOM_PRODUCT != null)
            //{
            //    CUSTOM_ORDERLINE = null;
            //    CUSTOM_SIDE_INDEX = null;
            //    CUSTOM_PRODUCT = null;
            //}

            // Create model and OrderLine
            CustomizeModel DeModel = new CustomizeModel();
            OrderLine DeOrderLine = new OrderLine();

            // Set product of OrderLine
            if (string.IsNullOrEmpty(OfferIndex))
            {
                // Menu product

                //if (OrderLine.IsNUll(CUSTOM_ORDERLINE) || DeProduct.isHalfandHalf)
                if (OrderLine.IsNUll(CUSTOM_ORDERLINE))
                {
                    // OrderLine is not exist in session
                    DeOrderLine = new OrderLine(DeProduct);
                    if (!OrderLine.IsNUll(BasketOrderLine))
                    {
                        DeOrderLine.Qty = BasketOrderLine.Qty;
                        DeOrderLine.DeMixedOption = BasketOrderLine.DeMixedOption;
                        DeOrderLine.Toppings = BasketOrderLine.Toppings;
                        DeOrderLine.Sides = BasketOrderLine.Sides; // HalfNHalf sides
                        for (int i = 0; i < DeOrderLine.Sides.Count; i++)
                        {
                            HALF_PRODUCT.Add(GetSessionKeyFromSideIndex(i + 1), DeOrderLine.Sides[i].DeProduct);
                        }
                        foreach (ItemOnProduct TheTopping in BasketOrderLine.Toppings)
                        {
                            TheTopping.DeProduct = Store.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == TheTopping.ID);
                        }
                    }
                }
                else
                {
                    // OrderLine is exist in session
                    if (!OrderLine.IsNUll(BasketOrderLine))
                    {
                        DeOrderLine = new OrderLine(DeProduct);
                        DeOrderLine.Qty = BasketOrderLine.Qty;
                        DeOrderLine.DeMixedOption = BasketOrderLine.DeMixedOption;
                        DeOrderLine.Toppings = BasketOrderLine.Toppings;
                        foreach (ItemOnProduct TheTopping in BasketOrderLine.Toppings)
                        {
                            TheTopping.DeProduct = Store.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == TheTopping.ID);
                        }
                    }
                    else
                    {
                        // refresh
                        DeOrderLine = CUSTOM_ORDERLINE;
                        //SetProductAndOption2OrderLine(Store, DeOrderLine, DeProduct, DeOrderLine.Qty);
                    }
                }
            }
            else
            {
                // Deal product
                OfferOrderLine TempOrderLine = DEAL_ORDERLINE;
                DeOrderLine = TempOrderLine.DeOrderLines[Int32.Parse(OfferIndex) - 1];
                DeOrderLine.SetProduct(DeProduct, DeProduct.DePrintingGrp, (decimal)Store.CalcVatValue((decimal)DeOrderLine.OfferPrice, (decimal)DeOrderLine.Vatrate));
            }

            // Get option list of product
            var OptionList = OrderLine.GetOptions(DeOrderLine);
            OrderLine.Update(DeOrderLine);
            Dictionary<string, List<Option>> GroupedLinq = null;
            DeModel.ListGroupKey = new List<String>();
            int Index = 0;
            if (OptionList.Count > 0)
            {
                GroupedLinq = OptionList.GroupBy(x => x.UIEID)
                             .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList());
                foreach (KeyValuePair<string, List<Option>> Group in GroupedLinq)
                {
                    DeModel.ListGroupKey.Add(Group.Key);
                    Group.Value.OrderBy(p => p.ModifierOrder);
                }
            }

            //if (_config.GetSection("Culture").Value == "de-DE")
            //{
            //    DeModel.ListGroupKey[0] = "Größe";
            //    DeModel.ListGroupKey[1] = "Kruste";
            //}
            //else
            //{
            //    DeModel.ListGroupKey[0] = "Size";
            //    DeModel.ListGroupKey[1] = "Crust";
            //}

            // Set current side index
            if (CUSTOM_SIDE_INDEX == null)
            {
                if (DeProduct.isHalfandHalf)
                {
                    CUSTOM_SIDE_INDEX = 1;
                }
                else
                {
                    CUSTOM_SIDE_INDEX = 0;
                }
            }

            // Set model
            DeModel.TheOrderLine = DeOrderLine;
            if (DeModel.TheOrderLine.DeMixedOption.DeSideDef == null) DeModel.IsExistSide = false;
            else DeModel.IsExistSide = true;

            if (!string.IsNullOrEmpty(DeProduct.ToppingGrpID))
            {
                WinPizzaData.Group ToppingGroup = Store.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID);
                DeModel.AllToppings = ToppingGroup.DeProducts;
                DeModel.ToppingGroupName = ToppingGroup.Name;
            }
            DeModel.Product = DeProduct;
            DeModel.GroupedLinq = GroupedLinq;
            DeModel.CurrentSideIndex = CUSTOM_SIDE_INDEX;
            DeModel.MaxToppingCount = Int32.Parse(AppSettings.MaxToppingCount);

            DeModel.AllProducts = Store.DeCats.FirstOrDefault(cat => cat.ID == DeProduct.CatID).DeGroup.FirstOrDefault().DeProducts.Where(p => p.CanBeOnHnH == true).ToList();

            // SideDef issue
            if (DeModel.TheOrderLine.DeMixedOption.DeSideDef == null)
            {
                DeModel.SideNumber = -1;
            }
            else
            {
                DeModel.SideNumber = DeModel.TheOrderLine.DeMixedOption.DeSideDef.Number;
            }

            List<OpeningDate> roles = Store.OnlineRoles;
            List<string> TimeList = new List<string>();
            foreach (OpeningDate date in roles)
            {
                string StartTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeStartHh, date.DeOpeningTimes[0].DeStartMin);
                string EndTime = string.Format("{0}:{1}", date.DeOpeningTimes[1].DeStartHh + date.DeOpeningTimes[1].DeEndtHh, date.DeOpeningTimes[1].DeStartMin + date.DeOpeningTimes[1].DeEndMin);
                TimeList.Add(string.Format("{0} - {1}", StartTime, EndTime));
            }

            DeModel.RoleList = TimeList;

            // HalfNHalf
            // Set Half Product on Side
            //if (DeProduct.isHalfandHalf)
            //{
            //    model.HalfProducts = new Dictionary<int, WPProduct>();
            //    for (int i = 0; i <= OrdeLineObject.DeMixedOption.DeSideDef.Number; i++)
            //    {
            //        if (HttpContext.Session.Get(GetSessionKeyFromSideIndex(i)) == null)
            //            continue;

            //        WPProduct SideProduct = (WPProduct)BinerySerializer.DeSerializer(typeof(WPProduct), HttpContext.Session.Get(GetSessionKeyFromSideIndex(i)));
            //        model.HalfProducts.Add(i, SideProduct);
            //    }
            //}

            if (OrderLine.IsNUll(BasketOrderLine) && DeOrderLine.DeMixedOption.DeSideDef != null)
            {
                for (int i = 0; i <= DeOrderLine.DeMixedOption.DeSideDef.Number; i++)
                {

                }
            }

            // Set session
            CUSTOM_PRODUCT = DeProduct;
            CUSTOM_ORDERLINE = DeOrderLine;


            // Set OfferOrderLine of Deal Page
            if (!string.IsNullOrEmpty(OfferIndex))
            {
                DEAL_SELECTED_OFFERID = OfferIndex;
            }

            if (OrderLindID > -1)
            {
                BASKET_SELECTED_ORDERLINEID = OrderLindID;
            }

            // Set view data
            return DeModel;
        }
        public static string SelectSize(string SizeName)
        {
            if (CUSTOM_ORDERLINE == null)
            {
                return "unsuccess";
            }

            WPProduct DeProduct = CUSTOM_PRODUCT;
            Store DeStore = Store;
            //SetProductAndOption2OrderLine(DeStore, CUSTOM_ORDERLINE, DeProduct, CUSTOM_ORDERLINE.Qty);
            if (DeProduct.isHalfandHalf)
            {
                // Set Half Product on Side
                for (int i = 0; i <= CUSTOM_ORDERLINE.DeMixedOption.DeSideDef.Number; i++)
                {
                    SetProduct4HalfNHalf(i, CUSTOM_ORDERLINE, DeProduct, DeStore, false);
                }
            }

            // Get NewOption from SizeName
            Option NewOption = null;
            foreach (Price ThePrice in DeProduct.DeGroupedPrices.DePrices)
            {
                NewOption = ThePrice.DeMixOption.OptionList.FirstOrDefault(opt => opt.Value.Name == SizeName).Value;
                if (NewOption != null) break;
            }

            CUSTOM_ORDERLINE.UpdateOption(NewOption);
            OrderLine.Update(CUSTOM_ORDERLINE);

            return CUSTOM_ORDERLINE.Price.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
        }
        public static string SelectSize2SizePage(string SizeName)
        {
            try
            {
                // Get NewOption from SizeName
                Option NewOption = null;
                foreach (Price ThePrice in CUSTOM_PRODUCT.DeGroupedPrices.DePrices)
                {
                    NewOption = ThePrice.DeMixOption.OptionList.FirstOrDefault(opt => opt.Value.Name == SizeName).Value;
                    if (NewOption != null)
                    {
                        return ThePrice.Amount.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return "";
        }
        public static string SelectCrust(string CrustName)
        {
            if (CUSTOM_ORDERLINE == null)
            {
                return "unsuccess";
            }
            OrderLine DeOrderLine = CUSTOM_ORDERLINE;
            WPProduct DeProduct = CUSTOM_PRODUCT;
            Store DeStore = Store;

            //SetProductAndOption2OrderLine(DeStore, DeOrderLine, DeProduct, DeOrderLine.Qty);

            if (DeProduct.isHalfandHalf)
            {
                // Set Half Product on Side
                for (int i = 0; i <= DeOrderLine.DeMixedOption.DeSideDef.Number; i++)
                {
                    SetProduct4HalfNHalf(i, DeOrderLine, DeProduct, DeStore, false);
                }
            }

            // Get NewOption from SizeName
            Option NewOption = null;
            foreach (Price ThePrice in DeProduct.DeGroupedPrices.DePrices)
            {
                NewOption = ThePrice.DeMixOption.OptionList.FirstOrDefault(opt => opt.Value.Name == CrustName).Value;
                if (NewOption != null) break;
            }

            if (NewOption != null)
            {
                DeOrderLine.UpdateOption(NewOption);
            }
            OrderLine.Update(DeOrderLine);
            CUSTOM_ORDERLINE = DeOrderLine;
            return DeOrderLine.Price.ToString();
        }
        public static string SelectCrust2CrustPage(string CrustName)
        {
            try
            {
                Option NewOption = null;
                foreach (Price ThePrice in CUSTOM_PRODUCT.DeGroupedPrices.DePrices)
                {
                    NewOption = ThePrice.DeMixOption.OptionList.FirstOrDefault(opt => opt.Value.Name == CrustName).Value;
                    if (NewOption != null)
                    {
                        return ThePrice.Amount.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "";
        }
        public static List<SelectedSide> SelectSide(int SideIndex)
        {
            if (CUSTOM_ORDERLINE == null)
            {
                return null;
            }

            OrderLine TheOrderLine = CUSTOM_ORDERLINE;
            CUSTOM_SIDE_INDEX = SideIndex;

            List<SelectedSide> SideToppings = new List<SelectedSide>();
            for (int i = 0; i < TheOrderLine.Toppings.Count; i++)
            {
                for (int k = 0; k < TheOrderLine.Toppings[i].Sides.Count; k++)
                {
                    if (SideIndex == 0 && TheOrderLine.Toppings[i].Sides.Count == TheOrderLine.DeMixedOption.DeSideDef.Number && TheOrderLine.Toppings[i].Portion > 0)
                    {
                        SideToppings.Add(new SelectedSide(TheOrderLine.Toppings[i].ID, TheOrderLine.Toppings[i].Portion, TheOrderLine.Toppings[i].Name));
                        break;
                    }
                    else if (TheOrderLine.Toppings[i].Sides[k].SideID == SideIndex && TheOrderLine.Toppings[i].Sides[k].Portion > 0)
                    {
                        SideToppings.Add(new SelectedSide(TheOrderLine.Toppings[i].ID, TheOrderLine.Toppings[i].Sides[k].Portion, TheOrderLine.Toppings[i].Name));
                        break;
                    }
                }
            }
            return SideToppings;
        }
        public static ResultSelectTopping SelectTopping(string ToppingIndex, bool IsAdd)
        {
            ResultSelectTopping result = new ResultSelectTopping();
            if (CUSTOM_ORDERLINE == null)
            {
                result.Message = "unsuccess";
                return result;
            }
            OrderLine DeOrderLine = CUSTOM_ORDERLINE;

            if (DeOrderLine.Guest >= Int32.Parse(AppSettings.MaxCanModifyToppings))
            {
                result.Message = "cannot";
                return result;
            }

            WPProduct DeProduct = CUSTOM_PRODUCT;
            Store DeStore = Store;

            //SetProductAndOption2OrderLine(DeStore, DeOrderLine, DeProduct, DeOrderLine.Qty);

            if (DeProduct.isHalfandHalf)
            {
                // Set Half Product on Side
                for (int i = 0; i <= DeOrderLine.DeMixedOption.DeSideDef.Number; i++)
                {
                    SetProduct4HalfNHalf(i, DeOrderLine, DeProduct, DeStore, false);
                }
            }

            int nPortion = 0;
            ItemOnProduct TheTopping = DeOrderLine.Toppings.FirstOrDefault(top => top.ID == ToppingIndex);
            if (ItemOnProduct.IsNUll(TheTopping))
            {

                if (DeOrderLine.Guest >= Int32.Parse(AppSettings.MaxCanModifyToppings))
                {
                    result.Message = "cannot";
                    return result;
                }
                //OrderLine.AddToSide(new ItemOnProduct(DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == ToppingIndex)), 1, GetSelectedSide(), DeOrderLine);
                //OrderLine.AddToSide(new ItemOnProduct(DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == ToppingIndex)), nPortion, GetSelectedSide(), DeOrderLine);
                var Price = OrderLine.UpdateToppings(new ItemOnProduct(DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == ToppingIndex)), 1, GetSelectedSide(), DeOrderLine);
                nPortion = 1;
            }
            else
            {
                int nSelectedSideIndex = GetSelectedSide();
                if (nSelectedSideIndex == 0)
                {
                    nPortion = DeOrderLine.Toppings.FirstOrDefault(top => top.ID == ToppingIndex).Portion;
                }
                else
                {
                    if (ToppingSide.IsNUll(DeOrderLine.Toppings.FirstOrDefault(top => top.ID == ToppingIndex).Sides.FirstOrDefault(sid => sid.SideID == nSelectedSideIndex)))
                    {
                        nPortion = 0;
                    }
                    else
                    {
                        nPortion = DeOrderLine.Toppings.FirstOrDefault(top => top.ID == ToppingIndex).Sides.FirstOrDefault(sid => sid.SideID == nSelectedSideIndex).Portion;
                    }
                }

                if (IsAdd)
                {
                    nPortion++;
                }
                else
                {
                    nPortion--;

                }

                if (nPortion > Int32.Parse(AppSettings.MaxCanModifyToppings))
                {
                    result.Message = "max";
                    return result;
                }
                //OrderLine.AddToSide(new ItemOnProduct(DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == ToppingIndex)), nPortion, GetSelectedSide(), DeOrderLine);
                var Price = OrderLine.UpdateToppings(new ItemOnProduct(DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == ToppingIndex)), nPortion, GetSelectedSide(), DeOrderLine);
            }
            //  OrderLine.Update(DeOrderLine);

            DeOrderLine.Guest++;
            CUSTOM_ORDERLINE = DeOrderLine;

            result.Price = DeOrderLine.Price.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
            result.Portion = nPortion.ToString();

            return result;
        }
        private static void SetProduct4HalfNHalf(int SideIndex, OrderLine DeOrderLine, WPProduct DeProduct, Store DeStore, bool IsCompareProduct = true)
        {
            if (!HALF_PRODUCT.ContainsKey(GetSessionKeyFromSideIndex(SideIndex)))
            {
                return;
            }
            WPProduct SideProduct = HALF_PRODUCT[GetSessionKeyFromSideIndex(SideIndex)];

            WPProduct OriginalProduct = (WPProduct)DeStore.DeCats.FirstOrDefault(cat => cat.ID == SideProduct.CatID).DeGroup.FirstOrDefault(grp => grp.ID == SideProduct.GrpID).DeProducts.FirstOrDefault(prd => prd.ID == SideProduct.ID);
            if (OriginalProduct != null)
            {
                DeOrderLine.AddProHnHSides(SideIndex, OriginalProduct);
                // Romove toppings of left side
                if (IsCompareProduct && GetSelectedSide() == SideIndex && OriginalProduct.ID != DeProduct.ID)
                {
                    List<ItemOnProduct> OriginalToppings = new List<ItemOnProduct>();
                    foreach (ItemOnProduct item in DeOrderLine.Toppings)
                    {
                        ToppingSide DeSide = item.Sides.FirstOrDefault(sid => sid.SideID == SideIndex);
                        if (!ToppingSide.IsNUll(DeSide))
                        {
                            OriginalToppings.Add(item);
                        }
                    }
                    foreach (ItemOnProduct item in OriginalToppings)
                    {
                        OrderLine.RemoveTopOfSide(item, SideIndex, DeOrderLine);
                    }
                }
            }
        }
        private static int GetSelectedSide()
        {
            int SelectedIndex = (int)CUSTOM_SIDE_INDEX;
            if (CUSTOM_ORDERLINE.IsThisHnH())
                return SelectedIndex;
            else
                return SelectedIndex == -1 ? 0 : SelectedIndex;
        }
        public static ResultAddToBasket AddToBasket(string CatID, string GroupID, string ProductID, int Qty)
        {
            ResultAddToBasket result = new ResultAddToBasket();
            if (BASKET == null)
            {
                return result;
            }

            // Find product from CatID and GroupID and ProductID
            WPBaseProduct DeProduct = Store.DeCats.FirstOrDefault(cat => cat.ID == CatID).DeGroup.FirstOrDefault(grp => grp.ID == GroupID).DeProducts.FirstOrDefault(prd => prd.ID == ProductID);
            if (DeProduct == null)
            {
                return result;
            }

            // Create OrderLine from product
            WinPizzaEnums.OrderType DeOrderType = ORDER_TYPE == "DELIVERY" ? WinPizzaEnums.OrderType.DELIVERY : WinPizzaEnums.OrderType.COLLECTION;
            var VatRate = Store.CalcVatRate(((WPBaseProduct)DeProduct).VAT, DeOrderType.ToString());

            OrderLine DeOrderLine = new OrderLine(DeProduct, DeProduct.DePrintingGrp, VatRate, 0, AppSettings.MenuID);
            //new OrderLine will do The setProduct
            // DeOrderLine.SetProduct(DeProduct, Qty);
            // DeOrderLine.Min4DLV = (DeProduct as WPProduct).Min4DLV;
            OrderLine.GetOptions(DeOrderLine);

            if (!DeProduct.IsSingelPrice() && DeProduct.CanHvItem == true)
            {
                foreach (ItemOnProduct TheTopping in DeOrderLine.Toppings)
                {
                    TheTopping.DeProduct = Store.ToppingGrp.FirstOrDefault(grp => grp.ID == (DeProduct as WPProduct).ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == TheTopping.ID);
                }
                OrderLine.Update(DeOrderLine);
            }

            BASKET.AddLine(DeOrderLine);

            result.Message = "success";
            result.TotalItemCount = BASKET.NumOfItem;
            result.TotalPrice = BASKET.DeOrderHeader.Total.ToString() + " " + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
            return result;
        }
        public static string AddToOrder(int Qty)
        {
            Basket DeBasket = BASKET;
            OrderLine DeOrderLine = CUSTOM_ORDERLINE;
            WPProduct DeProduct = CUSTOM_PRODUCT;
            Store DeStore = Store;

            if (DeProduct.isHalfandHalf && DeOrderLine.Sides.Count < DeOrderLine.DeMixedOption.DeSideDef.Number)
            {
                return "unpicked";
            }

            string strResult = "menu";
            if (!string.IsNullOrEmpty(DEAL_SELECTED_OFFERID))
            {
                string OfferID = DEAL_SELECTED_OFFERID;
                DEAL_SELECTED_OFFERID = null;
                OfferOrderLine TheOfferOrderLine = DEAL_ORDERLINE;
                if (!OfferOrderLine.IsNUll(TheOfferOrderLine))
                {
                    OrderLine OrderLineObject = TheOfferOrderLine.DeOrderLines[Int32.Parse(OfferID) - 1];

                    MixedOption TempMixedOption = DeOrderLine.DeMixedOption;
                    List<ItemOnProduct> TempToppings = DeOrderLine.Toppings;

                    OrderLineObject.SetProduct(DeProduct);
                    OrderLineObject.DeMixedOption = TempMixedOption;
                    OrderLineObject.Toppings = TempToppings;
                    foreach (ItemOnProduct TheTopping in OrderLineObject.Toppings)
                    {
                        TheTopping.DeProduct = DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == DeProduct.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == TheTopping.ID);
                    }

                    OrderLineObject.UpDatePrice();
                    OrderLine.Update(OrderLineObject);
                    OrderLineObject.OfferPrice = (decimal)OfferOrderLine.GetOfferItemPrice(DeStore, OrderLineObject);
                    TheOfferOrderLine.CalcDealPrice();
                    DEAL_ORDERLINE = TheOfferOrderLine;
                    strResult = TheOfferOrderLine.ID;
                }
            }
            else if (BASKET_SELECTED_ORDERLINEID > 0)
            {
                int OrderLineID = (int)(BASKET_SELECTED_ORDERLINEID);
                BASKET_SELECTED_ORDERLINEID = null;

                // Get current OrderLine
                // Changed by Hans 20200501 for fixing index error
                //OrderLine PreviousOrderLine = DeBasket.DeOrderLines[OrderLineID]; 
                OrderLine PreviousOrderLine = DeBasket.DeOrderLines.FirstOrDefault(ordLine => ordLine.ID == OrderLineID.ToString());
                DeBasket.DeleteLineByPos(OrderLineID);
                //DeBasket.DeleteLine(PreviousOrderLine);
                DeBasket.AddLine(DeOrderLine);

                strResult = "basket";
            }
            else
            {
                SetProductAndOption2OrderLine(DeStore, DeOrderLine, DeProduct, Qty);
                if (DeProduct.isHalfandHalf)
                {
                    // Set Half Product on Side
                    for (int i = 0; i <= DeOrderLine.DeMixedOption.DeSideDef.Number; i++)
                    {
                        SetProduct4HalfNHalf(i, DeOrderLine, DeProduct, DeStore, false);
                    }
                }

                DeOrderLine.UpDatePrice();
                DeBasket.AddLine(DeOrderLine);
            }

            BASKET = DeBasket;
            CUSTOM_ORDERLINE = null;
            CUSTOM_SIDE_INDEX = null;
            CUSTOM_PRODUCT = null;
            return strResult;
        }
        private static void SetProductAndOption2OrderLine(Store store, OrderLine orderLine, WPProduct product, int Qty = 1)
        {
            // Save MixedOption and Toppings in temp
            MixedOption TempMixedOption = orderLine.DeMixedOption;
            List<ItemOnProduct> TempToppings = orderLine.Toppings;

            // Set Product and MixedOption and Toppings
            orderLine.SetProduct(product, Qty);
            orderLine.DeMixedOption = TempMixedOption;
            orderLine.Toppings = TempToppings;
            foreach (ItemOnProduct TheTopping in orderLine.Toppings)
            {
                TheTopping.DeProduct = store.ToppingGrp.FirstOrDefault(grp => grp.ID == product.ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == TheTopping.ID);
            }
        }
        public static ResultSelectHalfNHalf ChooseProduct4Half(string CatID, string GroupID, string ProductID)
        {
            ResultSelectHalfNHalf result = new ResultSelectHalfNHalf();

            // If session has no StoreID, rediect to first page
            if (BASKET == null)
                return result;

            Store DeStore = Store;

            // Find product from CatID and GroupID and ProductID
            WPProduct DeProduct = (WPProduct)DeStore.DeCats.FirstOrDefault(cat => cat.ID == CatID).DeGroup.FirstOrDefault(grp => grp.ID == GroupID).DeProducts.FirstOrDefault(prd => prd.ID == ProductID);
            if (DeProduct == null)
            {
                return result;
            }

            WPProduct CustomizeProduct = CUSTOM_PRODUCT;
            OrderLine DeOrderLine = CUSTOM_ORDERLINE;
            //SetProductAndOption2OrderLine(DeStore, DeOrderLine, CustomizeProduct, DeOrderLine.Qty);

            int nSideIndex = GetSelectedSide();

            // Set Half Product on Side
            for (int i = 0; i <= DeOrderLine.DeMixedOption.DeSideDef.Number; i++)
            {
                SetProduct4HalfNHalf(i, DeOrderLine, DeProduct, DeStore);
            }

            bool bIsExistSide = false;
            foreach (ItemOnProduct item in DeOrderLine.Toppings)
            {
                ToppingSide DeSide = item.Sides.FirstOrDefault(sid => sid.SideID == nSideIndex);
                if (!ToppingSide.IsNUll(DeSide) && DeSide.Portion > 0)
                {
                    bIsExistSide = true;
                    break;
                }
            }
            result.Price = DeOrderLine.AddProHnHSides(nSideIndex, (WPProduct)DeStore.DeCats.FirstOrDefault(cat => cat.ID == DeProduct.CatID).DeGroup.FirstOrDefault(grp => grp.ID == DeProduct.GrpID).DeProducts.FirstOrDefault(prd => prd.ID == DeProduct.ID)).ToString();

            WPProduct SideProduct = null;
            if (HALF_PRODUCT.ContainsKey(GetSessionKeyFromSideIndex(nSideIndex)))
            {
                SideProduct = HALF_PRODUCT[GetSessionKeyFromSideIndex(nSideIndex)];
            }

            if (!bIsExistSide || (SideProduct != null && SideProduct.ID != DeProduct.ID))
            {
                // Get topping
                OrderLine TempOrderLine = new OrderLine(DeProduct);
                List<ItemOnProduct> lstToppings = TempOrderLine.Toppings;
                foreach (ItemOnProduct Topping in lstToppings)
                {
                    OrderLine.AddToSide(Topping, 1, nSideIndex, DeOrderLine);
                }
            }
            OrderLine.Update(DeOrderLine);

            // Get topping list to show
            List<SelectedSide> SideToppings = new List<SelectedSide>();
            for (int i = 0; i < DeOrderLine.Toppings.Count; i++)
            {
                for (int k = 0; k < DeOrderLine.Toppings[i].Sides.Count; k++)
                {
                    if (nSideIndex == 0 && DeOrderLine.Toppings[i].Sides.Count == DeOrderLine.DeMixedOption.DeSideDef.Number && DeOrderLine.Toppings[i].Portion > 0)
                    {
                        SideToppings.Add(new SelectedSide(DeOrderLine.Toppings[i].ID, DeOrderLine.Toppings[i].Portion, DeOrderLine.Toppings[i].Name));
                        break;
                    }
                    else if (DeOrderLine.Toppings[i].Sides[k].SideID == nSideIndex && DeOrderLine.Toppings[i].Sides[k].Portion > 0)
                    {
                        SideToppings.Add(new SelectedSide(DeOrderLine.Toppings[i].ID, DeOrderLine.Toppings[i].Sides.FirstOrDefault(sid => sid.SideID == nSideIndex).Portion, DeOrderLine.Toppings[i].Name));
                        break;
                    }
                }
            }

            // Save in session OrderLine and HalfNHalf product info
            CUSTOM_ORDERLINE = DeOrderLine;
            if (HALF_PRODUCT.ContainsKey(GetSessionKeyFromSideIndex(nSideIndex)))
            {
                HALF_PRODUCT[GetSessionKeyFromSideIndex(nSideIndex)] = DeProduct;
            }
            else
            {
                HALF_PRODUCT.Add(GetSessionKeyFromSideIndex(nSideIndex), DeProduct);
            }

            result.ToppingList = SideToppings;
            return result;
        }
        public static string GetSessionKeyFromSideIndex(int SideIndex)
        {
            string strKey = string.Empty;
            switch (SideIndex)
            {
                case 0:
                    strKey = HALF_PRODUCT_0;
                    break;
                case 1:
                    strKey = HALF_PRODUCT_1;
                    break;
                case 2:
                    strKey = HALF_PRODUCT_2;
                    break;
                case 3:
                    strKey = HALF_PRODUCT_3;
                    break;
            }
            return strKey;
        }

        public static async Task<DealsModel> Deals()
        {
            // If session has no StoreID, rediect to first page
            if (BASKET == null)
                return null;

            Store DeStore = Store;
            if (DeStore == null)
            {
                return null;
            }

            DealsModel DeModel = new DealsModel();

            FoodStoreProfile DeProfile = StoreProfile;
            ServersUrl DeServersUrl = SeversUrl;
            DeModel.IsStoreClosed = await CoreServices.GetStoreClosed(DeProfile.DeDataSourceName, DeServersUrl, DeStore);

            // Get deals category
            foreach (Category cat in DeStore.DeCats)
            {
                if (cat.DisplyAble == true && cat.CatType == WPUtility.WinPizzaEnums.ItemType.OFFER)
                {
                    DeModel.Products = new List<WPMealDeal>();
                    foreach (WinPizzaData.Group group in cat.DeGroup)
                    {
                        foreach (WPBaseProduct product in group.DeProducts)
                        {
                            DeModel.Products.Add((WPMealDeal)product);
                        }
                    }
                    break;
                }
            }

            if (DeModel.Products != null)
            {
                DeModel.Products = DeModel.Products.OrderBy(prd => prd.Feeds).ToList();
            }

            List<OpeningDate> roles = DeStore.OnlineRoles;
            List<string> TimeList = new List<string>();
            foreach (OpeningDate date in roles)
            {
                string StartTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeStartHh, date.DeOpeningTimes[0].DeStartMin);
                string EndTime = string.Format("{0}:{1}", date.DeOpeningTimes[1].DeStartHh + date.DeOpeningTimes[1].DeEndtHh, date.DeOpeningTimes[1].DeStartMin + date.DeOpeningTimes[1].DeEndMin);
                TimeList.Add(string.Format("{0} - {1}", StartTime, EndTime));
            }

            DeModel.RoleList = TimeList;

            return DeModel;
        }

        public static DealModel Deal(string id, string OrderLineID)
        {
            if (BASKET == null)
                return null;

            Store DeStore = Store;
            if (DeStore == null)
            {
                return null;
            }

            DealModel DeModel = new DealModel();

            Basket DeBasket = BASKET;
            OrderLine BasketOrderLine = DeBasket.DeOrderLines.FirstOrDefault(ordline => ordline.ID == OrderLineID);

            if (!OrderLine.IsNUll(BasketOrderLine))
            {
                DeModel.OfferOrderLine = BasketOrderLine as OfferOrderLine;
                DeModel.OfferOrderLine.DeMealDeal = (WPMealDeal)GetOfferProductFromID(id);
            }
            else
            {
                // Get deal from session or create orderline
                WinPizzaEnums.OrderType DeOrderType = ORDER_TYPE == "DELIVERY" ? WinPizzaEnums.OrderType.DELIVERY : WinPizzaEnums.OrderType.COLLECTION;
                var Pro = GetOfferProductFromID(id);
                var VatRate = DeStore.CalcVatRate(Pro.VAT, DeOrderType.ToString());
                if (OfferOrderLine.IsNUll(DEAL_ORDERLINE))
                {
                    // Create OrderLine from product store
                    DeModel.OfferOrderLine = new OfferOrderLine(Pro, VatRate, 0, AppSettings.MenuID);
                    DeModel.OfferOrderLine.DeMealDeal = (WPMealDeal)GetOfferProductFromID(id);
                }
                else
                {
                    // Get product ID from session
                    string strSessionDealProductID = DEAL_PRODUCT_ID;
                    if (!string.IsNullOrEmpty(strSessionDealProductID))
                    {
                        if (strSessionDealProductID == id)
                        {
                            // Session product ID and selected product ID is same
                            DeModel.OfferOrderLine = DEAL_ORDERLINE;
                            DeModel.OfferOrderLine.DeMealDeal = (WPMealDeal)GetOfferProductFromID(id);
                        }
                        else
                        {
                            // not same
                            DeModel.OfferOrderLine = new OfferOrderLine(GetOfferProductFromID(id), VatRate, 0, AppSettings.MenuID);
                        }
                    }
                }
            }

            // Set product already set on offer order line
            foreach (OrderLine DeOrderLine in DeModel.OfferOrderLine.DeOrderLines)
            {
                if (!string.IsNullOrEmpty(DeOrderLine.DeOfferItem.ProductID))
                {
                    // Find product from CatID and GroupID and ProductID
                    WPBaseProduct DeProduct = (WPBaseProduct)DeStore.DeCats.FirstOrDefault(cat => cat.ID == DeOrderLine.DeOfferItem.CatID).DeGroup.FirstOrDefault(grp => grp.ID == DeOrderLine.DeOfferItem.GroupID).DeProducts.FirstOrDefault(prd => prd.ID == DeOrderLine.DeOfferItem.ProductID);
                    if (DeProduct == null)
                    {
                        continue;
                    }

                    DeOrderLine.SetProduct(DeProduct, DeProduct.DePrintingGrp, (decimal)Store.CalcVatValue((decimal)DeOrderLine.OfferPrice, (decimal)DeOrderLine.Vatrate));
                    DeOrderLine.UpDatePrice();
                    OrderLine.Update(DeOrderLine);
                    DeModel.OfferOrderLine.CalcDealPrice();
                }
            }

            DeModel.IsAllItemPicked = DeModel.OfferOrderLine.IsAllItemPicked();


            List<OpeningDate> roles = DeStore.OnlineRoles;
            List<string> TimeList = new List<string>();
            foreach (OpeningDate date in roles)
            {
                string StartTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeStartHh, date.DeOpeningTimes[0].DeStartMin);
                string EndTime = string.Format("{0}:{1}", date.DeOpeningTimes[1].DeStartHh + date.DeOpeningTimes[1].DeEndtHh, date.DeOpeningTimes[1].DeStartMin + date.DeOpeningTimes[1].DeEndMin);
                TimeList.Add(string.Format("{0} - {1}", StartTime, EndTime));
            }

            DeModel.RoleList = TimeList;

            // Set session
            DEAL_ORDERLINE = DeModel.OfferOrderLine;
            DEAL_PRODUCT_ID = id;

            //Changed by Hans 20200502 to fix double add order on Basket issue
            if (!string.IsNullOrEmpty(OrderLineID))
            {
                BASKET_SELECTED_ORDERLINEID = Int32.Parse(OrderLineID);
            }

            return DeModel;
        }
        public static ResultAddToOfferOrder AddToDeal(string CatID, string GroupID, string ProductID, string OfferIndex)
        {
            ResultAddToOfferOrder result = new ResultAddToOfferOrder("", "0.00", "unsuccess");
            if (DEAL_ORDERLINE == null)
            {
                result.Message = "null";
                return result;
            }

            OrderLine TheOrderLine = DEAL_ORDERLINE;
            Store store = Store;


            // Find product from CatID and GroupID and ProductID
            WPBaseProduct TheProduct = (WPBaseProduct)store.DeCats.FirstOrDefault(cat => cat.ID == CatID).DeGroup.FirstOrDefault(grp => grp.ID == GroupID).DeProducts.FirstOrDefault(prd => prd.ID == ProductID);
            if (TheProduct == null)
            {
                return result;
            }

            OrderLine OrderLineObject = (TheOrderLine as OfferOrderLine).DeOrderLines[Int32.Parse(OfferIndex) - 1];
            OrderLineObject.SetProduct(TheProduct, TheProduct.DePrintingGrp, (decimal)Store.CalcVatValue((decimal)OrderLineObject.OfferPrice, (decimal)OrderLineObject.Vatrate));
            OrderLineObject.UpDatePrice();
            OrderLine.Update(OrderLineObject);
            var DeOfferLine = (TheOrderLine as OfferOrderLine);
            DeOfferLine.CalcDealPrice();

            // Store basket in session
            DEAL_ORDERLINE = TheOrderLine as OfferOrderLine;

            if (DeOfferLine.IsAllItemPicked())
            {
                result.IsAllPicked = true;
            }
            result.Message = "success";
            result.ProductName = OrderLineObject.Name4Binding;
            result.TotalPrice = TheOrderLine.Price.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
            return result;
        }
        public static string AddDealToBasket()
        {
            OrderLine DeOrderLine = DEAL_ORDERLINE;
            Basket DeBasket = BASKET;

            //Changed by Hans 20200502 to fix double deal issue on Basket
            if (BASKET_SELECTED_ORDERLINEID > 0)
            {
                int OrderLineID = (int)(BASKET_SELECTED_ORDERLINEID);
                BASKET_SELECTED_ORDERLINEID = null;
                OrderLine PreviousOrderLine = DeBasket.DeOrderLines.FirstOrDefault(ordLine => ordLine.ID == OrderLineID.ToString());
                DeBasket.DeleteLineByPos(OrderLineID);
            }

            DeBasket.AddLine(DeOrderLine);
            BASKET = DeBasket;
            DEAL_ORDERLINE = null;
            CUSTOM_ORDERLINE = null;
            DEAL_PRODUCT_ID = null;
            return "success";
        }
        public static BasketModel Basket()
        {
            if (BASKET == null)
                return null;

            BasketModel DeModel = new BasketModel();
            Store DeStore = Store;
            if (DeStore == null)
            {
                return null;
            }

            // Get basket info from session
            DeModel.BasketInfo = BASKET;

            DeModel.Store = DeStore;
            if (DeModel.Store.DeDlvParam == null)
            {
                DeModel.Store.DeDlvParam = new DeliveryParameters();
                DeModel.Store.DeDlvParam.MinOrderValueInStore = 0;
            }


            List<OpeningDate> roles = DeStore.OnlineRoles;
            List<string> TimeList = new List<string>();
            foreach (OpeningDate date in roles)
            {
                string StartTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeStartHh, date.DeOpeningTimes[0].DeStartMin);
                string EndTime = string.Format("{0}:{1}", date.DeOpeningTimes[1].DeStartHh + date.DeOpeningTimes[1].DeEndtHh, date.DeOpeningTimes[1].DeStartMin + date.DeOpeningTimes[1].DeEndMin);
                TimeList.Add(string.Format("{0} - {1}", StartTime, EndTime));
            }

            DeModel.RoleList = TimeList;
            return DeModel;
        }
        public static ResultAddToBasket AddToBasketOnBasket(int OrderLineID, int IsAdd)
        {
            ResultAddToBasket result = new ResultAddToBasket();
            Store DeStore = Store;
            if (DeStore == null)
            {
                return result;
            }

            // Get basket info from session
            Basket DeBasket = BASKET;

            // Get current OrderLine
            //Changed by Hans 20200501 to fix index error
            //OrderLine DeOrderLine = DeBasket.DeOrderLines[OrderLineID];
            OrderLine DeOrderLine = DeBasket.DeOrderLines.FirstOrDefault(ordLine => ordLine.ID == OrderLineID.ToString());

            // Find product from CatID and GroupID and ProductID
            WPBaseProduct DeProduct = DeStore.DeCats.FirstOrDefault(cat => cat.ID == DeOrderLine.CatID).DeGroup.FirstOrDefault(grp => grp.ID == DeOrderLine.GrpID).DeProducts.FirstOrDefault(prd => prd.ID == DeOrderLine.ProID);
            if (DeProduct == null)
            {
                return result;
            }

            OrderLine TempOrderLine = new OrderLine(DeProduct);
            TempOrderLine.SetProduct(DeProduct, 1);
            OrderLine.GetOptions(TempOrderLine);

            if (!DeProduct.IsSingelPrice() && DeProduct.CanHvItem == true)
            {
                foreach (ItemOnProduct TheTopping in TempOrderLine.Toppings)
                {
                    TheTopping.DeProduct = DeStore.ToppingGrp.FirstOrDefault(grp => grp.ID == (DeProduct as WPProduct).ToppingGrpID).DeProducts.FirstOrDefault(prd => prd.ID == TheTopping.ID);
                }
                OrderLine.Update(TempOrderLine);
            }
            // Create OrderLine from product
            if (IsAdd == 1)
            {

                DeBasket.AddToLine(DeOrderLine, true);
            }
            else
            {
                // i know what is the problem in addtoline we using same memory location , but in your caes is different each time
                // changes let me correct tid back to u in 10 min correct the ddls
                // do you know what i mean?
                // we delete the object from orderlines based on oject location.
                // i can fix this in sec

                DeBasket.AddToLine(DeOrderLine, false);
            }

            // Store basket in session
            BASKET = DeBasket;
            result.Message = "success";
            result.TotalItemCount = DeBasket.NumOfItem;
            result.CurrentOrderLinePrice = DeOrderLine.Price.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
            result.TotalPrice = DeBasket.DeOrderHeader.Total.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;

            return result;
        }
        public static ResultAddToBasket DeleteProductOnBasket(int OrderLineID) //Changed hans param =OrderLinePos
        {
            ResultAddToBasket result = new ResultAddToBasket();
            Store DeStore = Store;
            if (DeStore == null)
            {
                return result;
            }

            // Get basket info from session
            Basket DeBasket = BASKET;

            // Get current OrderLine
            //Changed by Hans 20200501 to fix index error
            //OrderLine DeOrderLine = DeBasket.DeOrderLines[OrderLineID];
            OrderLine DeOrderLine = DeBasket.DeOrderLines.FirstOrDefault(ordLine => ordLine.ID == OrderLineID.ToString());

            DeBasket.DeleteLineByPos(OrderLineID);

            // Store basket in session
            BASKET = DeBasket;
            result.Message = "success";
            result.TotalItemCount = DeBasket.NumOfItem;
            result.TotalPrice = DeBasket.DeOrderHeader.Total.ToString() + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
            return result;
        }
        private static WPBaseProduct GetOfferProductFromID(string ProductID)
        {
            WPBaseProduct TheProduct = null;
            Store store = Store;
            Category catDeals = store.DeCats.FirstOrDefault(cat => cat.DisplyAble == true && cat.CatType == WPUtility.WinPizzaEnums.ItemType.OFFER);
            foreach (WinPizzaData.Group group in catDeals.DeGroup)
            {
                TheProduct = group.DeProducts.FirstOrDefault(prd => prd.ID == ProductID);
                if (TheProduct != null)
                {
                    break;
                }
            }
            return TheProduct;
        }

        public static async Task<CheckOutModel> CheckOut()
        {
            CheckOutModel DeModel = new CheckOutModel();
            if (string.IsNullOrEmpty(POSTCODE))
            {
                return null;
            }
            if (BASKET == null)
            {
                return null;
            }
            Store DeStore = Store;
            if (DeStore == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(DeModel.StoreID))
            {
                DeModel.StoreID = POSTCODE;
                if (!string.IsNullOrEmpty(DeModel.StoreID))
                {
                    FoodStoreProfile DeStoreProfile = StoreProfile;
                    ServersUrl DeServersUrl = SeversUrl;
                    DeModel.AddressList = await CoreServices.GetAddressFromPostcode(DeModel.StoreID, DeStoreProfile, AppSettings.CountryCode, DeServersUrl);
                }
                DeModel.OrderType = ORDER_TYPE;

                DeModel.AddressFormat = AppSettings.CountryCode; ;
                ADDRESSLIST = DeModel.AddressList;
            }
            List<OpeningDate> roles = DeStore.OnlineRoles;
            List<string> TimeList = new List<string>();
            foreach (OpeningDate date in roles)
            {
                string StartTime = string.Format("{0}:{1}", date.DeOpeningTimes[0].DeStartHh, date.DeOpeningTimes[0].DeStartMin);
                string EndTime = string.Format("{0}:{1}", date.DeOpeningTimes[1].DeStartHh + date.DeOpeningTimes[1].DeEndtHh, date.DeOpeningTimes[1].DeStartMin + date.DeOpeningTimes[1].DeEndMin);
                TimeList.Add(string.Format("{0} - {1}", StartTime, EndTime));
            }

            DeModel.RoleList = TimeList;
            return DeModel;
        }

        public static async Task<string> PlaceOrderAndPay(MyDetailPageModel model)
        {
            // If basket is null, redirect to first page
            if (BASKET == null)
                return "basket is null";

            // If session has no Store, rediect to first page
            Store DeStore = Store;
            if (DeStore == null)
            {
                return "session has no Store";
            }

            // Check user phone number validate
            //   model.ContactNumber = DeStore.DePhoneNumber.ConvertToDialableNumber(model.ContactNumber);
            if (!WinPizzaData.PhoneNumber.IsMobile(model.Contact_Number, DeStore.DePhoneNumber))
            {
                return "Contact number is not valid.|Please enter correct contact number.";
                //return RedirectToAction("CheckOut", "Home", model);
            }

            // Set addresss on va
            List<WinPizzaData.Address> DeAddressList = ADDRESSLIST;
            ServersUrl DeServersUrl = SeversUrl;
            Basket basket = BASKET;
            basket.DeOrderHeader.DePeople.Name = model.First_Name;
            //basket.DeOrderHeader.DePeople.Email = model.EmailAddress;
            basket.DeOrderHeader.DePeople.Phone = model.Contact_Number;
            basket.DeOrderHeader.DePeople.DeAddress = DeAddressList[model.SelectedAddressIndex];

            BASKET = basket;

            PaymentMethod = model.PaymentMethod;

            string PhoneNumber = AppSettings.CountryPhoneCode + model.Contact_Number;

            if (model.PaymentMethod.Equals(WinPizzaEnums.PaymentType.CASH.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                bool IsUserVerified = await CoreServices.IsUserVerified(PhoneNumber, "DEVDATA", DeServersUrl);
                if (!IsUserVerified)
                {
                    return "go to SetVerifyMode";
                }
            }

            // If user is verified 
            if (model.PaymentMethod.ToString().Equals(WinPizzaEnums.PaymentType.CASH.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // If payment method is cash, submit order to store
                WinPizzaEnums.OrderType DeOrderType = ORDER_TYPE == "DELIVERY" ? WinPizzaEnums.OrderType.DELIVERY : WinPizzaEnums.OrderType.COLLECTION;
                FoodStoreProfile DeStoreProfile = StoreProfile;
                WPMessage DeMessage = await CoreServices.SubmitOrder(DeStoreProfile.StoreID, DeServersUrl, BASKET, DeStoreProfile.DeDataSourceName, DeOrderType, POSTCODE, DeAddressList[model.SelectedAddressIndex], AppSettings.CountryCode);
                if (DeMessage.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
                {
                    OrderNumber = DeMessage.DeMsgBody;

                    return "go to Thanks";
                }
                return "Error occurs while submit order to store.|Please try again to submit.";
            }
            else
            {
                // If payment method is card, redirect to CardPayment page
                return "card";
            }
        }

        public static MenuModel MenuModel { get; set; }
        public static CustomizeModel CustomizeModel { get; set; }
        public static Store Store { get; set; }
        public static FoodStoreProfile StoreProfile { get; set; }
        public static ServersUrl SeversUrl { get; set; }


        public static Dictionary<string, string> SelectedProduct = new Dictionary<string, string>();

        public static Dictionary<string, WPProduct> HALF_PRODUCT = new Dictionary<string, WPProduct>();

        //public static Dictionary<string, WPBaseProduct> DealsProduct = new Dictionary<string, WPBaseProduct>();

        public static string HalfNHalf_Price = string.Empty;

        private static int _HalfNHalf_SideNumber;
        public static int HalfNHalf_SideNumber
        {
            get
            {
                return _HalfNHalf_SideNumber;
            }
            set
            {
                if (value == 2 || value == 4)
                {
                    _HalfNHalf_SideNumber = value;
                }
            }
        }

        public static string POSTCODE { get; set; }

        public static int? IsStart { get; set; }

        public static string DEALS { get; set; }

        public static string DEALS_LUNCH { get; set; }

        public static Basket BASKET { get; set; }

        public static WPProduct CUSTOM_PRODUCT { get; set; }

        public static int? CUSTOM_SIDE_INDEX { get; set; }

        public static OrderLine CUSTOM_ORDERLINE { get; set; }
        public static OrderLine TEMP_ORDERLINE { get; set; }


        public static string HALF_PRODUCT_0 = "Half_Product_0";
        public static string HALF_PRODUCT_1 = "Half_Product_1";
        public static string HALF_PRODUCT_2 = "Half_Product_2";
        public static string HALF_PRODUCT_3 = "Half_Product_3";

        public static OfferOrderLine DEAL_ORDERLINE { get; set; }

        public static string DEAL_SELECTED_OFFERID { get; set; }

        public static string DEAL_PRODUCT_ID { get; set; }

        public static int? BASKET_SELECTED_ORDERLINEID { get; set; }

        public static string ORDER_TYPE { get; set; }

        public static List<WinPizzaData.Address> ADDRESSLIST { get; set; }

        public static string UserPhoneNumber { get; set; }

        public static string UserVerifyCode { get; set; }

        public static string OrderNumber { get; set; }

        public static string PaymentMethod { get; set; }
    }
}
