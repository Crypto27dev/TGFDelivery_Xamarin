using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPageMaster : ContentPage
    {
        public ListView ListView;

        public MenuPageMaster()
        {
            InitializeComponent();

            BindingContext = new MenuPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MenuPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuPageMasterMenuItem> MenuItems { get; set; }

            public MenuPageMasterViewModel()
            {
                //MenuItems = new ObservableCollection<MenuPageMasterMenuItem>(new[]
                //{
                //    new MenuPageMasterMenuItem { Id = 0, Title = "Page 1" },
                //    new MenuPageMasterMenuItem { Id = 1, Title = "Page 2" },
                //    new MenuPageMasterMenuItem { Id = 2, Title = "Page 3" },
                //    new MenuPageMasterMenuItem { Id = 3, Title = "Page 4" },
                //    new MenuPageMasterMenuItem { Id = 4, Title = "Page 5" },
                //});
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void MenuItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}