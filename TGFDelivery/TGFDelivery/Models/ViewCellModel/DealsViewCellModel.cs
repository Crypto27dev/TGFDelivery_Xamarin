using System.Windows.Input;
using TGFDelivery.Views;
using Xamarin.Forms;

namespace TGFDelivery.Models.ViewCellModel
{
    public class DealsViewCellModel
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string BtnName { get; set; }
        public string BtnBackgroundColor { get; set; }
        public ICommand onChoose { get; set; }
        public DealsViewCellModel(string Id, string ImgUrl, string Name, string Desc, string BtnName, string BtnBackgroundColor)
        {
            this.Id = Id;
            this.ImgUrl = ImgUrl;
            this.Name = Name;
            this.Desc = Desc;
            this.BtnName = BtnName;
            this.BtnBackgroundColor = BtnBackgroundColor;
            onChoose = new Command(proc_Choose);
        }
        private void proc_Choose()
        {
            App._NavigationPage.PushAsync(new DealPage(Id, null));
        }
    }
}
