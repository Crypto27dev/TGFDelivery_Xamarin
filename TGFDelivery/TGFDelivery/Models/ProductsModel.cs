using System.Collections.ObjectModel;
using TGFDelivery.Data;
using WinPizzaData;
namespace TGFDelivery.Models
{

    public class FoodItem : ViewModelBase
    {

        public FoodItem()
        {
            IsSelected = false;
        }
        bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; OnPropertyChanged(); OnPropertyChanged("Visible"); }
        }
    }

    public class ProductsModel : FoodItem
    {
        public ProductsModel()
        {
            IsSelected = false;
        }
        public ProductsModel(WPBaseProduct Item)
        {
            IsSelected = false;
            MyPro = Item;
            MyPro.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + MyPro.ImgUrl;
        }
        WPBaseProduct _MYPro;
        public WPBaseProduct MyPro
        {
            get { return _MYPro; }
            set { _MYPro = value; OnPropertyChanged(); }
        }
    }
    public class ToppingsModel : FoodItem
    {
        int _calorie;
        public int Calorie
        {
            get { return _calorie; }
            set { _calorie = value; OnPropertyChanged(); }
        }

        bool _isVegan;
        public bool IsVegan
        {
            get { return _isVegan; }
            set { _isVegan = value; OnPropertyChanged(); }
        }

        int _order;
        public int Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(); OnPropertyChanged("Visible"); }
        }

        public bool Visible
        {
            get
            {
                return !IsSelected && Order > 0;
            }
        }


        public ToppingsModel()
        {
            IsSelected = false;
        }
        public ToppingsModel(WPBaseProduct Item)
        {
            IsSelected = false;
            Order = 0;
            MyPro = Item;
            MyPro.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + MyPro.ImgUrl;
        }
        WPBaseProduct _MYPro;
        public WPBaseProduct MyPro
        {
            get { return _MYPro; }
            set { _MYPro = value; OnPropertyChanged(); }
        }
    }

    public class GroupModel : FoodItem
    {
        public GroupModel()
        {
            IsSelected = false;
        }
        public GroupModel(Group Item)
        {
            IsSelected = false;
            MyGr = Item;
            MyGr.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + MyGr.ImgUrl;
            if (Item.DeProducts.Count > 0)
            {
                Item.DeProducts.ForEach(
                    p => MyPros.Add(new ProductsModel(p)));
            }
        }
        Group _MYGr;
        public Group MyGr
        {
            get { return _MYGr; }
            set { _MYGr = value; OnPropertyChanged(); }
        }
        ObservableCollection<ProductsModel> _MyPros = new ObservableCollection<ProductsModel>();
        public ObservableCollection<ProductsModel> MyPros
        {
            get => _MyPros;
            set
            {
                _MyPros = value;
                OnPropertyChanged();
            }
        }
    }
    public class CategoryModel : FoodItem
    {
        public CategoryModel()
        {
            IsSelected = false;
        }
        public CategoryModel(Category Item)
        {
            IsSelected = false;
            MYCat = Item;
            MYCat.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + MYCat.ImgUrl;
            MyGrp = new ObservableCollection<GroupModel>();
            if (Item.DeGroup.Count > 0)
            {
                Item.DeGroup.ForEach(
                    p => MyGrp.Add(new GroupModel(p)));
            }
        }
        Category _MYCat;
        public Category MYCat
        {
            get { return _MYCat; }
            set { _MYCat = value; OnPropertyChanged(); }
        }
        ObservableCollection<GroupModel> _MyGrp;
        public ObservableCollection<GroupModel> MyGrp
        {
            get => _MyGrp;
            set
            {
                _MyGrp = value;
                OnPropertyChanged();
            }
        }

    }
}
