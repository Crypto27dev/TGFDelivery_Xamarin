using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.CustomViewCells;
using TGFDelivery.Data;
using TGFDelivery.Models.ViewCellModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SizePage : ContentPage
    {
        public SizePage()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            foreach (WinPizzaData.Option size in DataManager.CustomizeModel.GroupedLinq[DataManager.CustomizeModel.ListGroupKey[0]])
            {
                if (DataManager.CustomizeModel.TheOrderLine.DeMixedOption.OptionList.FirstOrDefault(o => o.Value.Name == size.Name).Value != null)
                {
                    SizeViewCellModel sizeViewCellModel = new SizeViewCellModel(size.Name, DataManager.SelectSize2SizePage(size.Name), "#4a4b5d");
                    SizeViewCell sizeViewCell = new SizeViewCell(sizeViewCellModel) { BindingContext = sizeViewCellModel };
                    sizeViewCell.Tapped += SizeViewCell_Tapped;
                    xName_List.Add(sizeViewCell);
                }
                else
                {
                    SizeViewCellModel sizeViewCellModel = new SizeViewCellModel(size.Name, DataManager.SelectSize2SizePage(size.Name), "");
                    SizeViewCell sizeViewCell = new SizeViewCell(sizeViewCellModel) { BindingContext = sizeViewCellModel };
                    sizeViewCell.Tapped += SizeViewCell_Tapped;
                    xName_List.Add(sizeViewCell);
                }
            }
        }
        private async void SizeViewCell_Tapped(object sender, EventArgs e)
        {
            App.Loading(this);
            ViewCell vc = (ViewCell)sender;
            var selectedSize = (SizeViewCellModel)vc.BindingContext;

            await Task.Run( () => {
                DataManager.SelectSize(selectedSize.Size);
            }); 
            await App._NavigationPage.PopAsync();
            App.Stop(this);
        }
        protected override void OnDisappearing()
        {
            xName_List.Clear();
        }

    }
}