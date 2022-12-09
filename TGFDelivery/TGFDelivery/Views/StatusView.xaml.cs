using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusView : ContentView
    {
        public StatusView()
        {
            InitializeComponent();
            Initialize();
        }
        #region "Helpers"
        public void Show()
        {
            this.IsVisible = true;
            status.IsEnabled = true;
            status.IsRunning = true;
        }

        public void Hide()
        {
            this.IsVisible = false;
            status.IsEnabled = false;
            status.IsRunning = false;
        }
        #endregion

        #region "Message Center"
        public void Initialize()
        {
            MessagingCenter.Subscribe<object>(this, AppSettings.STATUS_LOADING, (sender) => {
                Show();
            });

            MessagingCenter.Subscribe<object>(this, AppSettings.STATUS_DONE, (sender) => {
                Hide();
            });
        }
        #endregion
    }
}