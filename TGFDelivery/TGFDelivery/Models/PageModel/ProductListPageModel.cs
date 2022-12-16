using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using TGFDelivery.Data;
using TGFDelivery.Models.ServiceModel;
using TGFDelivery.Models.ViewCellModel;
using WinPizzaData;
using Xamarin.Forms;

namespace TGFDelivery.Models.PageModel
{

    public class ProductListPageModel : ViewModelBase
    {
        private ObservableCollection<ProductViewCellModel> _ProductList { get; set; }
        public ObservableCollection<ProductViewCellModel> ProductList
        {
            get => _ProductList;
            set
            {
                _ProductList = value;

                OnPropertyChanged();
            }
        }

        private string _category_name { get; set; }
        public string category_name
        {
            get => _category_name;
            set
            {
                _category_name = value;
                OnPropertyChanged();
            }
        }

        private string _group_name { get; set; }
        public string group_name
        {
            get => _group_name;
            set
            {
                _group_name = value;
                OnPropertyChanged();
            }
        }

        public ProductListPageModel()
        {
            ProductList = new ObservableCollection<ProductViewCellModel>();

            MessagingCenter.Subscribe<GroupProductMsg>(this, "loadProducts", args =>
            {
                LoadMyGroupProducts(args.Value);
            });
        }

        public void LoadMyGroupProducts(Group group)
        {
            ProductList.Clear();
            if (group.DeCat.DeGroup.Count == 1)
            {
                group_name = group.Name;
            }
            else
            {
                category_name = group.DeCat.Name + "/";
                group_name = group.Name;
            }

            foreach (var product in group.DeProducts)
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
                    productViewCellModel.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + product.ImgUrl;

                    //productViewCellModel.wPBaseProduct = product;
                    productViewCellModel.CatID = product.CatID;
                    productViewCellModel.GroupID = product.GrpID;
                    productViewCellModel.ProductID = product.ID;
                    //productViewCellModel.AddBtn_Clicked += ProductViewCellModel_AddBtn_Clicked;
                    if (!product.IsSingelPrice() && product.CanHvItem == true && !StoreDataSource.IsStoreClosed)
                    {
                        productViewCellModel.Price = product.DeGroupedPrices.DePrices.FirstOrDefault().DeMixOption.Name.Replace(",", " ") + product.DeGroupedPrices.DePrices.FirstOrDefault().Amount.ToString();

                        //ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                        //productViewCell.Tapped += ProductViewCell_Tapped;
                        ProductList.Add(productViewCellModel);
                    }
                    else if (!DataManager.MenuModel.IsStoreClosed)
                    {
                        productViewCellModel.Price = product.DePrice.ToString((CultureInfo)CultureInfo.CurrentCulture) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
                        //ViewCell productViewCell = new ProductViewCell() { BindingContext = productViewCellModel };
                        ProductList.Add(productViewCellModel);
                    }
                }
            }
        }
    }
}
