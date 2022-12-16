using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertDialogPopup
    {
        private Func<bool, Task> callback;

        public AlertDialogPopup(string title, string message, string cancel, string ok, Func<bool, Task> callback)
        {
            this.callback = callback;

            InitializeComponent();
            LbTitle.Text = title;
            LbMessage.Text = message;
            BtCancel.IsVisible = !string.IsNullOrWhiteSpace(cancel);
            BtOk.Text = ok;
            BtCancel.Text = cancel;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // FrContent.HeightRequest = LbMessage.Height + 60 + FrContent.Padding.HorizontalThickness + AlertContent.Margin.HorizontalThickness;
            FrContent.Opacity = 1;
        }

        private async void BtCancel_Clicked(object sender, EventArgs e)
        {
            await callback.Invoke(false);
        }

        private async void BtOk_Clicked(object sender, EventArgs e)
        {
            await callback.Invoke(true);
        }

        private async void BtClose_Clicked(object sender, EventArgs e)
        {
            await callback.Invoke(false);
        }
    }
}