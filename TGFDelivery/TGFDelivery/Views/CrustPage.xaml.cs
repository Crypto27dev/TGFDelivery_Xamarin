using System;
using System.Linq;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.Data;
using TGFDelivery.Models.ViewCellModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrustPage : ContentPage
    {
        public CrustPage()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            foreach (WinPizzaData.Option crust in DataManager.CustomizeModel.GroupedLinq[DataManager.CustomizeModel.ListGroupKey[1]])
            {
                if (DataManager.CustomizeModel.TheOrderLine.DeMixedOption.OptionList.FirstOrDefault(o => o.Value.Name == crust.Name).Value != null)
                {
                    CrustViewCellModel crustViewCellModel = new CrustViewCellModel(crust.Name, DataManager.SelectCrust2CrustPage(crust.Name), "#4a4b5d");
                    CrustViewCell crustViewCell = new CrustViewCell(crustViewCellModel) { BindingContext = crustViewCellModel };
                    crustViewCell.Tapped += CrustViewCell_Tapped;
                    xName_List.Add(crustViewCell);
                }
                else
                {
                    CrustViewCellModel crustViewCellModel = new CrustViewCellModel(crust.Name, DataManager.SelectCrust2CrustPage(crust.Name), "");
                    CrustViewCell crustViewCell = new CrustViewCell(crustViewCellModel) { BindingContext = crustViewCellModel };
                    crustViewCell.Tapped += CrustViewCell_Tapped;
                    xName_List.Add(crustViewCell);
                }
            }
        }
        private async void CrustViewCell_Tapped(object sender, EventArgs e)
        {
            App.Loading(this);
            ViewCell vc = (ViewCell)sender;
            var selectedCrust = (CrustViewCellModel)vc.BindingContext;

            var Price = await Task.Run(() => proc_Crust(selectedCrust));
            if (DataManager.CustomizeModel.Product.isHalfandHalf)
            {
                DataManager.HalfNHalf_Price = Price;
            }
            await App._NavigationPage.PopAsync();
            App.Stop(this);
        }
        private string proc_Crust(CrustViewCellModel selectedCrust)
        {
            return DataManager.SelectCrust(selectedCrust.Size);
        }
    }
}