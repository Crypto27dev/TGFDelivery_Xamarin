using System;
using TGFDelivery.Data;
using TGFDelivery.Helpers.Data;
using TGFDelivery.Models.ViewCellModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.CustomViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideViewCell : ViewCell
    {
        public event EventHandler<string> ButtonClicked;

        private SideViewCellModel _Model;
        public SideViewCell(SideViewCellModel Model)
        {
            InitializeComponent();
            _Model = Model;
            xName_Layout.BackgroundColor = Color.FromHex(Model.BackColor);
            xName_Name.Text = Model.Topping.Name;
            if (Model.Portion != 0)
            {
                xName_BtnClose.IsVisible = true;
                xName_Portion.Text = Model.Portion.ToString();
            }
        }

        private void onClose(object sender, EventArgs e)
        {
            ResultSelectTopping selectedTopping = DataManager.SelectTopping(_Model.Topping.ID, false);
            if (selectedTopping.Message == "unsuccess")
            {
                return;
            }
            else if (selectedTopping.Message == "cannot")
            {
                this.ButtonClicked?.Invoke(sender, "Error");
                return;
            }
            else if (selectedTopping.Message == "max")
            {
                return;
            }
            else
            {
                if (Int32.Parse(selectedTopping.Portion) == 0)
                {
                    xName_Layout.BackgroundColor = Color.FromHex("#2d2d2d");
                    xName_Portion.Text = "";
                    xName_BtnClose.IsVisible = false;
                }
                else
                {
                    xName_Portion.Text = selectedTopping.Portion;
                }

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ResultSelectTopping selectedTopping = DataManager.SelectTopping(_Model.Topping.ID, true);
            if (selectedTopping.Message == "unsuccess")
            {
                return;
            }
            else if (selectedTopping.Message == "cannot")
            {
                this.ButtonClicked?.Invoke(sender, "Error");
                return;
            }
            else if (selectedTopping.Message == "max")
            {
                return;
            }
            else
            {
                xName_BtnClose.IsVisible = true;
                xName_Portion.Text = selectedTopping.Portion;
                xName_Layout.BackgroundColor = Color.FromHex("#4a4b5d");
            }
        }
    }
}