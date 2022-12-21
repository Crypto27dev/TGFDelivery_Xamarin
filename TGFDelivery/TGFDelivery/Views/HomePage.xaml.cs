using Xamarin.Forms;

namespace TGFDelivery.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {

                 Navigation.PushAsync(new CategoryListPage());
            };
            //((Image)this.FindByName("Menue")).GestureRecognizers.Add(tapGestureRecognizer);
            Menue.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
