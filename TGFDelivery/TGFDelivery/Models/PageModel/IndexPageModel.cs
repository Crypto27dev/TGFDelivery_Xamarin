using TGFDelivery.Helpers;

namespace TGFDelivery.Models
{
    public class IndexPageModel : ViewModelBase
    {
        #region "Language"
        public string PostCodePlaceHolder
        {
            get { return Languages.PostCode; }
        }
        public string Collection
        {
            get { return Languages.Collection.ToUpper(); }
        }
        public string Delivery
        {
            get { return Languages.Delivery.ToUpper(); }
        }
        public string Login
        {
            get { return Languages.Login.ToUpper(); }
        }

        #endregion
    }
}
