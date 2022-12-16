using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TGFDelivery.Helpers;
using Xamarin.Forms;

namespace TGFDelivery.Models.PageModel
{
    public class GroupListPageModel : ViewModelBase
    {
        private ObservableCollection<GroupModel> _GroupList { get; set; }
        public ObservableCollection<GroupModel> GroupList
        {
            get => _GroupList;
            set
            {
                _GroupList = value;

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

        private GroupModel _myGroupSelected { get; set; }
        public GroupModel MyGroupSelected
        {
            get => _myGroupSelected;
            set
            {
                _myGroupSelected = value;
                OnPropertyChanged();
            }
        }

        public ICommand OnGroupClickedCommand { get; set; }
        public event EventHandler OnLoadProducts;

        public GroupListPageModel()
        {
            GroupList = new ObservableRangeCollection<GroupModel>();
            OnGroupClickedCommand = new Command(() => OnLoadProductClicked());

            /*           MessagingCenter.Subscribe<CategoryGroupMsg>(this, "loadGroups", args =>
                       {
                           LoadMyCategoryGroups(args.Value);
                       });*/

        }

        /*  public void LoadMyCategoryGroups(Category cat)
          {
              GroupList.Clear();
              GroupList = new ObservableCollection<GroupModel>(cat.DeGroup);
              category_name = cat.Name;
              foreach (var group in GroupList)
              {
                  if (!group.ImgUrl.Contains("http"))
                      group.ImgUrl = StoreDataSource.DeStoreProfile.DeStoreLinks.Photo + group.ImgUrl;
              }
          }*/

        private void OnLoadProductClicked()
        {
            if (MyGroupSelected == null) return;
            /*OnLoadProducts?.Invoke(this, EventArgs.Empty);
            MessagingCenter.Send<GroupProductMsg>(new GroupProductMsg()
            {
                Value = MyGroupSelected
            }, "loadProducts");*/
        }
    }
}
