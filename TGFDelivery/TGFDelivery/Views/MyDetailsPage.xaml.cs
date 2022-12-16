using System;
using TGFDelivery.Data;
using TGFDelivery.Models.PageModel;
using TGFDelivery.Models.ServiceModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyDetailsPage : ContentPage
    {
        MyDetailPageModel _Model = new MyDetailPageModel();
        public MyDetailsPage()
        {
            InitializeComponent();
            Init();
            BindingContext = _Model;
        }
        private async void Init()
        {
            App.Loading(this);
            try
            {
                CheckOutModel deModel = await DataManager.CheckOut();
                if (deModel.AddressList != null)
                {
                    if (deModel.AddressFormat != "UK")
                    {
                        foreach (var Address in deModel.AddressList)
                        {
                            _Model.AddressList.Add(string.Format("{0}, {1}", Address.DePostCode.PrimaryStreet, Address.DePostCode.SecondaryStreet));
                        }
                    }
                    else
                    {
                        foreach (var Address in deModel.AddressList)
                        {
                            _Model.AddressList.Add(string.Format("{0}, {1}, {2}", Address.BuildingNumber, Address.DePostCode.PrimaryStreet, Address.DePostCode.SecondaryStreet));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                App.Stop(this);
            }

        }

    }
}