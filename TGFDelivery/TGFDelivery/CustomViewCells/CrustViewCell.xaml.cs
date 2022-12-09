using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.Models.ViewCellModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.CustomViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrustViewCell : ViewCell
    {
        public CrustViewCell(CrustViewCellModel Model)
        {
            InitializeComponent();
            xName_Size.Text = Model.Size;
            xName_Price.Text = Model.Price;
            xName_Layout.BackgroundColor = Color.FromHex(Model.BackColor);
        }
    }
}