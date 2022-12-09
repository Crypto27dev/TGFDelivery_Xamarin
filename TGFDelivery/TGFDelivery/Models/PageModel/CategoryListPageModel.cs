using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TGFDelivery.Data;
using TGFDelivery.Helpers;
using TGFDelivery.Models.ServiceModel;
using WinPizzaData;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace TGFDelivery.Models.PageModel
{

    public class CategoryListPageModel : ViewModelBase
    {
        private Boolean _hasMultipleGroup;
        public Boolean hasMultipleGroup
        {
            get => _hasMultipleGroup;
            set
            {
                _hasMultipleGroup = value;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<CategoryModel> _CategoryList;
        public ObservableRangeCollection<CategoryModel> CategoryList
        {
            get => _CategoryList;
            set
            {
                _CategoryList = value;

                OnPropertyChanged();
            }
        }

        public CategoryModel _myCategorySelected;
        public CategoryModel MyCategorySelected
        {
            get => _myCategorySelected;
            set 
            {
                _myCategorySelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<GroupModel> _GroupList;
        public ObservableRangeCollection<GroupModel> GroupList
        {
            get => _GroupList;
            set
            {
                _GroupList = value;

                OnPropertyChanged();
            }
        }

        private GroupModel _myGroupSelected;
        public GroupModel MyGroupSelected
        {
            get => _myGroupSelected;
            set
            {
                _myGroupSelected = value;
                OnPropertyChanged();
            }
        }

        // public ICommand OnCategoryClickedCommand { get; set; }
        public event EventHandler OnLoadGroups;

        public CategoryListPageModel()
        {
            CategoryList = new ObservableRangeCollection<CategoryModel>();
            CategoryList.Clear();
            var hh = StoreDataSource.DeStore.DeCats.Where(p => p.DisplyAble);
            hh.All(p => {
                CategoryList.Add(new CategoryModel(p));
                
                     return true;});
            //= new ObservableRangeCollection<CategoryModel>();
            // CategoryList.AddRange(DataManager.MenuModel.CategoryList);
            foreach (var cat in CategoryList)
            {
                if (!cat.MYCat.ImgUrl.Contains("http"))
                    cat.MYCat.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + cat.MYCat.ImgUrl;
            }
          //  OnCategoryClickedCommand = new Command(() => OnLoadGroupClicked());
        }

        /*public  void OnLoadGroupClicked()
        {
            if (MyCategorySelected == null) return;
            if(MyCategorySelected.DeGroup.Count != 1)
            {
                OnLoadGroups?.Invoke(this, EventArgs.Empty);
                MessagingCenter.Send<CategoryGroupMsg>(new CategoryGroupMsg()
                {
                    Value = MyCategorySelected
                }, "loadGroups");
            }
            else
            {
                OnLoadGroups?.Invoke(this, EventArgs.Empty);
                MessagingCenter.Send<GroupProductMsg>(new GroupProductMsg()
                {
                    Value = MyCategorySelected.DeGroup[0]
                }, "loadProducts");
            }
        }*/
    }
}
